using RuleSet.Effects;

namespace RuleSetEditor.ViewModels.EffectViewModels
{
    public class SpawnWorldResourceEffectViewModel : EffectViewModel
    {
        public SpawnWorldResourceEffect SpawnWorldResourceEffect => (SpawnWorldResourceEffect)Effect;

        public override string ToString()
        {
            return "Spawn World Resource Effect";
        }
    }
}
