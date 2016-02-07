namespace RuleSet.Elements
{
    public class WorldResource : Element
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

        public int Variants { get; set; } = 0;
    }
}
