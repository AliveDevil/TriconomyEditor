using RuleSet.Effects;

namespace RuleSetEditor.ViewModels.EffectViewModels
{
    public class GatherResourceEffectViewModel : EffectViewModel
    {
        public GatherResourceEffect GatherResourceEffect => (GatherResourceEffect)Effect;

        public override string ToString()
        {
            return "Gather Resource";
        }
    }
}
