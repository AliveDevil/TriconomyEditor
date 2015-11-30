using RuleSet.Effects;

namespace RuleSetEditor.ViewModels.EffectViewModels
{
    public class ExtendSettlerAmountEffectViewModel : EffectViewModel
    {
        public ExtendSettlerAmountEffect ExtendSettlerAmountEffect => (ExtendSettlerAmountEffect)Effect;

        public override string ToString()
        {
            return "Extend Settler Amount";
        }
    }
}
