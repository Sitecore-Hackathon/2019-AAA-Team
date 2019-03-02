using Hackathon.AAATeam.Feature.Navigation.Models;
using Microsoft.Extensions.Logging;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.EntityViews;
using Sitecore.Framework.Pipelines;
using System.Collections.Generic;

namespace Hackathon.AAATeam.Feature.Navigation.Pipelines
{
    public class BizFxBreadcrumbItemPipeline : CommercePipeline<string, List<BreadcrumbModel>>, IBizFxBreadcrumbItemPipeline, IPipeline<string, List<BreadcrumbModel>, CommercePipelineExecutionContext>, IPipelineBlock<string, List<BreadcrumbModel>, CommercePipelineExecutionContext>, IPipelineBlock, IPipeline
    {
        public BizFxBreadcrumbItemPipeline(
          IPipelineConfiguration<IBizFxBreadcrumbItemPipeline> configuration,
          ILoggerFactory loggerFactory)
          : base(configuration, loggerFactory)
        {
        }
    }
}
