using RuleSet.Effects;

namespace RuleSetEditor.ViewModels.EffectViewModels
{
    public class ProduceResourceEffectViewModel : EffectViewModel
    {
        public ProduceResourceEffect ProduceResourceEffect => (ProduceResourceEffect)Effect;

        public override string ToString()
        {
            return "Produce Resource";
        }
    }
}
