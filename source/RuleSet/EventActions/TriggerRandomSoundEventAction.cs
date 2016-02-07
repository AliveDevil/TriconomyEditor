using System.Collections.Generic;

namespace RuleSet.EventActions
{
    public class TriggerRandomSoundEventAction : EventAction
    {
        public List<string> Sources
        {
            get;
        } = new List<string>();
    }
}
