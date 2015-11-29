using Reactive.Bindings;
using RuleSet.Effects;

namespace RuleSetEditor.ViewModels.EffectViewModels
{
    public class ExtendSettlerAmountEffectViewModel : EffectViewModel
    {
        private ReactiveProperty<int> amountProperty;

        public ReactiveProperty<int> Amount
        {
            get { return amountProperty; }
            private set { RaiseSetIfChanged(ref amountProperty, value); }
        }

        public ExtendSettlerAmountEffect ExtendSettlerAmountEffect => (ExtendSettlerAmountEffect)Effect;

        public override string ToString()
        {
            return "Extend Settler Amount";
        }

        protected override void OnEffectChanged()
        {
            base.OnEffectChanged();
            Amount = ReactiveProperty.FromObject(ExtendSettlerAmountEffect, e => e.SettlerAmount);
        }
    }
}
