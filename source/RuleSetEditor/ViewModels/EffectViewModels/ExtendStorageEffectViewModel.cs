using Reactive.Bindings;
using RuleSet.Effects;

namespace RuleSetEditor.ViewModels.EffectViewModels
{
    public class ExtendStorageEffectViewModel : EffectViewModel
    {
        private ReactiveProperty<int> amountProperty;

        public ReactiveProperty<int> Amount
        {
            get { return amountProperty; }
            private set { RaiseSetIfChanged(ref amountProperty, value); }
        }

        public ExtendStorageEffect ExtendStorageEffect => (ExtendStorageEffect)Effect;

        public override string ToString()
        {
            return "Extend Storage";
        }

        protected override void OnEffectChanged()
        {
            base.OnEffectChanged();
            Amount = ReactiveProperty.FromObject(ExtendStorageEffect, e => e.Amount);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Dispose(ref amountProperty);
            }
            base.Dispose(disposing);
        }
    }
}
