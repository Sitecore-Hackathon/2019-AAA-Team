namespace Hackathon.AAATeam.Feature.Navigation
{
    using System;
    using System.Threading.Tasks;
    using Hackathon.AAATeam.Feature.Navigation.Pipelines;
    using Sitecore.Commerce.Core;
    using Sitecore.Commerce.Core.Commands;
    using Sitecore.Commerce.EntityViews;

    public class BizFxBreadcrumbItemCommand : CommerceCommand
    {
        private readonly IBizFxBreadcrumbItemPipeline _pipeline;

        public BizFxBreadcrumbItemCommand(IBizFxBreadcrumbItemPipeline pipeline, IServiceProvider serviceProvider)
          : base(serviceProvider)
        {
            _pipeline = pipeline;
        }

        public virtual async Task<EntityView> Process(string id, CommerceContext commerceContext)
        {
            EntityView entityView;

            using (CommandActivity.Start(commerceContext, this))
            {
                entityView = await _pipeline.Run(id, commerceContext.GetPipelineContextOptions());
            }
            return entityView;
        }
    }
}