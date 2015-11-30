using System.Linq;
using Reactive.Bindings;
using ReactiveUI;
using RuleSet.Effects;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels;

namespace RuleSetEditor.ViewModels.EffectViewModels
{
    public class UseResourceEffectViewModel : EffectViewModel
    {
        private ReactiveProperty<int> amountProperty;
        private ReactiveProperty<ResourceViewModel> resource;
        private IReactiveDerivedList<ResourceViewModel> resources;

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

        public UseResourceEffect UseResourceEffect => (UseResourceEffect)Effect;

        public override string ToString()
        {
            return "Use Resource";
        }

        protected override void OnEffectChanged()
        {
            base.OnEffectChanged();
            Amount = ReactiveProperty.FromObject(UseResourceEffect, e => e.Amount);
            Resource = ReactiveProperty.FromObject(UseResourceEffect,
                r => r.Resource,
                r => Resources.SingleOrDefault(e => e.Resource == r),
                r => r?.Resource);

            Amount.PropertyChanged += OnPropertyChanged;
            Resource.PropertyChanged += OnPropertyChanged;
        }

        protected override void OnRuleSetChanged()
        {
            base.OnRuleSetChanged();
            Resources = RuleSetViewModel.ElementList.CreateDerivedCollection(e => (ResourceViewModel)e, e => e is ResourceViewModel, (l, r) => l.Name.Value.CompareTo(r.Name.Value));
            Resources.ChangeTrackingEnabled = true;
        }
    }
}
