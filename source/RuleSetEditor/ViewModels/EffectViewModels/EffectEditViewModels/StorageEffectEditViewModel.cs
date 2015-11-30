using Reactive.Bindings;

namespace RuleSetEditor.ViewModels.EffectViewModels.EffectEditViewModels
{
    public class StorageEffectEditViewModel : RuleSetViewModelBase
    {
        private ReactiveProperty<bool> publicAccessibleProperty;
        private StorageEffectViewModel storageEffect;

        public ReactiveProperty<bool> PublicAccessible
        {
            get { return publicAccessibleProperty; }
            private set { RaiseSetIfChanged(ref publicAccessibleProperty, value); }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Dispose(ref publicAccessibleProperty);
            }
            base.Dispose(disposing);
        }

        public StorageEffectViewModel StorageEffect
        {
            get
            {
                return storageEffect;
            }
            set
            {
                if (!RaiseSetIfChanged(ref storageEffect, value)) return;
                PublicAccessible = ReactiveProperty.FromObject(StorageEffect.StorageEffect, e => e.PublicAccessible);
            }
        }
    }
}
