using System.Linq;
using Reactive.Bindings;
using ReactiveUI;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels;

namespace RuleSetEditor.ViewModels.EffectViewModels.EffectEditViewModels
{
    public class UseResourceEffectEditViewModel : RuleSetViewModelBase
    {
        private ReactiveProperty<int> amountProperty;
        private ReactiveProperty<ResourceViewModel> resource;
        private IReactiveDerivedList<ResourceViewModel> resources;
        private UseResourceEffectViewModel useResourceEffect;

        public ReactiveProperty<int> Amount
        {
            get { return amountProperty; }
            private set { RaiseSetIfChanged(ref amountProperty, value); }
        }

        public ReactiveProperty<ResourceViewModel> Resource
        {
            get { return resource; }
            private set { RaiseSetIfChanged(ref resource, value); }
        }

        public IReactiveDerivedList<ResourceViewModel> Resources
        {
            get { return resources; }
            private set { RaiseSetIfChanged(ref resources, value); }
        }

        public UseResourceEffectViewModel UseResourceEffect
        {
            get
            {
                return useResourceEffect;
            }
            set
            {
                if (!RaiseSetIfChanged(ref useResourceEffect, value)) return;
                Amount = ReactiveProperty.FromObject(UseResourceEffect.UseResourceEffect, e => e.Amount);
                Resource = ReactiveProperty.FromObject(UseResourceEffect.UseResourceEffect,
                    r => r.Resource,
                    r => Resources.SingleOrDefault(e => e.Element == r),
                    r => r?.Element);

                Amount.PropertyChanged += OnPropertyChanged;
                Resource.PropertyChanged += OnPropertyChanged;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Dispose(ref amountProperty);
                Dispose(ref resource);
                Dispose(ref resources);
            }
            base.Dispose(disposing);
        }

        protected override void OnRuleSetChanged()
        {
            base.OnRuleSetChanged();
            Resources = RuleSetViewModel.ElementList.CreateDerivedCollection(e => (ResourceViewModel)e, e => e is ResourceViewModel, (l, r) => l.Name.Value.CompareTo(r.Name.Value));
            Resources.ChangeTrackingEnabled = true;
        }
    }
}
