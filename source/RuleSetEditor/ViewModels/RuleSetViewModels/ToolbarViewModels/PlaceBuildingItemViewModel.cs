using Reactive.Bindings;
using ReactiveUI;
using System.Linq;
using RuleSet.Menus;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ToolbarViewModels
{
    public class PlaceBuildingItemViewModel : ToolbarItemViewModel
    {
        private ReactiveProperty<BuildingViewModel> buildingProperty;
        private IReactiveDerivedList<BuildingViewModel> buildings;

        public ReactiveProperty<BuildingViewModel> Building
        {
            get { return buildingProperty; }
            private set { RaiseSetIfChanged(ref buildingProperty, value); }
        }

        public PlaceBuildingItem PlaceBuilding => (PlaceBuildingItem)MenuItem;

        public IReactiveDerivedList<BuildingViewModel> Buildings
        {
            get { return buildings; }
            private set { RaiseSetIfChanged(ref buildings, value); }
        }

        protected override void OnMenuItemChanged()
        {
            base.OnMenuItemChanged();
            Building = ReactiveProperty.FromObject(PlaceBuilding,
                v => v.Building,
                b => Buildings.SingleOrDefault(v => v.Building == b),
                v => v?.Building);
        }

        protected override void OnRuleSetChanged()
        {
            base.OnRuleSetChanged();
            Buildings = RuleSetViewModel.ElementList.CreateDerivedCollection(e => (BuildingViewModel)e, e => e is BuildingViewModel);
        }
    }
}
