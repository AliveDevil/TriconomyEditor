using System;
using de.alivedevil.Attributes;
using RuleSet.Elements;

namespace RuleSet.Effects
{
    [Serializable]
    public class WorkplaceEffect : Effect
    {
        [KeepReference]
        public Job Job
        {
            get; set;
        }
    }
}
