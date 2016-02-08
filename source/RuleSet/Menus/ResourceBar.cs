using System.Collections.Generic;
using ProtoBuf;
using RuleSet.Elements;

namespace RuleSet.Menus
{
    [ProtoContract(UseProtoMembersOnly = true)]
    public class ResourceBar
    {
        [ProtoMember(1, AsReference = true, OverwriteList = false)]
        public List<Resource> Resources { get; set; } = new List<Resource>();
    }
}
