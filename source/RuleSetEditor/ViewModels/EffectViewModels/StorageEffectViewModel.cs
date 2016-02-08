using Reactive.Bindings;
using RuleSet.Effects;

namespace RuleSetEditor.ViewModels.EffectViewModels
{
    public class StorageEffectViewModel : EffectViewModel<StorageEffect>
    {
        private ReactiveProperty<bool> publicAccessibleProperty;

        public ReactiveProperty<bool> PublicAccessible
        {
            get
            {
                return publicAccessibleProperty;
            }
            private set
            {
                RaiseSetIfChanged(ref publicAccessibleProperty, value);
            }
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            PublicAccessible = ReactiveProperty.FromObject(Effect, e => e.PublicAccessible);
        }
    }
}
