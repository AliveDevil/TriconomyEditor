using ReactiveUI;
using RuleSet.Elements;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels;

namespace RuleSetEditor.ViewModels.RuleSetViewModels
{
    public class WorldResourceListViewModel : RuleSetViewModelBase
    {
        private RelayCommand addWorldResourceCommand;
        private RelayCommand<WorldResourceViewModel> editWorldResourceCommand;
        private RelayCommand removeWorldResourceCommand;
        private WorldResourceViewModel selectedWorldResource;
        private IReactiveDerivedList<WorldResourceViewModel> worldResources;

        public RelayCommand AddWorldResourceCommand => addWorldResourceCommand ?? (addWorldResourceCommand = new RelayCommand(() =>
        {
            WorldResourceViewModel model = new WorldResourceViewModel() { RuleSetViewModel = RuleSetViewModel, Element = new WorldResource() { Name = "New World Resource" } };
            RuleSetViewModel.ElementList.Add(model);
            SelectedWorldResource = model;
        }));

        public RelayCommand<WorldResourceViewModel> EditWorldResourceCommand => editWorldResourceCommand ?? (editWorldResourceCommand = new RelayCommand<WorldResourceViewModel>(worldResource =>
        {
            ViewStack.Push(SelectedWorldResource = worldResource);
        }));

        public RelayCommand RemoveWorldResourceCommand => removeWorldResourceCommand ?? (removeWorldResourceCommand = new RelayCommand(() =>
        {
            RuleSetViewModel.ElementList.Remove(SelectedWorldResource);
        }));

        public WorldResourceViewModel SelectedWorldResource
        {
            get
            {
                return selectedWorldResource;
            }
            set
            {
                RaiseSetIfChanged(ref selectedWorldResource, value);
            }
        }

        public IReactiveDerivedList<WorldResourceViewModel> WorldResources
        {
            get
            {
                return worldResources;
            }
            private set
            {
                RaiseSetIfChanged(ref worldResources, value);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Dispose(ref worldResources);
                Dispose(ref addWorldResourceCommand);
                Dispose(ref editWorldResourceCommand);
                Dispose(ref removeWorldResourceCommand);
            }
            base.Dispose(disposing);
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            WorldResources = RuleSetViewModel.ElementList.CreateDerivedCollection(e => (WorldResourceViewModel)e, e => e is WorldResourceViewModel, (l, r) => l.Name.Value.CompareTo(r.Name.Value));
            WorldResources.ChangeTrackingEnabled = true;
        }
    }
}
