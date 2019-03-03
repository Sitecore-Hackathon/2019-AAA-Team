using Sitecore.Commerce.Core;
using Sitecore.Framework.Pipelines;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hackathon.AAATeam.Feature.Navigation.Models;
using Sitecore.Commerce.Plugin.Catalog;
using Hackathon.AAATeam.Feature.Navigation.Entities;
using System;
using Hackathon.AAATeam.Feature.Navigation.Extensions;

namespace Hackathon.AAATeam.Feature.Navigation.Pipelines.Blocks
{
    [PipelineDisplayName("AAATeam.Navigation.block.GetBreadcrumbView")]
    public class GetBreadcrumbViewBlock : PipelineBlock<string, List<BreadcrumbModel>, CommercePipelineExecutionContext>
    {
        private readonly IFindEntityPipeline _findEntityPipeline;
        private readonly IFindEntityVersionsPipeline _findEntityVersionsPipeline;
        private readonly CommerceCommander _commerceCommander;
        private readonly IGetCatalogsPipeline _getCatalogsPipeline;

        public GetBreadcrumbViewBlock(CommerceCommander commerceCommander, IFindEntityPipeline findEntityPipeline, IFindEntityVersionsPipeline findEntityVersionsPipeline, IGetCatalogsPipeline getCatalogsPipeline)
        {
            _commerceCommander = commerceCommander;
            _findEntityPipeline = findEntityPipeline;
            _findEntityVersionsPipeline = findEntityVersionsPipeline;
            _getCatalogsPipeline = getCatalogsPipeline;
        }

        public override async Task<List<BreadcrumbModel>> Run(string arg, CommercePipelineExecutionContext context)
        {
            var result = new List<BreadcrumbModel>();
            var childList = new List<CatalogItemBase>();

            
            if (arg.StartsWith("Entity-"))
            {
                var currentItem =
                    await _findEntityPipeline.Run(new FindEntityArgument(typeof(CatalogItemBase), arg, false), context)
                        .ConfigureAwait(false) as CatalogItemBase;

                if (currentItem == null)
                {
                    result.Add(new BreadcrumbModel() { Href = "/", Icon = "si si-temple", IsActive = false, Name = "COMMERCE", EntityId = "root" });
                    return result;
                }

                var model = new List<CatalogItemBase>();

                childList.Add(currentItem);
                await GetParents(currentItem, model, context).ConfigureAwait(false);
                childList.AddRange(model);

                if (currentItem != null && currentItem.HasComponent<ExtendedCatalogItemComponent>())
                {
                    var component = currentItem.GetComponent<ExtendedCatalogItemComponent>();
                    var catalog = await _findEntityPipeline.Run(new FindEntityArgument(typeof(CatalogItemBase), component.ParentCatalogEntitiesList, false), context) as CatalogItemBase;
                    childList.Add(catalog);
                }
            }

            result.Add(new BreadcrumbModel() { Href = "/", Icon = "si si-temple", IsActive = false, Name = "COMMERCE", EntityId = "root" });
            childList.Reverse();

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
                var existingCategoryItem = await _findEntityPipeline.Run(new FindEntityArgument(typeof(CatalogItemBase), component.ParentCategoryEntitiesList, false), context) as CatalogItemBase;
                model.Add(existingCategoryItem);
                await GetParents(existingCategoryItem, model, context).ConfigureAwait(false);
            }

            return model;
        }
    }
}
