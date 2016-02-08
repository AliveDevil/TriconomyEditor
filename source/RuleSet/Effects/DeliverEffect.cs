using ProtoBuf;
using RuleSet.Elements;

namespace RuleSet.Effects
{
    [ProtoContract(UseProtoMembersOnly = true)]
    public class DeliverEffect : Effect
    {
        [ProtoMember(1, AsReference = true)]
        public Building PreferredBuilding
        {
            get; set;
        }

        [ProtoMember(2)]
        public int Priority
        {
            get; set;
        }

        [ProtoMember(3)]
        public Resource Resource
        {
            get; set;
        }
    }
}
