using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reactive.Bindings;

namespace RuleSetEditor.ViewModels.EffectViewModels.EffectEditViewModels
{
    public class ExtendStorageEffectEditViewModel : RuleSetViewModelBase
    {
        private ExtendStorageEffectViewModel extendStorageEffect;

        private ReactiveProperty<int> amountProperty;

        public ReactiveProperty<int> Amount
        {
            get { return amountProperty; }
            private set { RaiseSetIfChanged(ref amountProperty, value); }
        }


        public ExtendStorageEffectViewModel ExtendStorageEffect
        {
            get { return extendStorageEffect; }
            set
            {
                if (!RaiseSetIfChanged(ref extendStorageEffect, value)) return;
                Amount = ExtendStorageEffect.Amount;
            }
        }
    }
}
