using ProtoBuf;

namespace RuleSet.EventActions
{
    [ProtoContract(UseProtoMembersOnly = true)]
    public class PlayAnimationEventAction : EventAction
    {
        [ProtoMember(1)]
        public NamedReference Animation
        {
            get; set;
        }
    }
}
