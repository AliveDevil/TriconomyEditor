using System.Collections.Generic;
using ProtoBuf;

namespace RuleSet.Elements
{
    [ProtoContract(UseProtoMembersOnly = true)]
    public class Building : Element
    {
        [ProtoMember(1, OverwriteList = false)]
        public List<Event> Events
        {
            get;
        } = new List<Event>();

        [ProtoMember(2, OverwriteList = false)]
        public List<Upgrade> Upgrades
        {
            get;
        } = new List<Upgrade>();

        [ProtoMember(3)]
        public int Variants { get; set; } = 0;
    }
}
