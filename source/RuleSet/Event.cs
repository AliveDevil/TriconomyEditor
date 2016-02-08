using System.Collections.Generic;
using ProtoBuf;

namespace RuleSet
{
    [ProtoContract(UseProtoMembersOnly = true)]
    public class Event
    {
        [ProtoMember(1, OverwriteList = false)]
        public List<EventAction> Actions
        {
            get;
        } = new List<EventAction>();

        [ProtoMember(2)]
        public EventFrequency Frequency
        {
            get; set;
        }

        [ProtoMember(3, OverwriteList = false)]
        public List<Condition> Reset
        {
            get;
        } = new List<Condition>();

        [ProtoMember(4, OverwriteList = false)]
        public List<Condition> Set
        {
            get;
        } = new List<Condition>();
    }
}
