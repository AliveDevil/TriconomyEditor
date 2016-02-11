using ProtoBuf;

namespace RuleSet
{
    [ProtoContract(UseProtoMembersOnly = true)]
    public class NamedReference
    {
        [ProtoMember(1)]
        public string Name
        {
            get; set;
        }
    }
}
