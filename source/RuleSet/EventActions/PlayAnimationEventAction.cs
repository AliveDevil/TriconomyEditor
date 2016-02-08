using ProtoBuf;

namespace RuleSet.EventActions
{
    [ProtoContract(UseProtoMembersOnly = true)]
    public class PlayAnimationEventAction : EventAction
    {
        [ProtoMember(1)]
        public string Animation
        {
            get; set;
        }
    }
}
