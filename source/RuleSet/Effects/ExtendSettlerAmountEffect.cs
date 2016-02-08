using ProtoBuf;

namespace RuleSet.Effects
{
    [ProtoContract(UseProtoMembersOnly = true)]
    public class ExtendSettlerAmountEffect : Effect
    {
        [ProtoMember(1)]
        public int SettlerAmount
        {
            get; set;
        }
    }
}
