using Sitecore.Commerce.Core;
using Sitecore.Commerce.EntityViews;
using Sitecore.Framework.Pipelines;

namespace Hackathon.AAATeam.Feature.Navigation.Pipelines
{
    [PipelineDisplayName("AAATeam.Navigation.Pipeline.BizFxBreadcrumbItem")]
    public interface IBizFxBreadcrumbItemPipeline : IPipeline<string, EntityView, CommercePipelineExecutionContext>, IPipelineBlock<string, EntityView, CommercePipelineExecutionContext>, IPipelineBlock, IPipeline
    {
    }
}
