using ProtoBuf;
using RuleSet.Effects;

namespace RuleSet
{
    [ProtoContract(UseProtoMembersOnly = true)]
    [ProtoInclude(1, typeof(AddRecipeEffect))]
    [ProtoInclude(2, typeof(AssemblyPointEffect))]
    [ProtoInclude(3, typeof(DeliverEffect))]
    [ProtoInclude(4, typeof(ExtendSettlerAmountEffect))]
    [ProtoInclude(5, typeof(ExtendStorageEffect))]
    [ProtoInclude(6, typeof(GatherResourceEffect))]
    [ProtoInclude(7, typeof(HabitEffect))]
    [ProtoInclude(8, typeof(IncreaseProductivityEffect))]
    [ProtoInclude(9, typeof(ProduceResourceEffect))]
    [ProtoInclude(10, typeof(ResearchEffect))]
    [ProtoInclude(11, typeof(SpawnLivingResourceEffect))]
    [ProtoInclude(12, typeof(SpawnWorldResourceEffect))]
    [ProtoInclude(13, typeof(StorageEffect))]
    [ProtoInclude(14, typeof(UseResourceEffect))]
    [ProtoInclude(15, typeof(WorkplaceEffect))]
    public class Effect
    {
    }
}
