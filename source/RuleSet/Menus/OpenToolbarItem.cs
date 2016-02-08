using ProtoBuf;

namespace RuleSet.Menus
{
    [ProtoContract(UseProtoMembersOnly = true)]
    public class OpenToolbarItem : ToolbarItem
    {
        [ProtoMember(1)]
        public Toolbar Toolbar
        {
            get; set;
        }
    }
}
