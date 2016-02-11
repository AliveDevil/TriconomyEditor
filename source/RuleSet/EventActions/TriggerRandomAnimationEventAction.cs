using System.Collections.Generic;
using ProtoBuf;

namespace RuleSet.EventActions
{
    [ProtoContract(UseProtoMembersOnly = true)]
    public class TriggerRandomAnimationEventAction : EventAction
    {
        [ProtoMember(1, OverwriteList = false)]
        public List<NamedReference> Sources
        {
            get;
        } = new List<NamedReference>();
    }
}
