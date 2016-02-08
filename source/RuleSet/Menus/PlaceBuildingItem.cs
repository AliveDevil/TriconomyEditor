using ProtoBuf;
using RuleSet.Elements;

namespace RuleSet.Menus
{
    [ProtoContract(UseProtoMembersOnly = true)]
    public class PlaceBuildingItem : ToolbarItem
    {
        [ProtoMember(1, AsReference = true)]
        public Building Building
        {
            get; set;
        }
    }
}
