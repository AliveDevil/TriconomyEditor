using RuleSet.Effects;

namespace RuleSetEditor.ViewModels.EffectViewModels
{
    public class UseResourceEffectViewModel : EffectViewModel
    {
        public UseResourceEffect UseResourceEffect => (UseResourceEffect)Effect;

        public override string ToString()
        {
            return "Use Resource";
        }
    }
}
