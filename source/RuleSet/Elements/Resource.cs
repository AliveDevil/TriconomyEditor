using System;
using ProtoBuf;

namespace RuleSet.Elements
{
    [Serializable]
    public class Resource : Element
    {
        public int StackSize
        {
            get; set;
        }
    }
}
