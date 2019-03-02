namespace Hackathon.AAATeam.Feature.Navigation
{
    using System.Threading.Tasks;
    using Hackathon.AAATeam.Feature.Navigation.Models;
    using Microsoft.AspNetCore.OData.Builder;

    using Sitecore.Commerce.Core;
    using Sitecore.Framework.Conditions;
    using Sitecore.Framework.Pipelines;

    [PipelineDisplayName("AAATeamPluginConfigureServiceApiBlock")]
    public class ConfigureServiceApiBlock : PipelineBlock<ODataConventionModelBuilder, ODataConventionModelBuilder, CommercePipelineExecutionContext>
    {
        public override Task<ODataConventionModelBuilder> Run(ODataConventionModelBuilder modelBuilder, CommercePipelineExecutionContext context)
        {
            Condition.Requires(modelBuilder).IsNotNull($"{this.Name}: The argument cannot be null.");

            var getBreadcrumbByItemId = modelBuilder.Function("GetBreadcrumbByItemId");
            getBreadcrumbByItemId.Parameter<string>("itemId");
            getBreadcrumbByItemId.Returns<BreadcrumItemsModel>();

            var getChildrenByItemId = modelBuilder.Function("GetChildrenByItemId");
            getChildrenByItemId.Parameter<string>("itemId");
            getChildrenByItemId.Returns<BreadcrumItemsModel>();
            
            return Task.FromResult(modelBuilder);
        }
    }
}
