using System;
using System.Linq;
using ReactiveUI;
using RuleSet.Elements;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels.BuildingViewModels;

namespace RuleSetEditor.ViewModels.RuleSetViewModels
{
    public class BuildingListViewModel : RuleSetViewModelBase
    {
        private IReactiveDerivedList<BuildingViewModel> buildings;
        private BuildingViewModel selectedBuilding;


        private RelayCommand addBuildingCommand;

        public RelayCommand AddBuildingCommand
        {
            get
            {
                return addBuildingCommand ?? (addBuildingCommand = new RelayCommand(() =>
                {
                    BuildingViewModel model = new BuildingViewModel() { RuleSetViewModel = RuleSetViewModel, Element = new Building() { Name = "New Building" } };
                    RuleSetViewModel.ElementList.Add(model);
                    SelectedBuilding = model;
                }));
            }
        }

        public IReactiveDerivedList<BuildingViewModel> Buildings
        {
            get { return buildings; }
            private set { RaiseSetIfChanged(ref buildings, value); }
        }


        private RelayCommand<BuildingViewModel> editBuildingCommand;

        public RelayCommand<BuildingViewModel> EditBuildingCommand
        {
            get
            {
                return editBuildingCommand ?? (editBuildingCommand = new RelayCommand<BuildingViewModel>(building =>
                {
                    SelectedBuilding = building;
                    ViewStack.Push<BuildingEditViewModel>()._(_ => _.Building = building);
                }));
            }
        }


        private RelayCommand removeBuildingCommand;

        public RelayCommand RemoveBuildingCommand
        {
            get
            {
                return removeBuildingCommand ?? (removeBuildingCommand = new RelayCommand(() =>
                {
                    RuleSetViewModel.ElementList.Remove(SelectedBuilding);
                }));
            }
        }

        public BuildingViewModel SelectedBuilding
        {
            get { return selectedBuilding; }
            set { RaiseSetIfChanged(ref selectedBuilding, value); }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Dispose(ref buildings);
                AddBuildingCommand.Dispose();
                EditBuildingCommand.Dispose();
                RemoveBuildingCommand.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void OnRuleSetChanged()
        {
            base.OnRuleSetChanged();
            Buildings = RuleSetViewModel.ElementList.CreateDerivedCollection(e => (BuildingViewModel)e, e => e is BuildingViewModel, (l, r) => l.Name.Value.CompareTo(r.Name.Value));
        }
    }
}
