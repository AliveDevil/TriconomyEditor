using System;
using de.alivedevil.Attributes;

namespace RuleSet.Elements
{
    [Serializable]
    public class ResourcePart : SerializedObject
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
