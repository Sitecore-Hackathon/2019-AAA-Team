using Hackathon.AAATeam.Feature.Navigation.Policies;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.EntityViews;
using Sitecore.Commerce.Plugin.Shops;
using Sitecore.Framework.Pipelines;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hackathon.AAATeam.Feature.Navigation.Pipelines.Blocks
{
    [PipelineDisplayName("AAATeam.Navigation.block.GetBreadcrumbView")]
    public class GetBreadcrumbViewBlock : PipelineBlock<string, EntityView, CommercePipelineExecutionContext>
    {
        public override Task<EntityView> Run(string arg, CommercePipelineExecutionContext context)
        {
            var entityView = new EntityView
            {
                EntityId = string.Empty,
                Name = context.GetPolicy<KnownExtendedBusinessUsersViewsPolicy>().ToolsBreadcrumb,
                Action = string.Empty,
                ItemId = string.Empty,
                UiHint = "List"
            };

            context.CommerceContext.AddObject(new EntityViewArgument
            {
                ViewName = context.GetPolicy<KnownExtendedBusinessUsersViewsPolicy>().ToolsBreadcrumb
            });

            Shop shop = context.CommerceContext.GetObjects<Shop>()
                .FirstOrDefault(s => s.Name.Equals(context.CommerceContext.CurrentShopName(), StringComparison.OrdinalIgnoreCase))
                ?? context.CommerceContext.GetEntities<Shop>()
                .FirstOrDefault(s => s.Name.Equals(context.CommerceContext.CurrentShopName(), StringComparison.OrdinalIgnoreCase));

            var level1 = new EntityView
            {
                EntityId = string.Empty,
                Name = context.GetPolicy<KnownExtendedBusinessUsersViewsPolicy>().ToolsBreadcrumb,
                Action = string.Empty,
                ItemId = string.Empty,
                UiHint = "List"
            };

            var viewPropertyArray = new[]
            {
                new ViewProperty
                {
                    Name = "Name",
                    RawValue = "Entity-Catalog-Habitat_Master",
                    IsReadOnly = true,
                    UiType = "EntityLink"
                },
                new ViewProperty
                {
                    Name = "DisplayName",
                    RawValue = "Habitat_Master",
                    IsReadOnly = true
                },
                new ViewProperty
                {
                    Name = "IsActive",
                    RawValue =true,
                    IsReadOnly = true
                },
                new ViewProperty
                {
                    Name = "Icon",
                    RawValue = "child.img",
                    IsReadOnly = true
                },
                new ViewProperty
                {
                    Name = "Href",
                    RawValue = "/entityView/Master/1/Entity-Catalog-Habitat_Master",
                    IsReadOnly = true
                },
                new ViewProperty
                {
                    Name = "Id",
                    RawValue = "/entityView/Master/1/Entity-Catalog-Habitat_Master",
                    IsReadOnly = true
                }
            };
            level1.Properties.AddRange(viewPropertyArray);
            entityView.ChildViews.Add(level1);

            var level2 = new EntityView
            {
                EntityId = string.Empty,
                Name = context.GetPolicy<KnownExtendedBusinessUsersViewsPolicy>().ToolsBreadcrumb,
                Action = string.Empty,
                ItemId = string.Empty,
                UiHint = "List"
            };

            level1.Properties.AddRange(new[]
            {
                new ViewProperty
                {
                    Name = "Name",
                    RawValue = "Entity-Catalog-Habitat_Master",
                    IsReadOnly = true,
                    UiType = "EntityLink"
                },
                new ViewProperty
                {
                    Name = "DisplayName",
                    RawValue = "Habitat_Master",
                    IsReadOnly = true
                },
                new ViewProperty
                {
                    Name = "IsActive",
                    RawValue =true,
                    IsReadOnly = true
                },
                new ViewProperty
                {
                    Name = "Icon",
                    RawValue = "child.img",
                    IsReadOnly = true
                },
                new ViewProperty
                {
                    Name = "Href",
                    RawValue = "/entityView/Master/1/Entity-Catalog-Habitat_Master",
                    IsReadOnly = true
                },
                new ViewProperty
                {
                    Name = "Id",
                    RawValue = "/entityView/Master/1/Entity-Catalog-Habitat_Master",
                    IsReadOnly = true
                }
            });
            entityView.ChildViews.Add(level2);

            var level3 = new EntityView
            {
                EntityId = string.Empty,
                Name = context.GetPolicy<KnownExtendedBusinessUsersViewsPolicy>().ToolsBreadcrumb,
                Action = string.Empty,
                ItemId = string.Empty,
                UiHint = "List"
            };

            level1.Properties.AddRange(new[]
            {
                new ViewProperty
                {
                    Name = "Name",
                    RawValue = "Entity-Catalog-Habitat_Master",
                    IsReadOnly = true,
                    UiType = "EntityLink"
                },
                new ViewProperty
                {
                    Name = "DisplayName",
                    RawValue = "Habitat_Master",
                    IsReadOnly = true
                },
                new ViewProperty
                {
                    Name = "IsActive",
                    RawValue =true,
                    IsReadOnly = true
                },
                new ViewProperty
                {
                    Name = "Icon",
                    RawValue = "child.img",
                    IsReadOnly = true
                },
                new ViewProperty
                {
                    Name = "Href",
                    RawValue = "/entityView/Master/1/Entity-Catalog-Habitat_Master",
                    IsReadOnly = true
                },
                new ViewProperty
                {
                    Name = "Id",
                    RawValue = "/entityView/Master/1/Entity-Catalog-Habitat_Master",
                    IsReadOnly = true
                }
            });
            entityView.ChildViews.Add(level3);

            return Task.FromResult(entityView);
        }
    }
}
