using System.Linq;
using Reactive.Bindings;
using ReactiveUI;
using RuleSet.Effects;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels;

namespace RuleSetEditor.ViewModels.EffectViewModels
{
    public class DeliverEffectViewModel : EffectViewModel<DeliverEffect>
    {
        private ReactiveProperty<BuildingViewModel> buildingProperty;
        private IReactiveDerivedList<BuildingViewModel> buildings;
        private ReactiveProperty<int> priorityProperty;
        private ReactiveProperty<ResourceViewModel> resourceProperty;
        private IReactiveDerivedList<ResourceViewModel> resources;

        public ReactiveProperty<BuildingViewModel> Building
        {
            get
            {
                return buildingProperty;
            }
            private set
            {
                RaiseSetIfChanged(ref buildingProperty, value);
            }
        }

        public IReactiveDerivedList<BuildingViewModel> Buildings
        {
            get
            {
                return buildings;
            }
            private set
            {
                RaiseSetIfChanged(ref buildings, value);
            }
        }

        public ReactiveProperty<int> Priority
        {
            get
            {
                return priorityProperty;
            }
            private set
            {
                RaiseSetIfChanged(ref priorityProperty, value);
            }
        }

        public ReactiveProperty<ResourceViewModel> Resource
        {
            get
            {
                return resourceProperty;
            }
            private set
            {
                RaiseSetIfChanged(ref resourceProperty, value);
            }
        }

        public IReactiveDerivedList<ResourceViewModel> Resources
        {
            get
            {
                return resources;
            }
            private set
            {
                RaiseSetIfChanged(ref resources, value);
            }
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Priority = ReactiveProperty.FromObject(Effect, d => d.Priority);
        }

        protected override void OnPostInitialize()
        {
            base.OnPostInitialize();

            Resources = RuleSetViewModel.ElementList.CreateDerivedCollection(e => (ResourceViewModel)e, e => e is ResourceViewModel, (l, r) => l.Name.Value.CompareTo(r.Name.Value));
            Resources.ChangeTrackingEnabled = true;

            Buildings = RuleSetViewModel.ElementList.CreateDerivedCollection(e => (BuildingViewModel)e, e => e is BuildingViewModel, (l, r) => l.Name.Value.CompareTo(r.Name.Value));
            Buildings.ChangeTrackingEnabled = true;

            Building = ReactiveProperty.FromObject(Effect,
                    b => b.PreferredBuilding,
                    b => Buildings.SingleOrDefault(e => e.Element == b),
                    b => b?.Element);

            Resource = ReactiveProperty.FromObject(Effect,
                    r => r.Resource,
                    r => Resources.SingleOrDefault(e => e.Element == r),
                    r => r?.Element);
        }
    }
}
