using Hackathon.AAATeam.Feature.Navigation.Entities;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Plugin.Catalog;
using Sitecore.Framework.Pipelines;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hackathon.AAATeam.Feature.Navigation.Models;

namespace Hackathon.AAATeam.Feature.Navigation.Pipelines.Blocks
{
    [PipelineDisplayName("AAATeam.Navigation.block.GetBreadcrumbItemView")]
    public class GetBreadcrumbItemViewBlock : PipelineBlock<string, List<BreadcrumbModel>, CommercePipelineExecutionContext>
    {
        private readonly IFindEntityPipeline _findEntityPipeline;
        private readonly IFindEntitiesInListPipeline _findEntitiesInListPipeline;
        private readonly IFindEntityVersionsPipeline _findEntityVersionsPipeline;

        public GetBreadcrumbItemViewBlock(IFindEntityPipeline findEntityPipeline, IFindEntitiesInListPipeline findEntitiesInListPipeline, IFindEntityVersionsPipeline findEntityVersionsPipeline)
        {
            _findEntityPipeline = findEntityPipeline;
            _findEntitiesInListPipeline = findEntitiesInListPipeline;
            _findEntityVersionsPipeline = findEntityVersionsPipeline;
        }

        public override async Task<List<BreadcrumbModel>> Run(string arg, CommercePipelineExecutionContext context)
        {
            var result = new List<BreadcrumbModel>();

            if (string.IsNullOrWhiteSpace(arg))
            {
                return result;
            }

            var currentItem =
                await _findEntityPipeline.Run(new FindEntityArgument(typeof(CatalogItemBase), arg, false), context)
                    .ConfigureAwait(false) as CatalogItemBase;

            if (currentItem == null)
            {
                return result;
            }

            var children = await GetChildren(currentItem, context).ConfigureAwait(false);

            foreach (var itm in children)
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
