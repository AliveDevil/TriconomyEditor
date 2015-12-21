using System.Linq;
using Reactive.Bindings;
using ReactiveUI;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels;

namespace RuleSetEditor.ViewModels.EffectViewModels.EffectEditViewModels
{
    public class DeliverEffectEditViewModel : RuleSetViewModelBase
    {
        private ReactiveProperty<BuildingViewModel> buildingProperty;
        private IReactiveDerivedList<BuildingViewModel> buildings;
        private DeliverEffectViewModel deliverEffect;
        private ReactiveProperty<int> priorityProperty;
        private ReactiveProperty<ResourceViewModel> resourceProperty;
        private IReactiveDerivedList<ResourceViewModel> resources;

        public ReactiveProperty<BuildingViewModel> Building
        {
            get { return buildingProperty; }
            private set { RaiseSetIfChanged(ref buildingProperty, value); }
        }

        public IReactiveDerivedList<BuildingViewModel> Buildings
        {
            get { return buildings; }
            private set { RaiseSetIfChanged(ref buildings, value); }
        }

        public DeliverEffectViewModel DeliverEffect
        {
            get
            {
                return deliverEffect;
            }
            set
            {
                if (!RaiseSetIfChanged(ref deliverEffect, value))
                    return;
                Building = ReactiveProperty.FromObject(DeliverEffect.DeliverEffect,
                    b => b.PreferredBuilding,
                    b => Buildings.SingleOrDefault(e => e.Building == b),
                    b => b?.Building);
                Priority = ReactiveProperty.FromObject(DeliverEffect.DeliverEffect, d => d.Priority);
                Resource = ReactiveProperty.FromObject(DeliverEffect.DeliverEffect,
                    r => r.Resource,
                    r => Resources.SingleOrDefault(e => e.Resource == r),
                    r => r?.Resource);
            }
        }

        public ReactiveProperty<int> Priority
        {
            get { return priorityProperty; }
            private set { RaiseSetIfChanged(ref priorityProperty, value); }
        }

        public ReactiveProperty<ResourceViewModel> Resource
        {
            get { return resourceProperty; }
            private set { RaiseSetIfChanged(ref resourceProperty, value); }
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

            Buildings = RuleSetViewModel.ElementList.CreateDerivedCollection(e => (BuildingViewModel)e, e => e is BuildingViewModel, (l, r) => l.Name.Value.CompareTo(r.Name.Value));
            Buildings.ChangeTrackingEnabled = true;
        }
    }
}
