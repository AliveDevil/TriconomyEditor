using System.Collections.Generic;
using ProtoBuf;
using RuleSet.Needs;

namespace RuleSet
{
    [ProtoContract(UseProtoMembersOnly = true)]
    [ProtoInclude(2, typeof(BuildingNeed))]
    [ProtoInclude(3, typeof(ResourceNeed))]
    public class Need
    {
        [ProtoMember(1, OverwriteList = false)]
        public List<Condition> Conditions { get; } = new List<Condition>();
    }
}
