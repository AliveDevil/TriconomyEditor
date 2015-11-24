using System.Collections.Generic;
using RuleSet.Elements;

namespace RuleSet.Effects
{
    public class AddRecipeEffect : Effect
    {
        public List<RecipePart> InResources { get; set; } = new List<RecipePart>();

        public List<RecipePart> OutResources { get; set; } = new List<RecipePart>();
    }
}
