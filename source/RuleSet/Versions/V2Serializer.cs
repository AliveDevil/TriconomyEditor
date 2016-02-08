using ProtoBuf.Meta;
using RuleSet.Conditions;
using RuleSet.Effects;
using RuleSet.Elements;
using RuleSet.EventActions;
using RuleSet.Menus;
using RuleSet.Needs;

namespace RuleSet.Versions
{
    public class V2Serializer : VersionSerializer
    {
        public override int Version
        {
            get
            {
                return 2;
            }
        }

        protected override void OnModelCreation(TypeModel model)
        {
            var runtimeTypeModel = (RuntimeTypeModel)model;
            runtimeTypeModel.AutoAddMissingTypes = false;
            runtimeTypeModel.AutoAddProtoContractTypesOnly = false;
            runtimeTypeModel.AutoCompile = true;

            CreateRuleSet(runtimeTypeModel);

            CreateConditions(runtimeTypeModel);

            CreateEffects(runtimeTypeModel);

            CreateElements(runtimeTypeModel);

            CreateEvents(runtimeTypeModel);

            CreateToolbar(runtimeTypeModel);

            CreateNeeds(runtimeTypeModel);

            CreateResearch(runtimeTypeModel);

            CreateUpgrade(runtimeTypeModel);

            runtimeTypeModel.Freeze();
        }

        private static void CreateConditions(RuntimeTypeModel model)
        {
            model.Add(typeof(DateTimeCondition), false).With(c =>
            {
                c.AddField(1, "DateTime");
            });

            model.Add(typeof(DayTimeCondition), false).With(c =>
            {
            });

            model.Add(typeof(DelayCondition), false).With(c =>
            {
                c.AddField(1, "Delay");
            });

            model.Add(typeof(ElementNearByCondition), false).With(c =>
            {
                c.AddField(1, "Distance");
                c.AddField(2, "Element").With(e =>
                {
                    e.AsReference = true;
                });
            });

            model.Add(typeof(ExistingBuildingCondition), false).With(c =>
            {
                c.AddField(1, "Building").With(b =>
                {
                    b.AsReference = true;
                });
            });

            model.Add(typeof(LevelCondition), false).With(c =>
            {
                c.AddField(1, "Level");
            });

            model.Add(typeof(ResearchCondition), false).With(c =>
            {
                c.AddField(1, "Research").With(r =>
                {
                    r.AsReference = true;
                });
            });

            model.Add(typeof(TraverseCondition), false).With(c =>
            {
            });

            model.Add(typeof(TriggerCondition), false).With(c =>
            {
            });

            model.Add(typeof(WorkingCondition), false).With(c =>
            {
            });

            model.Add(typeof(Condition), false).With(c =>
            {
                c.AddSubType(typeof(DateTimeCondition));
                c.AddSubType(typeof(DayTimeCondition));
                c.AddSubType(typeof(DelayCondition));
                c.AddSubType(typeof(ElementNearByCondition));
                c.AddSubType(typeof(ExistingBuildingCondition));
                c.AddSubType(typeof(LevelCondition));
                c.AddSubType(typeof(ResearchCondition));
                c.AddSubType(typeof(TraverseCondition));
                c.AddSubType(typeof(TriggerCondition));
                c.AddSubType(typeof(WorkingCondition));
            });
        }

