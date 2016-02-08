using ProtoBuf;

namespace RuleSet.Conditions
{
    [ProtoContract(UseProtoMembersOnly = true)]
    public class ResearchCondition : Condition
    {
        [ProtoMember(1, AsReference = true)]
        public Research Research
        {
            get; set;
        }
    }
}
