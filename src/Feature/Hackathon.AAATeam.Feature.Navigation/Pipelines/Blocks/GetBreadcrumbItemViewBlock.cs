using Hackathon.AAATeam.Feature.Navigation.Entities;
using Hackathon.AAATeam.Feature.Navigation.Policies;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.EntityViews;
using Sitecore.Commerce.Plugin.Catalog;
using Sitecore.Commerce.Plugin.Shops;
using Sitecore.Framework.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hackathon.AAATeam.Feature.Navigation.Models;

namespace Hackathon.AAATeam.Feature.Navigation.Pipelines.Blocks
{
    [PipelineDisplayName("AAATeam.Navigation.block.GetBreadcrumbItemView")]
    public class GetBreadcrumbItemViewBlock : PipelineBlock<string, BreadcrumbModel, CommercePipelineExecutionContext>
    {
        private readonly IFindEntityPipeline _findEntityPipeline;
        private readonly IFindEntitiesInListPipeline _findEntitiesInListPipeline;

        public GetBreadcrumbItemViewBlock(IFindEntityPipeline findEntityPipeline, IFindEntitiesInListPipeline findEntitiesInListPipeline)
        {
            _findEntityPipeline = findEntityPipeline;
            _findEntitiesInListPipeline = findEntitiesInListPipeline;
        }

        public override async Task<BreadcrumbModel> Run(string arg, CommercePipelineExecutionContext context)
        {
            var result  = new BreadcrumbModel();

            var currentItem = await _findEntityPipeline.Run(new FindEntityArgument(typeof(CatalogItemBase), arg, false), context).ConfigureAwait(false) as CatalogItemBase;
            var childList = new List<CatalogItemBase>();

            if (currentItem != null)
            {
                var children = await GetChildren(currentItem, context).ConfigureAwait(false);
                childList.AddRange(children);
            }


            

            return result;
        }

        private async Task<List<CatalogItemBase>> GetChildren(CatalogItemBase item, CommercePipelineExecutionContext context)
        {
            var result = new List<CatalogItemBase>();
            if (item == null || !item.HasComponent<ExtendedCatalogItemComponent>())
            {
                return result;
            }

            var component = item.GetComponent<ExtendedCatalogItemComponent>();


            if (item.Id.StartsWith(CommerceEntity.IdPrefix<Catalog>()) || item.Id.StartsWith(CommerceEntity.IdPrefix<Category>()))
            {
                if (!string.IsNullOrEmpty(component.ChildrenSellableItemEntitiesList))
                {
                    var sellableItems = component.ChildrenSellableItemEntitiesList.Split('|');

                    foreach(var sellableItemId in sellableItems)
                    {
                        var existingSellableItem = await _findEntityPipeline.Run(new FindEntityArgument(typeof(CatalogItemBase), sellableItemId, false), context).ConfigureAwait(false) as CatalogItemBase;

                        if (existingSellableItem != null)
                        {
                            result.Add(existingSellableItem);
                        }
                    }
                }

                if (!string.IsNullOrEmpty(component.ChildrenCategoryEntitiesList))
                {
                    var categories = component.ChildrenCategoryEntitiesList.Split('|');

                    foreach (var categoryItemId in categories)
                    {
                        var existingCategoryItem = await _findEntityPipeline.Run(new FindEntityArgument(typeof(CatalogItemBase), categoryItemId, false), context).ConfigureAwait(false) as CatalogItemBase;

                        if (existingCategoryItem != null)
                        {
                            result.Add(existingCategoryItem);
                        }
                    }
                }
            }

            return result;
        }
    }
}
