﻿using System;
using de.alivedevil.Attributes;

namespace RuleSet.Elements
{
    [Serializable]
    public class LivingResource : Element
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
    }
}
