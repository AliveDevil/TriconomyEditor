using System;

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
