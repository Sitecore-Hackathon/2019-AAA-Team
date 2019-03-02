using System.Collections.Generic;

namespace Hackathon.AAATeam.Feature.Navigation.Models
{
    public class BreadcrumsItems : Sitecore.Commerce.Core.Model
    {
        public List<BreadcrumbModel> Items { get; set; }
        public BreadcrumsItems()
        {
            Items = new List<BreadcrumbModel>();
        }

        public BreadcrumsItems(List<BreadcrumbModel> items)
        {
            Items = items;
        }
    }
}
