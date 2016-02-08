using ProtoBuf;

namespace RuleSet.Conditions
{
    [ProtoContract(UseProtoMembersOnly = true)]
    public class ElementNearByCondition : Condition
    {
        [ProtoMember(1)]
        public int Distance
        {
            get; set;
        }

        [ProtoMember(2, AsReference = true)]
        public Element Element
        {
            get; set;
        }
    }
}
