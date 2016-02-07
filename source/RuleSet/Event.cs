using System.Collections.Generic;

namespace RuleSet
{
    public class Event
    {
        public List<EventAction> Actions
        {
            get;
        } = new List<EventAction>();
    }
}
