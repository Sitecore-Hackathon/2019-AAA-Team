using Hackathon.AAATeam.Feature.Navigation.Entities;
using Hackathon.AAATeam.Feature.Navigation.Policies;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.EntityViews;
using Sitecore.Commerce.Plugin.Catalog;
using Sitecore.Commerce.Plugin.Shops;
using Sitecore.Framework.Pipelines;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Hackathon.AAATeam.Feature.Navigation.Pipelines.Blocks
{
    [PipelineDisplayName("AAATeam.Navigation.block.GetBreadcrumbView")]
    public class GetBreadcrumbViewBlock : PipelineBlock<string, EntityView, CommercePipelineExecutionContext>
    {
        private readonly IFindEntityPipeline _findEntityPipeline;

        public GetBreadcrumbViewBlock(IFindEntityPipeline findEntityPipeline)
        {
            _findEntityPipeline = findEntityPipeline;
        }

        public override async Task<EntityView> Run(string arg, CommercePipelineExecutionContext context)
        {
            CatalogItemBase catalogItemBase = await _findEntityPipeline.Run(new FindEntityArgument(typeof(CatalogItemBase), arg, false), context.ContextOptions) as CatalogItemBase;
            var catalogItemBaseComponent = catalogItemBase.GetComponent<ExtendedCatalogItemComponent>();

            EntityView entityView = new EntityView();
            //entityView.EntityId = string.Empty;
            //entityView.Name = context.GetPolicy<KnownExtendedBusinessUsersViewsPolicy>().ToolsBreadcrumb;
            //entityView.Action = string.Empty;
            //entityView.ItemId = string.Empty;
            //entityView.UiHint = "List";

            //context.CommerceContext.AddObject(new EntityViewArgument()
            //{
            //    ViewName = context.GetPolicy<KnownExtendedBusinessUsersViewsPolicy>().ToolsBreadcrumb
            //});

            //Shop shop = context.CommerceContext.GetObjects<Shop>()
            //    .FirstOrDefault(s => s.Name.Equals(context.CommerceContext.CurrentShopName(), StringComparison.OrdinalIgnoreCase))
            //    ?? context.CommerceContext.GetEntities<Shop>()
            //    .FirstOrDefault(s => s.Name.Equals(context.CommerceContext.CurrentShopName(), StringComparison.OrdinalIgnoreCase));
            //List<string> stringList;

            //if (shop == null || !shop.Languages.Any())
            //{
            //    stringList = new List<string>
            //    {
            //        context.CommerceContext.GetPolicy<GlobalEnvironmentPolicy>().DefaultLocale
            //    };
            //}
            //else
            //{
            //    stringList = shop.Languages;
            //}

            //stringList.ForEach(language =>
            //{
            //    EntityView entityView2 = new EntityView()
            //    {
            //        EntityId = string.Empty,
            //        ItemId = language,
            //        Name = context.GetPolicy<KnownExtendedBusinessUsersViewsPolicy>().Summary
            //    };
            //    CultureInfo cultureInfo = new CultureInfo(language);
            //    List<ViewProperty> properties1 = entityView2.Properties;
            //    properties1.Add(new ViewProperty()
            //    {
            //        Name = "Name",
            //        RawValue = cultureInfo.Name,
            //        IsReadOnly = true,
            //        IsHidden = false
            //    });
            //    List<ViewProperty> properties2 = entityView2.Properties;
            //    properties2.Add(new ViewProperty()
            //    {
            //        Name = "DisplayName",
            //        RawValue = cultureInfo.DisplayName + ": " + cultureInfo.NativeName,
            //        IsReadOnly = true,
            //        IsHidden = false
            //    });
            //    entityView.ChildViews.Add(entityView2);
            //});
            return entityView;
        }
    }
}
