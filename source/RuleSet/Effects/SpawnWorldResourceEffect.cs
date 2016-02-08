using ProtoBuf;
using RuleSet.Elements;

namespace RuleSet.Effects
{
    [ProtoContract(UseProtoMembersOnly = true)]
    public class SpawnWorldResourceEffect : Effect
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

        [ProtoMember(3)]
        public int Radius
        {
            get; set;
        }

        [ProtoMember(4, AsReference = true)]
        public WorldResource WorldResource
        {
            get; set;
        }
    }
}
