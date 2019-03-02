using Hackathon.AAATeam.Feature.Navigation.Models;
using Sitecore.Commerce.Plugin.Catalog;

namespace Hackathon.AAATeam.Feature.Navigation
{
    public static class Extensions
    {
        public const string CatalogIconPath = "";
        public const string CategoryIconPath = "";
        public const string ProductIconPath = "";

        public static BreadcrumbModel ToBreadcrumbModel( this CatalogItemBase item, int version)
        {
            var result = new BreadcrumbModel
            {
                DisplayMode = item.DisplayName,
                Name = item.Name,
                IsActive = true,
                Href = $"{version}/{item.Name}"
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