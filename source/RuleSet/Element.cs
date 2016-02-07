using System;

namespace RuleSet
{
    [Serializable]
    public abstract class Element : SerializedObject
    {
        public string Name
        {
            get; set;
        }
    }
}
