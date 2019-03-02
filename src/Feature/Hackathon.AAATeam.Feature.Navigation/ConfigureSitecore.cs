namespace Hackathon.AAATeam.Feature.Navigation
{
    using System.Reflection;
    using Hackathon.AAATeam.Feature.Navigation.Pipelines;
    using Hackathon.AAATeam.Feature.Navigation.Pipelines.Blocks;
    using Microsoft.Extensions.DependencyInjection;
    using Sitecore.Commerce.Core;
    using Sitecore.Commerce.EntityViews;
    using Sitecore.Commerce.Plugin.Catalog;
    using Sitecore.Framework.Configuration;
    using Sitecore.Framework.Pipelines.Definitions.Extensions;

    public class ConfigureSitecore : IConfigureSitecore
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.RegisterAllPipelineBlocks(assembly);

            services.Sitecore().Pipelines(config => config

            .AddPipeline<IBizFxBreadcrumbPipeline, BizFxBreadcrumbPipeline>(
                configure =>
                    {
                        configure.Add<GetBreadcrumbViewBlock>();
                    })
            .AddPipeline<IBizFxBreadcrumbItemPipeline, BizFxBreadcrumbItemPipeline>(
                configure =>
                {
                    configure.Add<GetBreadcrumbItemViewBlock>();
                })
            .ConfigurePipeline<ICreateRelationshipPipeline>(
                configure =>
                {
                    configure.Add<UpdateCatalogCustomHierarchyBlock>().Before<UpdateCatalogHierarchyBlock>();
                })
            .ConfigurePipeline<IDeleteRelationshipPipeline>(
                configure =>
                {
                    configure.Add<UpdateCatalogCustomHierarchyBlock>().Before<UpdateCatalogHierarchyBlock>();
                })
            .ConfigurePipeline<IConfigureServiceApiPipeline>(
                configure =>
                {
                    configure.Add<ConfigureServiceApiBlock>();
                })
             );

            services.RegisterAllCommands(assembly);
        }
    }
}