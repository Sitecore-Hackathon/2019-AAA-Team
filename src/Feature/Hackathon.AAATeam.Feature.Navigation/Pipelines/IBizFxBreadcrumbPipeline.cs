using Hackathon.AAATeam.Feature.Navigation.Models;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.EntityViews;
using Sitecore.Framework.Pipelines;

namespace Hackathon.AAATeam.Feature.Navigation.Pipelines
{
    [PipelineDisplayName("AAATeam.Navigation.Pipeline.BizFxBreadcrumb")]
    public interface IBizFxBreadcrumbPipeline : IPipeline<string, BreadcrumbModel, CommercePipelineExecutionContext>, IPipelineBlock<string, BreadcrumbModel, CommercePipelineExecutionContext>, IPipelineBlock, IPipeline
    {
    }
}
