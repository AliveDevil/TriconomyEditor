using ProtoBuf;

namespace RuleSet.Elements
{
    [ProtoContract(UseProtoMembersOnly = true)]
    public class LivingResource : Element
    {
        [ProtoMember(1)]
        public int Amount
        {
            get; set;
        }

        [ProtoMember(2)]
        public bool AutoSpawn
        {
            get; set;
        }

        [ProtoMember(3, AsReference = true)]
        public Resource Resource
        {
            get; set;
        }
    }
}
