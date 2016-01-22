using System.Linq;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using ReactiveUI;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.NeedViewModels.EditViewModels
{
    public class BuildingNeedEditViewModel : RuleSetViewModelBase
    {
        private ReactiveProperty<BuildingViewModel> building;
        private BuildingNeedViewModel buildingNeed;
        private IReactiveDerivedList<BuildingViewModel> buildings;
        private ReactiveProperty<int> radiusProperty;

        public ReactiveProperty<BuildingViewModel> Building
        {
            get { return building; }
            private set { RaiseSetIfChanged(ref building, value); }
        }

        public BuildingNeedViewModel BuildingNeed
        {
            get
            {
                return buildingNeed;
            }
            set
            {
                if (!RaiseSetIfChanged(ref buildingNeed, value))
                    return;
                Radius = BuildingNeed.Radius;
                Building = BuildingNeed.Building.ToReactivePropertyAsSynchronized(
                    r => r.Value,
                    w => Buildings.SingleOrDefault(r => r == BuildingNeed.Building.Value),
                    r => r);
            }
        }

        public IReactiveDerivedList<BuildingViewModel> Buildings
        {
            get { return buildings; }
            private set { RaiseSetIfChanged(ref buildings, value); }
        }

        public ReactiveProperty<int> Radius
        {
            get { return radiusProperty; }
            private set { RaiseSetIfChanged(ref radiusProperty, value); }
        }
        protected override void OnRuleSetChanged()
        {
            base.OnRuleSetChanged();
            Buildings = RuleSetViewModel.ElementList.CreateDerivedCollection(e => (BuildingViewModel)e, e => e is BuildingViewModel, (l, r) => l.Name.Value.CompareTo(r.Name.Value));
            Buildings.ChangeTrackingEnabled = true;
        }
    }
}
