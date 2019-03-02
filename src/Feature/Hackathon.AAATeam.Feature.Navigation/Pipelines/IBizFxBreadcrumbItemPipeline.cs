using Hackathon.AAATeam.Feature.Navigation.Models;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.EntityViews;
using Sitecore.Framework.Pipelines;
using System.Collections.Generic;

namespace Hackathon.AAATeam.Feature.Navigation.Pipelines
{
    [PipelineDisplayName("AAATeam.Navigation.Pipeline.BizFxBreadcrumbItem")]
    public interface IBizFxBreadcrumbItemPipeline : IPipeline<string, List<BreadcrumbModel>, CommercePipelineExecutionContext>, IPipelineBlock<string, List<BreadcrumbModel>, CommercePipelineExecutionContext>, IPipelineBlock, IPipeline
    {
    }
}
