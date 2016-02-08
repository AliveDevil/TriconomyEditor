using ProtoBuf;

namespace RuleSet.Conditions
{
    [ProtoContract(UseProtoMembersOnly = true)]
    public class LevelCondition : Condition
    {
        [ProtoMember(1)]
        public int Level
        {
            get; set;
        }
    }
}
