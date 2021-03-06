﻿using Reactive.Bindings;
using RuleSet.Effects;

namespace RuleSetEditor.ViewModels.EffectViewModels
{
    public class ExtendSettlerAmountEffectViewModel : EffectViewModel<ExtendSettlerAmountEffect>
    {
        private ReactiveProperty<int> amountProperty;

        public ReactiveProperty<int> Amount
        {
            get
            {
                return amountProperty;
            }
            private set
            {
                RaiseSetIfChanged(ref amountProperty, value);
            }
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Amount = ReactiveProperty.FromObject(Effect, e => e.SettlerAmount);
        }
    }
}
