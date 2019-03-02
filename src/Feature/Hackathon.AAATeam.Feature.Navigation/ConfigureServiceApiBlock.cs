namespace Hackathon.AAATeam.Feature.Navigation
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.OData.Builder;

    using Sitecore.Commerce.Core;
    using Sitecore.Commerce.EntityViews;
    using Sitecore.Framework.Conditions;
    using Sitecore.Framework.Pipelines;

    [PipelineDisplayName("SamplePluginConfigureServiceApiBlock")]
    public class ConfigureServiceApiBlock : PipelineBlock<ODataConventionModelBuilder, ODataConventionModelBuilder, CommercePipelineExecutionContext>
    {
        public override Task<ODataConventionModelBuilder> Run(ODataConventionModelBuilder modelBuilder, CommercePipelineExecutionContext context)
        {
            Condition.Requires(modelBuilder).IsNotNull($"{this.Name}: The argument cannot be null.");

            var getBreadcrumbByItemId = modelBuilder.Function("GetBreadcrumbByItemId");
            getBreadcrumbByItemId.Parameter<string>("itemId");
            getBreadcrumbByItemId.Returns<EntityView>();

            var getChildrenByItemId = modelBuilder.Function("GetChildrenByItemId");
            getChildrenByItemId.Parameter<string>("itemId");
            getChildrenByItemId.Returns<EntityView>();
            
            return Task.FromResult(modelBuilder);
        }
    }
}
