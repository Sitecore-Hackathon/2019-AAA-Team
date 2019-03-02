using System.Collections.Generic;

namespace Hackathon.AAATeam.Feature.Navigation.Models
{
    public class BreadcrumItemsModel : Sitecore.Commerce.Core.Model
    {
        public List<BreadcrumbModel> Items { get; set; }
        public BreadcrumItemsModel()
        {
            Items = new List<BreadcrumbModel>();
        }

        public BreadcrumItemsModel(List<BreadcrumbModel> items)
        {
            Items = items;
        }
    }
}
