using ProtoBuf;
using RuleSet.Elements;

namespace RuleSet
{
    [ProtoContract(UseProtoMembersOnly = false)]
    [ProtoInclude(2, typeof(Building))]
    [ProtoInclude(3, typeof(Job))]
    [ProtoInclude(4, typeof(LivingResource))]
    [ProtoInclude(5, typeof(Resource))]
    [ProtoInclude(6, typeof(WorldResource))]
    public class Element
    {
        [ProtoMember(1)]
        public string Name
        {
            get; set;
        }
    }
}
