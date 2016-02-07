namespace RuleSet.Elements
{
    public class LivingResource : Element
    {
        public int Amount
        {
            get; set;
        }

        public bool AutoSpawn
        {
            get; set;
        }

        public Resource Resource
        {
            get; set;
        }
    }
}
