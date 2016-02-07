using System;
using ProtoBuf;

namespace RuleSet.Effects
{
    [Serializable]
    public class ExtendSettlerAmountEffect : Effect
    {
        public int SettlerAmount
        {
            get; set;
        }
    }
}
