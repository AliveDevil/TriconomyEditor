using System;
using de.alivedevil.Attributes;
using System.Collections.Generic;
using RuleSet.Elements;
using ProtoBuf;

namespace RuleSet.Effects
{
    [Serializable]
    public class ExtendStorageEffect : Effect
    {
        public int Amount
        {
            get; set;
        }
    }
}
