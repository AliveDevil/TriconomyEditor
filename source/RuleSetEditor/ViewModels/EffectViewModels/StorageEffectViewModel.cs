using Reactive.Bindings;
using RuleSet.Effects;

namespace RuleSetEditor.ViewModels.EffectViewModels
{
    public class StorageEffectViewModel : EffectViewModel
    {
        public StorageEffect StorageEffect => (StorageEffect)Effect;

        public override string ToString()
        {
            return "Storage";
        }
    }
}
