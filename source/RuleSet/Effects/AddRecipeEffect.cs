using System.Collections.Generic;
using ProtoBuf;
using RuleSet.Elements;

namespace RuleSet.Effects
{
    [ProtoContract(UseProtoMembersOnly = true)]
    public class AddRecipeEffect : Effect
    {
        [ProtoMember(1, OverwriteList = false)]
        public List<ResourcePart> InResources { get; set; } = new List<ResourcePart>();

        [ProtoMember(2, OverwriteList = false)]
        public List<ResourcePart> OutResources { get; set; } = new List<ResourcePart>();
    }
}
