using ProtoBuf;
using RuleSet.Elements;

namespace RuleSet.Effects
{
    [ProtoContract(UseProtoMembersOnly = true)]
    public class UseResourceEffect : Effect
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