        private static void CreateEffects(RuntimeTypeModel model)
        {
            model.Add(typeof(AddRecipeEffect), false).With(e =>
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

            model.Add(typeof(AssemblyPointEffect), false).With(e =>
            {
            });

            model.Add(typeof(DeliverEffect), false).With(e =>
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

            model.Add(typeof(ExtendSettlerAmountEffect), false).With(e =>
            {
                e.AddField(1, "SettlerAmount");
            });

            model.Add(typeof(ExtendStorageEffect), false).With(e =>
            {
                e.AddField(1, "Amount");
            });

            model.Add(typeof(GatherResourceEffect), false).With(e =>
            {
                e.AddField(1, "Radius");
                e.AddField(2, "Resource").With(r =>
                {
                    r.AsReference = true;
                });
            });

            model.Add(typeof(HabitEffect), false).With(e =>
            {
            });

            model.Add(typeof(IncreaseProductivityEffect), false).With(e =>
            {
            });

            model.Add(typeof(ProduceResourceEffect), false).With(e =>
            {
                e.AddField(1, "Amount");
                e.AddField(2, "CheatModeOnly");
                e.AddField(3, "Delay");
                e.AddField(4, "Resource").With(r =>
                {
                    r.AsReference = true;
                });
            });

            model.Add(typeof(ResearchEffect), false).With(e =>
            {
            });

            model.Add(typeof(SpawnLivingResourceEffect), false).With(e =>
            {
                e.AddField(1, "Amount");
                e.AddField(2, "Delay");
                e.AddField(3, "LivingResource").With(r =>
                {
                    r.AsReference = true;
                });
                e.AddField(4, "Radius");
            });

            model.Add(typeof(SpawnWorldResourceEffect), false).With(e =>
            {
                e.AddField(1, "Amount");
                e.AddField(2, "Delay");
                e.AddField(3, "Radius");
                e.AddField(4, "WorldResource").With(r =>
                {
                    r.AsReference = true;
                });
            });

            model.Add(typeof(StorageEffect), false).With(e =>
            {
                e.AddField(1, "PublicAccessible");
            });

            model.Add(typeof(UseResourceEffect), false).With(e =>
            {
                e.AddField(1, "Amount");
                e.AddField(2, "Resource").With(r =>
                {
                    r.AsReference = true;
                });
            });

            model.Add(typeof(WorkplaceEffect), false).With(e =>
            {
                e.AddField(1, "Job").With(j =>
                {
                    j.AsReference = true;
                });
            });

            model.Add(typeof(Effect), false).With(e =>
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
        }

        private static void CreateElements(RuntimeTypeModel model)
        {
            model.Add(typeof(Building), false).With(e =>
            {
                e.AddField(1, "Events").With(v =>
                {
                    v.AsReference = false;
                    v.OverwriteList = false;
                });
                e.AddField(2, "Upgrades").With(u =>
                {
                    u.AsReference = false;
                    u.OverwriteList = false;
                });
                e.AddField(3, "Variants");
            });

            model.Add(typeof(Job), false).With(e =>
            {
            });

            model.Add(typeof(LivingResource), false).With(e =>
            {
                e.AddField(1, "Amount");
                e.AddField(2, "AutoSpawn");
                e.AddField(3, "Resource").With(r =>
                {
                    r.AsReference = true;
                });
            });

            model.Add(typeof(ResourceGroup), false).With(e =>
            {
                e.AddField(1, "Resources").With(r =>
                {
                    r.AsReference = true;
                    r.OverwriteList = false;
                });
            });

            model.Add(typeof(Resource), false).With(e =>
            {
                e.AddField(1, "StackSize");

                e.AddSubType(typeof(ResourceGroup));
            });

            model.Add(typeof(WorldResource), false).With(e =>
            {
                e.AddField(1, "Amount");
                e.AddField(2, "AutoSpawn");
                e.AddField(3, "Resource").With(r =>
                {
                    r.AsReference = true;
                });
                e.AddField(4, "Variants");
            });

            model.Add(typeof(Element), false).With(e =>
            {
                e.AddField(1, "Name");

                e.AddSubType(typeof(Building));
                e.AddSubType(typeof(Job));
                e.AddSubType(typeof(LivingResource));
                e.AddSubType(typeof(Resource));
                e.AddSubType(typeof(WorldResource));
            });

            model.Add(typeof(ResourcePart), false).With(e =>
            {
                e.AddField(1, "Amount");
                e.AddField(2, "Resource").With(r =>
                {
                    r.AsReference = true;
                });
            });
        }

        private static void CreateEvents(RuntimeTypeModel model)
        {
            model.Add(typeof(Event), false).With(e =>
            {
                e.AddField(1, "Actions").With(a =>
                {
                    a.AsReference = false;
                    a.OverwriteList = false;
                });
                e.AddField(2, "Frequency");
                e.AddField(3, "Reset").With(a =>
                {
                    a.AsReference = false;
                    a.OverwriteList = false;
                });
                e.AddField(4, "Set").With(a =>
                {
                    a.AsReference = false;
                    a.OverwriteList = false;
                });
            });

            model.Add(typeof(PlayAnimationEventAction), false).With(a =>
            {
                a.AddField(1, "Animation");
            });

            model.Add(typeof(PlaySoundEventAction), false).With(a =>
            {
                a.AddField(1, "Source");
            });

            model.Add(typeof(TriggerRandomAnimationEventAction), false).With(a =>
            {
                a.AddField(1, "Sources").With(s =>
                {
                    s.AsReference = false;
                    s.OverwriteList = false;
                });
            });

            model.Add(typeof(TriggerRandomSoundEventAction), false).With(a =>
            {
                a.AddField(1, "Sources").With(s =>
                {
                    s.AsReference = false;
                    s.OverwriteList = false;
                });
            });

            model.Add(typeof(EventAction), false).With(a =>
            {
                a.AddSubType(typeof(PlayAnimationEventAction));
                a.AddSubType(typeof(PlaySoundEventAction));
                a.AddSubType(typeof(TriggerRandomAnimationEventAction));
                a.AddSubType(typeof(TriggerRandomSoundEventAction));
            });
        }

        private static void CreateNeeds(RuntimeTypeModel model)
        {
            model.Add(typeof(BuildingNeed), false).With(n =>
            {
                n.AddField(1, "Building").With(b =>
                {
                    b.AsReference = true;
                });
                n.AddField(2, "Radius");
            });

            model.Add(typeof(ResourceNeed), false).With(n =>
            {
                n.AddField(1, "Amount");
                n.AddField(2, "Resource").With(r =>
                {
                    r.AsReference = true;
                });
            });

            model.Add(typeof(Need), false).With(n =>
            {
                n.AddField(1, "Conditions").With(c =>
                {
                    c.AsReference = false;
                    c.OverwriteList = false;
                });

                n.AddSubType(typeof(BuildingNeed));
                n.AddSubType(typeof(ResourceNeed));
            });
        }

        private static void CreateResearch(RuntimeTypeModel model)
        {
            model.Add(typeof(Research), false).With(r =>
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
        }

        private static void CreateRuleSet(RuntimeTypeModel model)
        {
            model.Add(typeof(RuleSet), false).With(m =>
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
        }

        private static void CreateToolbar(RuntimeTypeModel model)
        {
            model.Add(typeof(Toolbar), false).With(b =>
            {
                b.AddField(1, "Items").With(i =>
                {
                    i.AsReference = false;
                    i.OverwriteList = false;
                });
                b.AddField(2, "Name");
            });

            model.Add(typeof(ResourceBar), false).With(b =>
            {
                b.AddField(1, "Resources").With(r =>
                {
                    r.AsReference = true;
                    r.OverwriteList = false;
                });
            });

            model.Add(typeof(OpenToolbarItem), false).With(i =>
            {
                i.AddField(1, "Toolbar");
            });

            model.Add(typeof(PlaceBuildingItem), false).With(i =>
            {
                i.AddField(1, "Building").With(b =>
                {
                    b.AsReference = true;
                });
            });

            model.Add(typeof(ToolbarItem), false).With(i =>
            {
                i.AddSubType(typeof(OpenToolbarItem));
                i.AddSubType(typeof(PlaceBuildingItem));
            });
        }

        private static void CreateUpgrade(RuntimeTypeModel model)
        {
            model.Add(typeof(Upgrade), false).With(u =>
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
        }
    }
}
