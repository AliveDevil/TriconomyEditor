﻿using de.alivedevil.Attributes;
using RuleSet.Elements;

namespace RuleSet.Effects
{
    public class GatherResourceEffect : Effect
    {
        public int Radius { get; set; }

        [KeepReference]
        public Resource Resource { get; set; }
    }
}
