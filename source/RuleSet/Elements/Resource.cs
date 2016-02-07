using System;

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
