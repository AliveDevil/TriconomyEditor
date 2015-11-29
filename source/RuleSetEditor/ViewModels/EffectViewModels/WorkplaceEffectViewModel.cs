using RuleSet.Effects;

namespace RuleSetEditor.ViewModels.EffectViewModels
{
    public class WorkplaceEffectViewModel : EffectViewModel
    {
        public WorkplaceEffect WorkplaceEffect => (WorkplaceEffect)Effect;

        public override string ToString()
        {
            return "Workplace";
        }
    }
}
