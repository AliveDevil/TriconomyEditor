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
            get { return building; }
            private set { RaiseSetIfChanged(ref building, value); }
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Building?.Value?.Dispose();
                Building?.Dispose();
                Buildings.Dispose();

                buildings = null;
                building = null;
            }
            base.Dispose(disposing);
        }

        protected override void OnElementChanged()
        {
            base.OnElementChanged();
            Building = ReactiveProperty.FromObject(Need,
                b => b.Building,
                b => Buildings.SingleOrDefault(e => e.Building == b),
                b => b?.Building);
            Building.PropertyChanged += OnPropertyChanged;
            Radius = ReactiveProperty.FromObject(Need,
                b => b.Radius);
        }

        protected override void OnRuleSetChanged()
        {
            base.OnRuleSetChanged();
            Buildings = RuleSetViewModel.ElementList.CreateDerivedCollection(e => (BuildingViewModel)e, e => e is BuildingViewModel);
        }
    }
}
