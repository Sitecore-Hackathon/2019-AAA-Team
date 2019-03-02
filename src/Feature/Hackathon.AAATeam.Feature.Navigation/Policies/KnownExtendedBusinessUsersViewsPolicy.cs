using Sitecore.Commerce.Plugin.BusinessUsers;

namespace Hackathon.AAATeam.Feature.Navigation.Policies
{
    public class KnownExtendedBusinessUsersViewsPolicy : KnownBusinessUsersViewsPolicy
    {
        public KnownExtendedBusinessUsersViewsPolicy()
        {
            ToolsBreadcrumb = nameof(ToolsBreadcrumb);
        }

        public string ToolsBreadcrumb { get; set; }
    }
}
