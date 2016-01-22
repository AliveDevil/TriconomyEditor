using System.Collections.Generic;
using de.alivedevil.Attributes;

namespace RuleSet.Elements
{
    public class ResourceGroup : Resource
    {
        [KeepReference]
        public List<Resource> Resources { get; set; } = new List<Resource>();
    }
}
