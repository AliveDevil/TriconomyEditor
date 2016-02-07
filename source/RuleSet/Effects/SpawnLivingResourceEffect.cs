using RuleSet.Elements;

namespace RuleSet.Effects
{
    public class SpawnLivingResourceEffect : Effect
    {
        public int Amount
        {
            get; set;
        }

        public int Delay
        {
            get; set;
        }

        public LivingResource LivingResource
        {
            get; set;
        }

        public int Radius
        {
            get; set;
        }
    }
}
