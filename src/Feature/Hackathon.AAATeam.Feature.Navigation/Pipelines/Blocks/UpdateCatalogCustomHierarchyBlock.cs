using Hackathon.AAATeam.Feature.Navigation.Entities;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Plugin.Catalog;
using Sitecore.Framework.Conditions;
using Sitecore.Framework.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hackathon.AAATeam.Feature.Navigation.Pipelines.Blocks
{
    [PipelineDisplayName("AAATeam.Navigation.block.UpdateCatalogCustomHierarchy")]
    public class UpdateCatalogCustomHierarchyBlock : PipelineBlock<RelationshipArgument, RelationshipArgument, CommercePipelineExecutionContext>
    {
        private const string CatalogToCategory = "CatalogToCategory";
        private const string CategoryToCategory = "CategoryToCategory";
        private const string CatalogToSellableItem = "CatalogToSellableItem";
        private const string CategoryToSellableItem = "CategoryToSellableItem";
        private readonly IFindEntityPipeline _findEntityPipeline;
        private readonly IPersistEntityPipeline _persistEntityPipeline;

        public UpdateCatalogCustomHierarchyBlock(
          IFindEntityPipeline findEntityPipeline,
          IPersistEntityPipeline persistEntityPipeline)
        {
            _findEntityPipeline = findEntityPipeline;
            _persistEntityPipeline = persistEntityPipeline;
        }

        public override async Task<RelationshipArgument> Run(
          RelationshipArgument arg,
          CommercePipelineExecutionContext context)
        {
            Condition.Requires(arg).IsNotNull(Name + ": The argument can not be null");
            Condition.Requires(arg.TargetName).IsNotNullOrEmpty(Name + ": The target name can not be null or empty");
            Condition.Requires(arg.SourceName).IsNotNullOrEmpty(Name + ": The source name can not be null or empty");
            Condition.Requires(arg.RelationshipType).IsNotNullOrEmpty(Name + ": The relationship type can not be null or empty");

            if (!((IEnumerable<string>)new string[4]
            {
                CatalogToCategory,
                CatalogToSellableItem,
                CategoryToCategory,
                CategoryToSellableItem
            })
            .Contains(arg.RelationshipType, StringComparer.OrdinalIgnoreCase))
            {
                return arg;
            }

            CatalogItemBase source = await _findEntityPipeline.Run(new FindEntityArgument(typeof(CatalogItemBase), arg.SourceName, false), context) as CatalogItemBase;
            List<string> stringList = new List<string>();

            if (arg.TargetName.Contains("|"))
            {
                string[] strArray = arg.TargetName.Split('|');
                stringList.AddRange(strArray);
            }
            else
            {
                stringList.Add(arg.TargetName);
            }

            ValueWrapper<bool> sourceChanged = new ValueWrapper<bool>(false);

            if (!source.HasComponent<ExtendedCatalogItemComponent>())
            {
                source.SetComponent(new ExtendedCatalogItemComponent());
            }

            var sourceComponent = source.GetComponent<ExtendedCatalogItemComponent>();

            foreach (string entityId in stringList)
            {
                CatalogItemBase catalogItemBase = await _findEntityPipeline.Run(new FindEntityArgument(typeof(CatalogItemBase), entityId, false), context) as CatalogItemBase;
                if (source != null && catalogItemBase != null)
                {
                    if (!catalogItemBase.HasComponent<ExtendedCatalogItemComponent>())
                    {
                        catalogItemBase.SetComponent(new ExtendedCatalogItemComponent());
                    }

                    var catalogItemBaseComponent = catalogItemBase.GetComponent<ExtendedCatalogItemComponent>();
                    ValueWrapper<bool> changed = new ValueWrapper<bool>(false);

                    if (arg.RelationshipType.Equals(CatalogToCategory, StringComparison.OrdinalIgnoreCase))
                    {
                        sourceComponent.ChildrenCategoryEntitiesList = UpdateHierarchy(arg, catalogItemBase.Id, sourceComponent.ChildrenCategoryEntitiesList, sourceChanged);
                        catalogItemBaseComponent.ParentCatalogEntitiesList = UpdateHierarchy(arg, source.Id, catalogItemBaseComponent.ParentCatalogEntitiesList, changed);
                    }
                    else if (arg.RelationshipType.Equals(CategoryToCategory, StringComparison.OrdinalIgnoreCase))
                    {
                        sourceComponent.ChildrenCategoryEntitiesList = UpdateHierarchy(arg, catalogItemBase.Id, sourceComponent.ChildrenCategoryEntitiesList, sourceChanged);
                        catalogItemBaseComponent.ParentCategoryEntitiesList = UpdateHierarchy(arg, source.Id, catalogItemBaseComponent.ParentCategoryEntitiesList, changed);
                        catalogItemBaseComponent.ParentCatalogEntitiesList = UpdateHierarchy(arg, ExtractCatalogId(source.Id), catalogItemBaseComponent.ParentCatalogEntitiesList, changed);
                    }
                    else if (arg.RelationshipType.Equals(CatalogToSellableItem, StringComparison.OrdinalIgnoreCase))
                    {
                        sourceComponent.ChildrenSellableItemEntitiesList = UpdateHierarchy(arg, catalogItemBase.Id, sourceComponent.ChildrenSellableItemEntitiesList, sourceChanged);
                        catalogItemBaseComponent.ParentCatalogEntitiesList = UpdateHierarchy(arg, source.Id, catalogItemBaseComponent.ParentCatalogEntitiesList, changed);
                    }
                    else if (arg.RelationshipType.Equals(CategoryToSellableItem, StringComparison.OrdinalIgnoreCase))
                    {
                        sourceComponent.ChildrenSellableItemEntitiesList = UpdateHierarchy(arg, catalogItemBase.Id, sourceComponent.ChildrenSellableItemEntitiesList, sourceChanged);
                        catalogItemBaseComponent.ParentCategoryEntitiesList = UpdateHierarchy(arg, source.Id, catalogItemBaseComponent.ParentCategoryEntitiesList, changed);
                        catalogItemBaseComponent.ParentCatalogEntitiesList = UpdateHierarchy(arg, ExtractCatalogId(source.Id), catalogItemBaseComponent.ParentCatalogEntitiesList, changed);
                    }
                    if (changed.Value)
                    {
                        PersistEntityArgument persistEntityArgument = await _persistEntityPipeline.Run(new PersistEntityArgument(catalogItemBase), context);
                    }
                }
            }
            if (sourceChanged.Value)
            {
                await _persistEntityPipeline.Run(new PersistEntityArgument(source), context);
            }
            return arg;
        }

        private string ExtractCatalogId(string id)
        {
            string[] strArray = id.Split(new string[1] { "-" }, StringSplitOptions.RemoveEmptyEntries);

            return strArray.Length < 3 ? string.Empty : CommerceEntity.IdPrefix<Catalog>() + strArray[2];
        }

        private string UpdateHierarchy(
          RelationshipArgument arg,
          string targetId,
          string rawChildren,
          ValueWrapper<bool> changed)
        {
            if (rawChildren == null)
                rawChildren = string.Empty;
            List<string> list = ((IEnumerable<string>)rawChildren.Split(new char[1]
            {
                '|'
            }, StringSplitOptions.RemoveEmptyEntries)).ToList();

            if (arg.Mode.GetValueOrDefault() == RelationshipMode.Create & arg.Mode.HasValue && !list.Contains(targetId))
            {
                if (!changed.Value)
                {
                    changed.Value = true;
                }

                list.RemoveAll(c => c.Equals(targetId, StringComparison.OrdinalIgnoreCase));
                list.Add(targetId);
            }
            else
            {
                if (arg.Mode.GetValueOrDefault() == RelationshipMode.Delete & arg.Mode.HasValue && list.Contains(targetId))
                {
                    if (!changed.Value)
                    {
                        changed.Value = true;
                    }

                    list.RemoveAll(c => c.Equals(targetId, StringComparison.OrdinalIgnoreCase));
                }
            }
            return string.Join("|", list);
        }
    }
}
