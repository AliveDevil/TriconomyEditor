using Newtonsoft.Json;

namespace RuleSet
{
    public abstract class Element : SerializedObject
    {
        public string Name { get; set; }
    }
}
