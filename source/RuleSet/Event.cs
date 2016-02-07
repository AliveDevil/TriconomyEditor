using System.Collections.Generic;

namespace RuleSet
{
    public class Event
    {
        public List<EventAction> Actions
        {
            get;
        } = new List<EventAction>();

        public EventFrequency Frequency
        {
            get; set;
        }

        public List<Condition> Reset
        {
            get;
        } = new List<Condition>();

        public List<Condition> Set
        {
            get;
        } = new List<Condition>();
    }
}
