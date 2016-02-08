using System.Collections.Generic;
using ProtoBuf;

namespace RuleSet.Elements
{
    [ProtoContract(UseProtoMembersOnly = true)]
    public class ResourceGroup : Resource
    {
        [ProtoMember(1, AsReference = true, OverwriteList = false)]
        public List<Resource> Resources { get; } = new List<Resource>();
    }
}
