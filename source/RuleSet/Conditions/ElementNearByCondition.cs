﻿using de.alivedevil.Attributes;
using System;

namespace RuleSet.Conditions
{
    [Serializable]
    public class ElementNearByCondition : Condition
    {
        public int Distance
        {
            get; set;
        }

        [KeepReference]
        public Element Element
        {
            get; set;
        }
    }
}
