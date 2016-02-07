using System;
using de.alivedevil.Attributes;
using System.Collections.Generic;
using RuleSet.Elements;

namespace RuleSet.Effects
{
    [Serializable]
    public class ExtendStorageEffect : Effect
    {
        public int Amount
        {
            get; set;
        }

        [KeepReference]
        public List<Resource> ResourceTypes { get; set; } = new List<Resource>();
    }
}
