using System;

namespace RuleSet.Events
{
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
