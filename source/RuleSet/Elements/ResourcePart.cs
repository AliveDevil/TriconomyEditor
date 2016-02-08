using ProtoBuf;

namespace RuleSet.Elements
{
    [ProtoContract(UseProtoMembersOnly = true)]
    public class ResourcePart
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
