using Sitecore.Commerce.Core;
using Sitecore.Framework.Pipelines;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hackathon.AAATeam.Feature.Navigation.Models;
using Sitecore.Commerce.Plugin.Catalog;
using Hackathon.AAATeam.Feature.Navigation.Entities;

namespace Hackathon.AAATeam.Feature.Navigation.Pipelines.Blocks
{
    [PipelineDisplayName("AAATeam.Navigation.block.GetBreadcrumbView")]
    public class GetBreadcrumbViewBlock : PipelineBlock<string, BreadcrumbModel, CommercePipelineExecutionContext>
    {
        private readonly IFindEntityPipeline _findEntityPipeline;

        public GetBreadcrumbViewBlock(IFindEntityPipeline findEntityPipeline)
        {
            _findEntityPipeline = findEntityPipeline;
        }

        public override async Task<BreadcrumbModel> Run(string arg, CommercePipelineExecutionContext context)
        {
            var result = new BreadcrumbModel();

            var currentItem = await _findEntityPipeline.Run(new FindEntityArgument(typeof(CatalogItemBase), arg, false), context).ConfigureAwait(false) as CatalogItemBase;
            var childList = new List<CatalogItemBase>();

            if (currentItem != null)
            {
                var model = new List<CatalogItemBase>();
                await GetParents(currentItem, model, context).ConfigureAwait(false);
                childList.AddRange(model);
                childList.Add(currentItem);
            }

            //TODO: prepare model

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
