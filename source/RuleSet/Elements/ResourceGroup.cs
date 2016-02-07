using System;
using System.Collections.Generic;
using de.alivedevil.Attributes;
using ProtoBuf;

namespace RuleSet.Elements
{
    [Serializable]
    public class ResourceGroup : Resource
    {
        [KeepReference]
        public List<Resource> Resources { get; } = new List<Resource>();
    }
}
