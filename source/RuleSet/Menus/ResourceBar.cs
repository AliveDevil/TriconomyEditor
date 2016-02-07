using System;
using System.Collections.Generic;
using de.alivedevil.Attributes;
using ProtoBuf;
using RuleSet.Elements;

namespace RuleSet.Menus
{
    [Serializable]
    public class ResourceBar
    {
        [KeepReference]
        public List<Resource> Resources { get; set; } = new List<Resource>();
    }
}
