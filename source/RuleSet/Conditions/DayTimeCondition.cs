using ProtoBuf;

namespace RuleSet.Conditions
{
    [ProtoContract(UseProtoMembersOnly = true)]
    public class DayTimeCondition : Condition
    {
        [ProtoMember(1)]
        public DayTime DayTime
        {
            get; set;
        }
    }
}
