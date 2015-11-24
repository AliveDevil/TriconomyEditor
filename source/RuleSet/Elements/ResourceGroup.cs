using System.Collections.Generic;

namespace RuleSet.Elements
{
    public class ResourceGroup : Resource
    {
        public List<Resource> Resources { get; set; } = new List<Resource>();
    }
}
