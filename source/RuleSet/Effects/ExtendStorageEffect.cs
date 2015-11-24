using System.Collections.Generic;
using RuleSet.Elements;

namespace RuleSet.Effects
{
    public class ExtendStorageEffect : Effect
    {
        public int Amount { get; set; }

        public List<Resource> ResourceTypes { get; set; } = new List<Resource>();
    }
}
