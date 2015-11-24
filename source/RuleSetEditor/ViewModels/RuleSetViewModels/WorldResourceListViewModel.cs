using ReactiveUI;
using RuleSet.Elements;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels.WorldResourceViewModels;

namespace RuleSetEditor.ViewModels.RuleSetViewModels
{
    public class WorldResourceListViewModel : RuleSetViewModelBase
    {
        private RelayCommand addWorldResourceCommand;
        private RelayCommand<WorldResourceInfoViewModel> editWorldResourceCommand;
        private RelayCommand removeWorldResourceCommand;
        private WorldResourceInfoViewModel selectedWorldResource;
        private IReactiveDerivedList<WorldResourceInfoViewModel> worldResources;

        public RelayCommand AddWorldResourceCommand
        {
            get
            {
                return addWorldResourceCommand ?? (addWorldResourceCommand = new RelayCommand(() =>
                {
                    RuleSetViewModel.ElementList.Add(new WorldResourceViewModel() { RuleSetViewModel = RuleSetViewModel, Element = new WorldResource() { Name = "New World Resource" } });
                }));
            }
        }

        public RelayCommand<WorldResourceInfoViewModel> EditWorldResourceCommand
        {
            get
            {
                return editWorldResourceCommand ?? (editWorldResourceCommand = new RelayCommand<WorldResourceInfoViewModel>(worldResource =>
                {
                    ViewStack.Push<WorldResourceEditViewModel>()._(_ => _.WorldResource = worldResource.WorldResource);
                }));
            }
        }

        public RelayCommand RemoveWorldResourceCommand
        {
            get
            {
                return removeWorldResourceCommand ?? (removeWorldResourceCommand = new RelayCommand(() =>
                {
                    RuleSetViewModel.ElementList.Remove(SelectedWorldResource?.WorldResource);
                }));
            }
        }

        public WorldResourceInfoViewModel SelectedWorldResource
        {
            get { return selectedWorldResource; }
            set { RaiseSetIfChanged(ref selectedWorldResource, value); }
        }

        public IReactiveDerivedList<WorldResourceInfoViewModel> WorldResources
        {
            get { return worldResources; }
            private set { RaiseSetIfChanged(ref worldResources, value); }
        }

        protected override void OnRuleSetChanged()
        {
            base.OnRuleSetChanged();
            WorldResources = RuleSetViewModel.ElementList.CreateDerivedCollection(e => new WorldResourceInfoViewModel() { WorldResource = (WorldResourceViewModel)e }, e => e is WorldResourceViewModel, (l, r) => l.Name.Value.CompareTo(r.Name.Value));
        }
    }
}
