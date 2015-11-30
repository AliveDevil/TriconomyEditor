using Reactive.Bindings;

namespace RuleSetEditor.ViewModels.EffectViewModels.EffectEditViewModels
{
    public class ExtendStorageEffectEditViewModel : RuleSetViewModelBase
    {
        private ReactiveProperty<int> amountProperty;
        private ExtendStorageEffectViewModel extendStorageEffect;

        public ReactiveProperty<int> Amount
        {
            get { return amountProperty; }
            private set { RaiseSetIfChanged(ref amountProperty, value); }
        }

        public ExtendStorageEffectViewModel ExtendStorageEffect
        {
            get
            {
                return extendStorageEffect;
            }
            set
            {
                if (!RaiseSetIfChanged(ref extendStorageEffect, value)) return;
                Amount = ExtendStorageEffect.Amount;
            }
        }
    }
}
