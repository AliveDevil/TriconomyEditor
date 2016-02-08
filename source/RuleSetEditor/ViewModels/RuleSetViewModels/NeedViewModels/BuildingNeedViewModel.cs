using System.Linq;
using Reactive.Bindings;
using ReactiveUI;
using RuleSet.Needs;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.NeedViewModels
{
    public class BuildingNeedViewModel : NeedViewModel<BuildingNeed>
    {
        private ReactiveProperty<BuildingViewModel> building;
        private IReactiveDerivedList<BuildingViewModel> buildings;
        private ReactiveProperty<int> radiusProperty;

        public ReactiveProperty<BuildingViewModel> Building
        {
            get
            {
                return building;
            }
            private set
            {
                RaiseSetIfChanged(ref building, value);
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

        public ReactiveProperty<int> Radius
        {
            get
            {
                return radiusProperty;
            }
            private set
            {
                RaiseSetIfChanged(ref radiusProperty, value);
            }
        }
        
        protected override void OnInitialize()
        {
            base.OnInitialize();

            Buildings = RuleSetViewModel.ElementList.CreateDerivedCollection(e => (BuildingViewModel)e, e => e is BuildingViewModel, (l, r) => l.Name.Value.CompareTo(r.Name.Value));
            Buildings.ChangeTrackingEnabled = true;

            Radius = ReactiveProperty.FromObject(Need,
                b => b.Radius);
        }

        protected override void OnPostInitialize()
        {
            base.OnPostInitialize();

            Building = ReactiveProperty.FromObject(Need,
                b => b.Building,
                b => Buildings.SingleOrDefault(e => e.Element == b),
                b => b?.Element);
            Building.PropertyChanged += OnPropertyChanged;
        }
    }
}
