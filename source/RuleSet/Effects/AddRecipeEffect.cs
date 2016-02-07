using System;
using System.Collections.Generic;
using ProtoBuf;
using RuleSet.Elements;

namespace RuleSet.Effects
{
    [Serializable]
    public class AddRecipeEffect : Effect
    {
        public List<ResourcePart> InResources { get; set; } = new List<ResourcePart>();

        public List<ResourcePart> OutResources { get; set; } = new List<ResourcePart>();
    }
}
