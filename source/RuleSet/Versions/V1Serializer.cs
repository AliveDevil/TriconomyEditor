﻿using ProtoBuf.Meta;
using RuleSet.Conditions;
using RuleSet.Effects;
using RuleSet.Elements;
using RuleSet.Menus;
using RuleSet.Needs;

namespace RuleSet.Versions
{
    public class V1Serializer : VersionSerializer
    {
        public override int Version
        {
            get
            {
                return 1;
            }
        }

        protected override void OnModelCreation(TypeModel model)
        {
            var runtimeTypeModel = (RuntimeTypeModel)model;
            runtimeTypeModel.Add(typeof(RuleSet), false).With(m =>
            {
                m.AddField(1, "Elements").With(e =>
                {
                    e.AsReference = true;
                    e.OverwriteList = false;
                });
                m.AddField(2, "Name");
                m.AddField(3, "Needs").With(n =>
                {
                    n.AsReference = false;
                    n.OverwriteList = false;
                });
                m.AddField(4, "Research").With(r =>
                {
                    r.AsReference = false;
                    r.OverwriteList = false;
                });
                m.AddField(5, "ResourceBar");
                m.AddField(6, "StartResources").With(s =>
                {
                    s.AsReference = false;
                    s.OverwriteList = false;
                });
                m.AddField(7, "Toolbar");
            });

            runtimeTypeModel.Add(typeof(Condition), false).With(c =>
            {
                c.AddSubType(typeof(ElementNearByCondition));
                c.AddSubType(typeof(ExistingBuildingCondition));
                c.AddSubType(typeof(ResearchCondition));
            });

            runtimeTypeModel.Add(typeof(ElementNearByCondition), false).With(c =>
            {
                c.AddField(1, "Distance");
                c.AddField(2, "Element").With(e =>
                {
                    e.AsReference = true;
                });
            });

            runtimeTypeModel.Add(typeof(ExistingBuildingCondition), false).With(c =>
            {
                c.AddField(1, "Building").With(b =>
                {
                    b.AsReference = true;
                });
            });

            runtimeTypeModel.Add(typeof(ResearchCondition), false).With(c =>
            {
                c.AddField(1, "Research").With(r =>
                {
                    r.AsReference = true;
                });
            });

            runtimeTypeModel.Add(typeof(Effect), false).With(e =>
            {
                e.AddSubType(typeof(AddRecipeEffect));
                e.AddSubType(typeof(AssemblyPointEffect));
                e.AddSubType(typeof(DeliverEffect));
                e.AddSubType(typeof(ExtendSettlerAmountEffect));
                e.AddSubType(typeof(ExtendStorageEffect));
                e.AddSubType(typeof(GatherResourceEffect));
                e.AddSubType(typeof(HabitEffect));
                e.AddSubType(typeof(IncreaseProductivityEffect));
                e.AddSubType(typeof(ProduceResourceEffect));
                e.AddSubType(typeof(ResearchEffect));
                e.AddSubType(typeof(SpawnLivingResourceEffect));
                e.AddSubType(typeof(SpawnWorldResourceEffect));
                e.AddSubType(typeof(StorageEffect));
                e.AddSubType(typeof(UseResourceEffect));
                e.AddSubType(typeof(WorkplaceEffect));
            });

            runtimeTypeModel.Add(typeof(AddRecipeEffect), false).With(e =>
            {
                e.AddField(1, "InResources").With(r =>
                {
                    r.AsReference = false;
                    r.OverwriteList = false;
                });
                e.AddField(2, "OutResources").With(r =>
                {
                    r.AsReference = false;
                    r.OverwriteList = false;
                });
            });

            runtimeTypeModel.Add(typeof(AssemblyPointEffect), false).With(e =>
            {
            });

            runtimeTypeModel.Add(typeof(DeliverEffect), false).With(e =>
            {
                e.AddField(1, "PreferredBuilding").With(b =>
                {
                    b.AsReference = true;
                });
                e.AddField(2, "Priority");
                e.AddField(3, "Resource").With(r =>
                {
                    r.AsReference = true;
                });
            });

            runtimeTypeModel.Add(typeof(ExtendSettlerAmountEffect), false).With(e =>
            {
                e.AddField(1, "SettlerAmount");
            });

            runtimeTypeModel.Add(typeof(ExtendStorageEffect), false).With(e =>
            {
                e.AddField(1, "Amount");
            });

            runtimeTypeModel.Add(typeof(GatherResourceEffect), false).With(e =>
            {
                e.AddField(1, "Radius");
                e.AddField(2, "Resource").With(r =>
                {
                    r.AsReference = true;
                });
            });

            runtimeTypeModel.Add(typeof(HabitEffect), false).With(e =>
            {
            });

            runtimeTypeModel.Add(typeof(IncreaseProductivityEffect), false).With(e =>
            {
            });

            runtimeTypeModel.Add(typeof(ProduceResourceEffect), false).With(e =>
            {
                e.AddField(1, "Amount");
                e.AddField(2, "CheatModeOnly");
                e.AddField(3, "Delay");
                e.AddField(4, "Resource").With(r =>
                {
                    r.AsReference = true;
                });
            });

            runtimeTypeModel.Add(typeof(ResearchEffect), false).With(e =>
            {
            });

            runtimeTypeModel.Add(typeof(SpawnLivingResourceEffect), false).With(e =>
            {
                e.AddField(1, "Amount");
                e.AddField(2, "Delay");
                e.AddField(3, "LivingResource").With(r =>
                {
                    r.AsReference = true;
                });
                e.AddField(4, "Radius");
            });

            runtimeTypeModel.Add(typeof(SpawnWorldResourceEffect), false).With(e =>
            {
                e.AddField(1, "Amount");
                e.AddField(2, "Delay");
                e.AddField(3, "Radius");
                e.AddField(4, "WorldResource").With(r =>
                {
                    r.AsReference = true;
                });
            });

            runtimeTypeModel.Add(typeof(StorageEffect), false).With(e =>
            {
                e.AddField(1, "PublicAccessible");
            });

            runtimeTypeModel.Add(typeof(UseResourceEffect), false).With(e =>
            {
                e.AddField(1, "Amount");
                e.AddField(2, "Resource").With(r =>
                {
                    r.AsReference = true;
                });
            });

            runtimeTypeModel.Add(typeof(WorkplaceEffect), false).With(e =>
            {
                e.AddField(1, "Job").With(j =>
                {
                    j.AsReference = true;
                });
            });

            runtimeTypeModel.Add(typeof(Element), false).With(e =>
            {
                e.AddField(1, "Name");

                e.AddSubType(typeof(Building));
                e.AddSubType(typeof(Job));
                e.AddSubType(typeof(LivingResource));
                e.AddSubType(typeof(Resource));
                e.AddSubType(typeof(WorldResource));
            });

            runtimeTypeModel.Add(typeof(Building), false).With(e =>
            {
                e.AddField(1, "Upgrades").With(u =>
                {
                    u.AsReference = false;
                    u.OverwriteList = false;
                });
                e.AddField(2, "Variants");
            });

            runtimeTypeModel.Add(typeof(Job), false).With(e =>
            {
            });

            runtimeTypeModel.Add(typeof(LivingResource), false).With(e =>
            {
                e.AddField(1, "Amount");
                e.AddField(2, "AutoSpawn");
                e.AddField(3, "Resource").With(r =>
                {
                    r.AsReference = true;
                });
            });

            runtimeTypeModel.Add(typeof(Resource), false).With(e =>
            {
                e.AddField(1, "StackSize");

                e.AddSubType(typeof(ResourceGroup));
            });

            runtimeTypeModel.Add(typeof(ResourceGroup), false).With(e =>
            {
                e.AddField(1, "Resources").With(r =>
                {
                    r.AsReference = true;
                    r.OverwriteList = false;
                });
            });

            runtimeTypeModel.Add(typeof(WorldResource), false).With(e =>
            {
                e.AddField(1, "Amount");
                e.AddField(2, "AutoSpawn");
                e.AddField(3, "Resource").With(r =>
                {
                    r.AsReference = true;
                });
                e.AddField(4, "Variants");
            });

            runtimeTypeModel.Add(typeof(ResourcePart), false).With(e =>
            {
                e.AddField(1, "Amount");
                e.AddField(2, "Resource").With(r =>
                {
                    r.AsReference = true;
                });
            });

            runtimeTypeModel.Add(typeof(Toolbar), false).With(b =>
            {
                b.AddField(1, "Items").With(i =>
                {
                    i.AsReference = false;
                    i.OverwriteList = false;
                });
                b.AddField(2, "Name");
            });

            runtimeTypeModel.Add(typeof(ResourceBar), false).With(b =>
            {
                b.AddField(1, "Resources").With(r =>
                {
                    r.AsReference = true;
                    r.OverwriteList = false;
                });
            });

            runtimeTypeModel.Add(typeof(ToolbarItem), false).With(i =>
            {
                i.AddSubType(typeof(OpenToolbarItem));
                i.AddSubType(typeof(PlaceBuildingItem));
            });

            runtimeTypeModel.Add(typeof(OpenToolbarItem), false).With(i =>
            {
                i.AddField(1, "Toolbar");
            });

            runtimeTypeModel.Add(typeof(PlaceBuildingItem), false).With(i =>
            {
                i.AddField(1, "Building").With(b =>
                {
                    b.AsReference = true;
                });
            });

            runtimeTypeModel.Add(typeof(Need), false).With(n =>
            {
                n.AddField(1, "Conditions").With(c =>
                {
                    c.AsReference = false;
                    c.OverwriteList = false;
                });

                n.AddSubType(typeof(BuildingNeed));
                n.AddSubType(typeof(ResourceNeed));
            });

            runtimeTypeModel.Add(typeof(BuildingNeed), false).With(n =>
            {
                n.AddField(1, "Building").With(b =>
                {
                    b.AsReference = true;
                });
                n.AddField(2, "Radius");
            });

            runtimeTypeModel.Add(typeof(ResourceNeed), false).With(n =>
            {
                n.AddField(1, "Amount");
                n.AddField(2, "Resource").With(r =>
                {
                    r.AsReference = true;
                });
            });

            runtimeTypeModel.Add(typeof(Research), false).With(r =>
            {
                r.AddField(1, "Conditions").With(c =>
                {
                    c.AsReference = false;
                    c.OverwriteList = false;
                });
                r.AddField(2, "Costs").With(c =>
                {
                    c.AsReference = false;
                    c.OverwriteList = false;
                });
                r.AddField(3, "Name");
                r.AddField(4, "Time");
            });

            runtimeTypeModel.Add(typeof(Upgrade), false).With(u =>
            {
                u.AddField(1, "Conditions").With(c =>
                {
                    c.AsReference = false;
                    c.OverwriteList = false;
                });
                u.AddField(2, "Costs").With(c =>
                {
                    c.AsReference = false;
                    c.OverwriteList = false;
                });
                u.AddField(3, "Effects").With(e =>
                {
                    e.AsReference = false;
                    e.OverwriteList = false;
                });
                u.AddField(4, "Level");
            });

            runtimeTypeModel.Freeze();
        }
    }
}
