using ProtoBuf;
using RuleSet.Elements;

namespace RuleSet.Effects
{
    [ProtoContract(UseProtoMembersOnly = true)]
    public class GatherResourceEffect : Effect
    {
        [ProtoMember(1)]
        public int Radius
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
