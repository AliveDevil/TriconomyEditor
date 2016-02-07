using System;
using de.alivedevil.Attributes;
using ProtoBuf;

namespace RuleSet.Elements
{
    [Serializable]
    public class ResourcePart
    {
        public int Amount
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
