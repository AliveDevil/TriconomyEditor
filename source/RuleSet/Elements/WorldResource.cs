using ProtoBuf;

namespace RuleSet.Elements
{
    [ProtoContract(UseProtoMembersOnly = true)]
    public class WorldResource : Element
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

        [ProtoMember(3, AsReference = true, OverwriteList = false)]
        public Resource Resource
        {
            get; set;
        }

        [ProtoMember(4)]
        public int Variants { get; set; } = 0;
    }
}
