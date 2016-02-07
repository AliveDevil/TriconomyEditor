using System;
using de.alivedevil.Attributes;

namespace RuleSet.Elements
{
    [Serializable]
    public class WorldResource : Element
    {
        public int Amount
        {
            get; set;
        }

        public bool AutoSpawn
        {
            get; set;
        }

        [KeepReference]
        public Resource Resource
        {
            get; set;
        }

        public int Variants { get; set; } = 0;
    }
}
