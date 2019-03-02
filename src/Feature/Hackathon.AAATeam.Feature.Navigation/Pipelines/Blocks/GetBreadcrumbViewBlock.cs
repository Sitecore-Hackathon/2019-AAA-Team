using Sitecore.Commerce.Core;
using Sitecore.Framework.Pipelines;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hackathon.AAATeam.Feature.Navigation.Models;
using Sitecore.Commerce.Plugin.Catalog;
using Hackathon.AAATeam.Feature.Navigation.Entities;

namespace Hackathon.AAATeam.Feature.Navigation.Pipelines.Blocks
{
    [PipelineDisplayName("AAATeam.Navigation.block.GetBreadcrumbView")]
    public class GetBreadcrumbViewBlock : PipelineBlock<string, List<BreadcrumbModel>, CommercePipelineExecutionContext>
    {
        private readonly IFindEntityPipeline _findEntityPipeline;
        private readonly IFindEntityVersionsPipeline _findEntityVersionsPipeline;

        public GetBreadcrumbViewBlock(IFindEntityPipeline findEntityPipeline, IFindEntityVersionsPipeline findEntityVersionsPipeline)
        {
            _findEntityPipeline = findEntityPipeline;
            _findEntityVersionsPipeline = findEntityVersionsPipeline;
        }

        public override async Task<List<BreadcrumbModel>> Run(string arg, CommercePipelineExecutionContext context)
        {
            var result = new List<BreadcrumbModel>();

            var currentItem =
                await _findEntityPipeline.Run(new FindEntityArgument(typeof(CatalogItemBase), arg, false), context)
                    .ConfigureAwait(false) as CatalogItemBase;

            if (currentItem == null)
            {
                return result;
            }

            var childList = new List<CatalogItemBase>();

            var model = new List<CatalogItemBase>();
            await GetParents(currentItem, model, context).ConfigureAwait(false);
            childList.AddRange(model);
            childList.Add(currentItem);

            foreach (var itm in childList)
            {
                var version = 1;
                var commerceEntities =
                    await _findEntityVersionsPipeline.Run(new FindEntityArgument(itm.GetType(), itm.Id, false),
                        context);
                if (commerceEntities != null)
                {
                    var itemVersion = commerceEntities.LastOrDefault();
                    version = itemVersion?.Version ?? 1;
                }

                result.Add(itm.ToBreadcrumbModel(version));
            }

            return result;
        }

        private async Task<List<CatalogItemBase>> GetParents(CatalogItemBase item, List<CatalogItemBase> model, CommercePipelineExecutionContext context)
        {
            if (item == null || !item.HasComponent<ExtendedCatalogItemComponent>())
            {
                return model;
            }

            var component = item.GetComponent<ExtendedCatalogItemComponent>();

            if (!string.IsNullOrEmpty(component.ParentCategoryEntitiesList))
            {
                var existingCategoryItem = await _findEntityPipeline.Run(new FindEntityArgument(typeof(CatalogItemBase), component.ParentCategoryEntitiesList, false), context).ConfigureAwait(false) as CatalogItemBase;
                model.Add(existingCategoryItem);
                await GetParents(existingCategoryItem, model, context).ConfigureAwait(false);
            }

            model.Reverse();

            return model;
        }
    }
}
