using Reactive.Bindings;
using RuleSet.Effects;

namespace RuleSetEditor.ViewModels.EffectViewModels
{
    public class ExtendStorageEffectViewModel : EffectViewModel
    {
        public ExtendStorageEffect ExtendStorageEffect => (ExtendStorageEffect)Effect;

        public override string ToString()
        {
            return "Extend Storage";
        }
    }
}
