using System.Collections.Generic;
using de.alivedevil.Attributes;
using RuleSet.Elements;

namespace RuleSet.Effects
{
    public class ExtendStorageEffect : Effect
    {
        public int Amount { get; set; }

        [KeepReference]
        public List<Resource> ResourceTypes { get; set; } = new List<Resource>();
    }
}
