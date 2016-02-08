using ProtoBuf;
using RuleSet.EventActions;

namespace RuleSet
{
    [ProtoContract(UseProtoMembersOnly = true)]
    [ProtoInclude(1, typeof(PlayAnimationEventAction))]
    [ProtoInclude(2, typeof(PlaySoundEventAction))]
    [ProtoInclude(3, typeof(TriggerRandomAnimationEventAction))]
    [ProtoInclude(4, typeof(TriggerRandomSoundEventAction))]
    public class EventAction
    {
    }
}
