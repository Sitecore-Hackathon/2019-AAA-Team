using Hackathon.AAATeam.Feature.Navigation.Models;
using Sitecore.Commerce.Plugin.Catalog;

namespace Hackathon.AAATeam.Feature.Navigation.Extensions
{
    public static class BreadcrumbExtensions
    {
        public const string CatalogIconPath = "si si-folders2";
        public const string CategoryIconPath = "si si-folder2";
        public const string ProductIconPath = "si si-sitecore_logo";

        public static BreadcrumbModel ToBreadcrumbModel( this CatalogItemBase item, int version)
        {
            var result = new BreadcrumbModel
            {
                DisplayName = item.DisplayName,
                Name = item.Name,
                IsActive = true,
                Href = $"/entityView/Master/{version}/{item.Name}",
                EntityId = item.Id
            };

            if (item is Category)
            {
                result.Icon = CategoryIconPath;
            }
            else if (item is SellableItem)
            {
                result.Icon = ProductIconPath;
            }
            else if (item is Catalog)
            {
                result.Icon = CatalogIconPath;
            }

            return result;
        }
    }
}