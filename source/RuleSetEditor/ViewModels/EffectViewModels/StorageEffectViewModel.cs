using Reactive.Bindings;
using RuleSet.Effects;

namespace RuleSetEditor.ViewModels.EffectViewModels
{
    public class StorageEffectViewModel : EffectViewModel
    {
        private ReactiveProperty<bool> publicAccessibleProperty;

        public ReactiveProperty<bool> PublicAccessible
        {
            get { return publicAccessibleProperty; }
            private set { RaiseSetIfChanged(ref publicAccessibleProperty, value); }
        }

        public StorageEffect StorageEffect => (StorageEffect)Effect;

        public override string ToString()
        {
            return "Storage";
        }
    }
}
