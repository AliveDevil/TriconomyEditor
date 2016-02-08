using ProtoBuf;
using RuleSet.Elements;

namespace RuleSet.Needs
{
    [ProtoContract(UseProtoMembersOnly = true)]
    public class BuildingNeed : Need
    {
        [ProtoMember(1, AsReference = true)]
        public Building Building
        {
            get; set;
        }

        [ProtoMember(2)]
        public int Radius
        {
            get; set;
        }
    }
}
