using System.Collections.Generic;
using ProtoBuf;
using RuleSet.Elements;

namespace RuleSet
{
    [ProtoContract(UseProtoMembersOnly = true)]
    public class Research
    {
        [ProtoMember(1, OverwriteList = false)]
        public List<Condition> Conditions { get; } = new List<Condition>();

        [ProtoMember(2, OverwriteList = false)]
        public List<ResourcePart> Costs { get; } = new List<ResourcePart>();

        [ProtoMember(3)]
        public string Name
        {
            get; set;
        }

        [ProtoMember(4)]
        public int Time
        {
            get; set;
        }
    }
}
