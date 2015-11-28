using ReactiveUI;
using RuleSet.Elements;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels.WorldResourceViewModels;

namespace RuleSetEditor.ViewModels.RuleSetViewModels
{
    public class WorldResourceListViewModel : RuleSetViewModelBase
    {
        private WorldResourceViewModel selectedWorldResource;
        private IReactiveDerivedList<WorldResourceViewModel> worldResources;

        public RelayCommand AddWorldResourceCommand => new RelayCommand(() =>
        {
            WorldResourceViewModel model = new WorldResourceViewModel() { RuleSetViewModel = RuleSetViewModel, Element = new WorldResource() { Name = "New World Resource" } };
            RuleSetViewModel.ElementList.Add(model);
            SelectedWorldResource = model;
        });

        public RelayCommand<WorldResourceViewModel> EditWorldResourceCommand => new RelayCommand<WorldResourceViewModel>(worldResource =>
        {
            SelectedWorldResource = worldResource;
            ViewStack.Push<WorldResourceEditViewModel>()._(_ => _.WorldResource = worldResource);
        });

        public RelayCommand RemoveWorldResourceCommand => new RelayCommand(() =>
        {
            RuleSetViewModel.ElementList.Remove(SelectedWorldResource);
        });

        public WorldResourceViewModel SelectedWorldResource
        {
            get { return selectedWorldResource; }
            set { RaiseSetIfChanged(ref selectedWorldResource, value); }
        }

        public IReactiveDerivedList<WorldResourceViewModel> WorldResources
        {
            get { return worldResources; }
            private set { RaiseSetIfChanged(ref worldResources, value); }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Dispose(ref worldResources);
                AddWorldResourceCommand.Dispose();
                EditWorldResourceCommand.Dispose();
                RemoveWorldResourceCommand.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void OnRuleSetChanged()
        {
            base.OnRuleSetChanged();
            WorldResources = RuleSetViewModel.ElementList.CreateDerivedCollection(e => (WorldResourceViewModel)e, e => e is WorldResourceViewModel, (l, r) => l.Name.Value.CompareTo(r.Name.Value));
        }
    }
}
