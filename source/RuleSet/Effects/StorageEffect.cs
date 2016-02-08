using ProtoBuf;

namespace RuleSet.Effects
{
    [ProtoContract(UseProtoMembersOnly = true)]
    public class StorageEffect : Effect
    {
        [ProtoMember(1)]
        public bool PublicAccessible
        {
            get; set;
        }
    }
}
