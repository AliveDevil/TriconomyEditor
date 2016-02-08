using ProtoBuf;

namespace RuleSet.Effects
{
    [ProtoContract(UseProtoMembersOnly = true)]
    public class ExtendStorageEffect : Effect
    {
        [ProtoMember(1)]
        public int Amount
        {
            get; set;
        }
    }
}
