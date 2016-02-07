using System;

namespace RuleSet.Events
{
    public class OneTimedEvent : Event
    {
        public EventFrequency Frequency
        {
            get; set;
        }

        public DateTime Time
        {
            get; set;
        }
    }
}
