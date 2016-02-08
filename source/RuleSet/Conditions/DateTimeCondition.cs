using System;
using ProtoBuf;

namespace RuleSet.Conditions
{
    [ProtoContract(UseProtoMembersOnly = true)]
    public class DateTimeCondition : Condition
    {
        [ProtoMember(1)]
        public DateTime DateTime
        {
            get; set;
        }
    }
}
