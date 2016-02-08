using System.Collections.Generic;
using ProtoBuf;

namespace RuleSet.Menus
{
    [ProtoContract(UseProtoMembersOnly = true)]
    public class Toolbar
    {
        [ProtoMember(1, AsReference = true, OverwriteList = false)]
        public List<ToolbarItem> Items { get; } = new List<ToolbarItem>();

        [ProtoMember(2)]
        public string Name
        {
            get; set;
        }
    }
}
