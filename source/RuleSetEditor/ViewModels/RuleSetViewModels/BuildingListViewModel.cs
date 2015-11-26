using ReactiveUI;
using RuleSet.Elements;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels.BuildingViewModels;

namespace RuleSetEditor.ViewModels.RuleSetViewModels
{
    public class BuildingListViewModel : RuleSetViewModelBase
    {
        private RelayCommand addBuildingCommand;
        private IReactiveDerivedList<BuildingInfoViewModel> buildings;
        private RelayCommand<BuildingInfoViewModel> editBuildingCommand;
        private RelayCommand removeBuildingCommand;
        private BuildingInfoViewModel selectedBuilding;

        public RelayCommand AddBuildingCommand
        {
            get
            {
                return addBuildingCommand ?? (addBuildingCommand = new RelayCommand(() =>
                {
                    RuleSetViewModel.ElementList.Add(new BuildingViewModel() { RuleSetViewModel = RuleSetViewModel, Element = new Building() { Name = "New Building" } });
                }));
            }
        }

        public IReactiveDerivedList<BuildingInfoViewModel> Buildings
        {
            get { return buildings; }
            private set { RaiseSetIfChanged(ref buildings, value); }
        }

        public RelayCommand<BuildingInfoViewModel> EditBuildingCommand
        {
            get
            {
                return editBuildingCommand ?? (editBuildingCommand = new RelayCommand<BuildingInfoViewModel>(building =>
                {
                    ViewStack.Push<BuildingEditViewModel>()._(_ => _.Building = building.Building);
                }));
            }
        }

        public RelayCommand RemoveBuildingCommand
        {
            get
            {
                return removeBuildingCommand ?? (removeBuildingCommand = new RelayCommand(() =>
                {
                    RuleSetViewModel.ElementList.Remove(SelectedBuilding?.Building);
                }));
            }
        }

        public BuildingInfoViewModel SelectedBuilding
        {
            get { return selectedBuilding; }
            set { RaiseSetIfChanged(ref selectedBuilding, value); }
        }

        protected override void OnRuleSetChanged()
        {
            base.OnRuleSetChanged();
            Buildings = RuleSetViewModel.ElementList.CreateDerivedCollection(e => new BuildingInfoViewModel() { Building = (BuildingViewModel)e }, e => e is BuildingViewModel, (l, r) => l.Name.Value.CompareTo(r.Name.Value));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Buildings.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
