﻿namespace Hackathon.AAATeam.Feature.Navigation.Models
{
    public class BreadcrumbModel : Sitecore.Commerce.Core.Model
    {
        public string DisplayName { get; set; }
        public bool IsActive { get; set; }
        public string Icon { get; set; }
        public string Href { get; set; }
        public string EntityId { get; set; }
    }
}
