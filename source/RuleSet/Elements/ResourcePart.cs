using de.alivedevil.Attributes;

namespace RuleSet.Elements
{
    public class ResourcePart : SerializedObject
    {
        public int Amount { get; set; }

        [KeepReference]
        public Resource Resource { get; set; }
    }
}
