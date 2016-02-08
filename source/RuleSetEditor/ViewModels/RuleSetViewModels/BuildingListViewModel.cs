using ReactiveUI;
using RuleSet.Elements;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels;

namespace RuleSetEditor.ViewModels.RuleSetViewModels
{
    public class BuildingListViewModel : RuleSetViewModelBase
    {
        private RelayCommand addBuildingCommand;
        private IReactiveDerivedList<BuildingViewModel> buildings;
        private RelayCommand<BuildingViewModel> editBuildingCommand;
        private RelayCommand removeBuildingCommand;
        private BuildingViewModel selectedBuilding;

        public RelayCommand AddBuildingCommand => addBuildingCommand ?? (addBuildingCommand = new RelayCommand(() =>
        {
            BuildingViewModel model = new BuildingViewModel()
            {
                RuleSetViewModel = RuleSetViewModel,
                Element = new Building()
                {
                    Name = "New Building"
                }
            };
            RuleSetViewModel.ElementList.Add(model);
            ViewStack.Push(SelectedBuilding = model);
        }));

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

        public RelayCommand<BuildingViewModel> EditBuildingCommand => editBuildingCommand ?? (editBuildingCommand = new RelayCommand<BuildingViewModel>(building =>
        {
            ViewStack.Push(SelectedBuilding = building);
        }));

        public RelayCommand RemoveBuildingCommand => removeBuildingCommand ?? (removeBuildingCommand = new RelayCommand(() =>
         {
             RuleSetViewModel.ElementList.Remove(SelectedBuilding);
         }));

        public BuildingViewModel SelectedBuilding
        {
            get
            {
                return selectedBuilding;
            }
            set
            {
                RaiseSetIfChanged(ref selectedBuilding, value);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Dispose(ref buildings);
                Dispose(ref addBuildingCommand);
                Dispose(ref editBuildingCommand);
                Dispose(ref removeBuildingCommand);
            }
            base.Dispose(disposing);
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Buildings = RuleSetViewModel.ElementList.CreateDerivedCollection(e => (BuildingViewModel)e, e => e is BuildingViewModel, (l, r) => l.Name.Value.CompareTo(r.Name.Value));
            Buildings.ChangeTrackingEnabled = true;
        }
    }
}
