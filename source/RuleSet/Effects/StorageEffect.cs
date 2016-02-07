using System;
using ProtoBuf;

namespace RuleSet.Effects
{
    [Serializable]
    public class StorageEffect : Effect
    {
        public bool PublicAccessible
        {
            get; set;
        }
    }
}
