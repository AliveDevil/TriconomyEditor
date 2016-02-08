using ProtoBuf;
using RuleSet.Elements;

namespace RuleSet.Effects
{
    [ProtoContract(UseProtoMembersOnly = true)]
    public class ProduceResourceEffect : Effect
    {
        [ProtoMember(1)]
        public int Amount
        {
            get; set;
        }

        [ProtoMember(2)]
        public bool CheatModeOnly
        {
            get; set;
        }

        [ProtoMember(3)]
        public int Delay
        {
            get; set;
        }

        [ProtoMember(4, AsReference = true)]
        public Resource Resource
        {
            get; set;
        }
    }
}
