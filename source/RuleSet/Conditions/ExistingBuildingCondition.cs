using ProtoBuf;
using RuleSet.Elements;

namespace RuleSet.Conditions
{
    [ProtoContract(UseProtoMembersOnly = true)]
    public class ExistingBuildingCondition : Condition
    {
        [ProtoMember(1, AsReference = true)]
        public Building Building
        {
            get; set;
        }
    }
}
