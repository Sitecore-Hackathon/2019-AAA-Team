using Microsoft.Extensions.Logging;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.EntityViews;
using Sitecore.Framework.Pipelines;

namespace Hackathon.AAATeam.Feature.Navigation.Pipelines
{
    public class BizFxBreadcrumbItemPipeline : CommercePipeline<string, EntityView>, IBizFxBreadcrumbItemPipeline, IPipeline<string, EntityView, CommercePipelineExecutionContext>, IPipelineBlock<string, EntityView, CommercePipelineExecutionContext>, IPipelineBlock, IPipeline
    {
        public BizFxBreadcrumbItemPipeline(
          IPipelineConfiguration<IBizFxBreadcrumbItemPipeline> configuration,
          ILoggerFactory loggerFactory)
          : base(configuration, loggerFactory)
        {
        }
    }
}
