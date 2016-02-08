using ProtoBuf;
using RuleSet.Elements;

namespace RuleSet.Effects
{
    [ProtoContract(UseProtoMembersOnly = true)]
    public class WorkplaceEffect : Effect
    {
        [ProtoMember(1, AsReference = true)]
        public Job Job
        {
            get; set;
        }
    }
}
