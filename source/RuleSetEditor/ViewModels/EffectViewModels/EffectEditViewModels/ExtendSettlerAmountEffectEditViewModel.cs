﻿using Reactive.Bindings;

namespace RuleSetEditor.ViewModels.EffectViewModels.EffectEditViewModels
{
    public class ExtendSettlerAmountEffectEditViewModel : RuleSetViewModelBase
    {
        private ReactiveProperty<int> amountProperty;
        private ExtendSettlerAmountEffectViewModel extendSettlerAmountEffect;

        public ReactiveProperty<int> Amount
        {
            get { return amountProperty; }
            private set { RaiseSetIfChanged(ref amountProperty, value); }
        }

        public ExtendSettlerAmountEffectViewModel ExtendSettlerAmountEffect
        {
            get
            {
                return extendSettlerAmountEffect;
            }
            set
            {
                if (!RaiseSetIfChanged(ref extendSettlerAmountEffect, value)) return;
                Amount = ReactiveProperty.FromObject(ExtendSettlerAmountEffect.ExtendSettlerAmountEffect, e => e.SettlerAmount);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Dispose(ref extendSettlerAmountEffect);
            }
            base.Dispose(disposing);
        }

        protected override void OnRuleSetChanged()
        {
            base.OnRuleSetChanged();
        }
    }
}
