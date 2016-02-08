using ProtoBuf;
using RuleSet.Elements;

namespace RuleSet.Needs
{
    [ProtoContract(UseProtoMembersOnly = true)]
    public class ResourceNeed : Need
    {
        [ProtoMember(1)]
        public int Amount
        {
            get; set;
        }

        [ProtoMember(2, AsReference = true)]
        public Resource Resource
        {
            get; set;
        }
    }
}
