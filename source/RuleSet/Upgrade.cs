using System.Collections.Generic;
using ProtoBuf;
using RuleSet.Elements;

namespace RuleSet
{
    [ProtoContract(UseProtoMembersOnly = true)]
    public class Upgrade
    {
        [ProtoMember(1, OverwriteList = false)]
        public List<Condition> Conditions { get; } = new List<Condition>();

        [ProtoMember(2, OverwriteList = false)]
        public List<ResourcePart> Costs { get; } = new List<ResourcePart>();

        [ProtoMember(3, OverwriteList = false)]
        public List<Effect> Effects { get; } = new List<Effect>();

        [ProtoMember(4)]
        public int Level
        {
            get; set;
        }
    }
}
