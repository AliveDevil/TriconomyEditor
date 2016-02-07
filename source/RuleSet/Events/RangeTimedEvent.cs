using System;
using ProtoBuf;

namespace RuleSet.Events
{
    [Serializable]
    public class RangeTimedEvent : Event
    {
        public DateTime End
        {
            get; set;
        }

        public EventFrequency Frequency
        {
            get; set;
        }

        public DateTime Start
        {
            get; set;
        }
    }
}
