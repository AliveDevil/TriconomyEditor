using System;
using System.Collections.Generic;
using de.alivedevil.Attributes;
using RuleSet.Elements;

namespace RuleSet.Menus
{
    [Serializable]
    public class ResourceBar : SerializedObject
    {
        [KeepReference]
        public List<Resource> Resources { get; set; } = new List<Resource>();
    }
}
