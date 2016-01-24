using System.Linq;
using Reactive.Bindings;
using ReactiveUI;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels;

namespace RuleSetEditor.ViewModels.EffectViewModels.EffectEditViewModels
{
    public class ProduceResourceEffectEditViewModel : RuleSetViewModelBase
    {
        private ReactiveProperty<int> amountProperty;
        private ReactiveProperty<int> delayProperty;
        private ProduceResourceEffectViewModel produceResourceEffect;
        private ReactiveProperty<ResourceViewModel> resource;
        private IReactiveDerivedList<ResourceViewModel> resources;

        public ReactiveProperty<int> Amount
        {
            get { return amountProperty; }
            private set { RaiseSetIfChanged(ref amountProperty, value); }
        }

        public ReactiveProperty<int> Delay
        {
            get { return delayProperty; }
            private set { RaiseSetIfChanged(ref delayProperty, value); }
        }

        public ProduceResourceEffectViewModel ProduceResourceEffect
        {
            get
            {
                return produceResourceEffect;
            }
            set
            {
                if (!RaiseSetIfChanged(ref produceResourceEffect, value)) return;
                Amount = ReactiveProperty.FromObject(ProduceResourceEffect.ProduceResourceEffect, e => e.Amount);
                Delay = ReactiveProperty.FromObject(ProduceResourceEffect.ProduceResourceEffect, e => e.Delay);
                Resource = ReactiveProperty.FromObject(ProduceResourceEffect.ProduceResourceEffect,
                    r => r.Resource,
                    r => Resources.SingleOrDefault(e => e.Element == r),
                    r => r?.Element);
            }
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

        protected override void OnRuleSetChanged()
        {
            base.OnRuleSetChanged();

            Resources = RuleSetViewModel.ElementList.CreateDerivedCollection(e => (ResourceViewModel)e, e => e is ResourceViewModel, (l, r) => l.Name.Value.CompareTo(r.Name.Value));
            Resources.ChangeTrackingEnabled = true;
        }
    }
}
