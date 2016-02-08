using System.Linq;
using Reactive.Bindings;
using ReactiveUI;
using RuleSet.Menus;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ToolbarViewModels
{
    public class PlaceBuildingItemViewModel : ToolbarItemViewModel<PlaceBuildingItem>
    {
        private ReactiveProperty<BuildingViewModel> buildingProperty;
        private IReactiveDerivedList<BuildingViewModel> buildings;

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

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Buildings = RuleSetViewModel.ElementList.CreateDerivedCollection(e => (BuildingViewModel)e, e => e is BuildingViewModel, (l, r) => l.Element.Name.CompareTo(r.Element.Name));
        }

        protected override void OnPostInitialize()
        {
            base.OnPostInitialize();

            Building = ReactiveProperty.FromObject(MenuItem,
                v => v.Building,
                b => Buildings.SingleOrDefault(v => v.Element == b),
                v => v?.Element);
            Building.PropertyChanged += OnPropertyChanged;
        }
    }
}
