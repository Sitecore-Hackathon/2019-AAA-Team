using Sitecore.Commerce.Core;

namespace Hackathon.AAATeam.Feature.Navigation.Entities
{
    public class ExtendedCatalogItemComponent : Component
    {
        public string ParentCatalogEntitiesList { get; set; }

        public string ParentCategoryEntitiesList { get; set; }

        public string ChildrenCategoryEntitiesList { get; set; }

        public string ChildrenSellableItemEntitiesList { get; set; }
    }
}
