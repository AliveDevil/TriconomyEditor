using System.Collections.Generic;
using RuleSet.Elements;

namespace RuleSet.Effects
{
    public class AddRecipeEffect : Effect
    {
        public List<ResourcePart> InResources { get; set; } = new List<ResourcePart>();

        public List<ResourcePart> OutResources { get; set; } = new List<ResourcePart>();
    }
}
