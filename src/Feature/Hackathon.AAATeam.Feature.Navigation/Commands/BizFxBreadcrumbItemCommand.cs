﻿namespace Hackathon.AAATeam.Feature.Navigation
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Hackathon.AAATeam.Feature.Navigation.Models;
    using Hackathon.AAATeam.Feature.Navigation.Pipelines;
    using Sitecore.Commerce.Core;
    using Sitecore.Commerce.Core.Commands;

    public class BizFxBreadcrumbItemCommand : CommerceCommand
    {
        private readonly IBizFxBreadcrumbItemPipeline _pipeline;

        public BizFxBreadcrumbItemCommand(IBizFxBreadcrumbItemPipeline pipeline, IServiceProvider serviceProvider)
          : base(serviceProvider)
        {
            _pipeline = pipeline;
        }

        public virtual async Task<List<BreadcrumbModel>> Process(string id, CommerceContext commerceContext)
        {
            List<BreadcrumbModel> entityView;

            using (CommandActivity.Start(commerceContext, this))
            {
                entityView = await _pipeline.Run(id, commerceContext.GetPipelineContextOptions());
            }
            return entityView;
        }
    }
}