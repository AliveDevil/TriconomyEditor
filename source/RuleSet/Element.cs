using System;
using ProtoBuf;
using RuleSet.Elements;

namespace RuleSet
{
    [Serializable]
    public class Element
    {
        public string Name
        {
            get; set;
        }
    }
}
