using ProtoBuf;
using RuleSet.Elements;

namespace RuleSet.Effects
{
    [ProtoContract(UseProtoMembersOnly = true)]
    public class SpawnLivingResourceEffect : Effect
    {
        [ProtoMember(1)]
        public int Amount
        {
            get; set;
        }

        [ProtoMember(2)]
        public int Delay
        {
            get; set;
        }

        [ProtoMember(3, AsReference = true)]
        public LivingResource LivingResource
        {
            get; set;
        }

        [ProtoMember(4)]
        public int Radius
        {
            get; set;
        }
    }
}
