using System.Collections.Generic;
using RuleSet.Elements;

namespace RuleSet.Menus
{
    public class ResourceBar : SerializedObject
    {
        public List<Resource> Resources { get; set; } = new List<Resource>();
    }
}
