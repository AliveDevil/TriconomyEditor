using ProtoBuf;

namespace RuleSet.Menus
{
    [ProtoContract(UseProtoMembersOnly = true)]
    [ProtoInclude(1, typeof(OpenToolbarItem))]
    [ProtoInclude(2, typeof(PlaceBuildingItem))]
    public class ToolbarItem
    {
    }
}
