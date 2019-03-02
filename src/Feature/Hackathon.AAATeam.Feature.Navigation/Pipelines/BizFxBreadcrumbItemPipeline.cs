using Hackathon.AAATeam.Feature.Navigation.Models;
using Microsoft.Extensions.Logging;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.EntityViews;
using Sitecore.Framework.Pipelines;

namespace Hackathon.AAATeam.Feature.Navigation.Pipelines
{
    public class BizFxBreadcrumbItemPipeline : CommercePipeline<string, BreadcrumbModel>, IBizFxBreadcrumbItemPipeline, IPipeline<string, BreadcrumbModel, CommercePipelineExecutionContext>, IPipelineBlock<string, BreadcrumbModel, CommercePipelineExecutionContext>, IPipelineBlock, IPipeline
    {
        public BizFxBreadcrumbItemPipeline(
          IPipelineConfiguration<IBizFxBreadcrumbItemPipeline> configuration,
          ILoggerFactory loggerFactory)
          : base(configuration, loggerFactory)
        {
        }
    }
}
