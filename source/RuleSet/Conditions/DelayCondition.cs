using System;
using ProtoBuf;

namespace RuleSet.Conditions
{
    [ProtoContract(UseProtoMembersOnly = true)]
    public class DelayCondition : Condition
    {
        [ProtoMember(1)]
        public TimeSpan Delay
        {
            get; set;
        }
    }
}
