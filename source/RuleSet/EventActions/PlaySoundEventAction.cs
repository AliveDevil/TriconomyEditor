using ProtoBuf;

namespace RuleSet.EventActions
{
    [ProtoContract(UseProtoMembersOnly = true)]
    public class PlaySoundEventAction : EventAction
    {
        [ProtoMember(1)]
        public string Source
        {
            get; set;
        }
    }
}
