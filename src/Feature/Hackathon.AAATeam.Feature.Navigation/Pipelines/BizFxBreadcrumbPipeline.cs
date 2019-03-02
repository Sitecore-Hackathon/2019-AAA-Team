using Microsoft.Extensions.Logging;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.EntityViews;
using Sitecore.Framework.Pipelines;

namespace Hackathon.AAATeam.Feature.Navigation.Pipelines
{
    public class BizFxBreadcrumbPipeline : CommercePipeline<string, EntityView>, IBizFxBreadcrumbPipeline, IPipeline<string, EntityView, CommercePipelineExecutionContext>, IPipelineBlock<string, EntityView, CommercePipelineExecutionContext>, IPipelineBlock, IPipeline
    {
        public BizFxBreadcrumbPipeline(
          IPipelineConfiguration<IBizFxBreadcrumbPipeline> configuration,
          ILoggerFactory loggerFactory)
          : base(configuration, loggerFactory)
        {
        }
    }
}
