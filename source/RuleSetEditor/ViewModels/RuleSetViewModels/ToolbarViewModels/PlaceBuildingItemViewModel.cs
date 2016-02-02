using System.Linq;
using Reactive.Bindings;
using ReactiveUI;
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

        public IReactiveDerivedList<BuildingViewModel> Buildings
        {
            get { return buildings; }
            private set { RaiseSetIfChanged(ref buildings, value); }
        }

        public PlaceBuildingItem PlaceBuilding => (PlaceBuildingItem)MenuItem;

        protected override void OnMenuItemChanged()
        {
            base.OnMenuItemChanged();
            Building = ReactiveProperty.FromObject(PlaceBuilding,
                v => v.Building,
                b => Buildings.SingleOrDefault(v => v.Element == b),
                v => v?.Element);
        }

        protected override void OnRuleSetChanged()
        {
            base.OnRuleSetChanged();
            Buildings = RuleSetViewModel.ElementList.CreateDerivedCollection(e => (BuildingViewModel)e, e => e is BuildingViewModel, (l, r) => l.Element.Name.CompareTo(r.Element.Name));
        }
    }
}
