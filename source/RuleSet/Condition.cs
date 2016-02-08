using ProtoBuf;
using RuleSet.Conditions;

namespace RuleSet
{
    [ProtoContract(UseProtoMembersOnly = false)]
    [ProtoInclude(1, typeof(DateTimeCondition))]
    [ProtoInclude(2, typeof(DayTimeCondition))]
    [ProtoInclude(3, typeof(DelayCondition))]
    [ProtoInclude(4, typeof(ElementNearByCondition))]
    [ProtoInclude(5, typeof(ExistingBuildingCondition))]
    [ProtoInclude(6, typeof(LevelCondition))]
    [ProtoInclude(7, typeof(ResearchCondition))]
    [ProtoInclude(8, typeof(TraverseCondition))]
    [ProtoInclude(9, typeof(TriggerCondition))]
    [ProtoInclude(10, typeof(WorkingCondition))]
    public class Condition
    {
    }
}
