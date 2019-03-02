using Hackathon.AAATeam.Feature.Navigation.Models;
using Microsoft.Extensions.Logging;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.EntityViews;
using Sitecore.Framework.Pipelines;

namespace Hackathon.AAATeam.Feature.Navigation.Pipelines
{
    public class BizFxBreadcrumbPipeline : CommercePipeline<string, BreadcrumbModel>, IBizFxBreadcrumbPipeline, IPipeline<string, BreadcrumbModel, CommercePipelineExecutionContext>, IPipelineBlock<string, BreadcrumbModel, CommercePipelineExecutionContext>, IPipelineBlock, IPipeline
    {
        public BizFxBreadcrumbPipeline(
          IPipelineConfiguration<IBizFxBreadcrumbPipeline> configuration,
          ILoggerFactory loggerFactory)
          : base(configuration, loggerFactory)
        {
        }
    }
}
