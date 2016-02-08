using ProtoBuf;

namespace RuleSet.Elements
{
    [ProtoContract(UseProtoMembersOnly = true)]
    [ProtoInclude(2, typeof(ResourceGroup))]
    public class Resource : Element
    {
        [ProtoMember(1)]
        public int StackSize
        {
            get; set;
        }
    }
}
