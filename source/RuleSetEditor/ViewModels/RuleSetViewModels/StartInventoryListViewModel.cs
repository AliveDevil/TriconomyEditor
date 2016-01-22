using ReactiveUI;
using RuleSet.Elements;
using RuleSetEditor.ViewModels.RuleSetViewModels.ResourcePartViewModels;

namespace RuleSetEditor.ViewModels.RuleSetViewModels
{
    public class StartInventoryListViewModel : RuleSetViewModelBase
    {
        private RelayCommand addResourcePartCommand;
        private RelayCommand<ResourcePartViewModel> editResourcePartCommand;
        private RelayCommand removeResourcePartCommand;
        private ResourcePartViewModel selectedResourcePart;
        private IReactiveDerivedList<ResourcePartViewModel> startResources;

        public RelayCommand AddResourcePartCommand
        {
            get
            {
                return addResourcePartCommand ?? (addResourcePartCommand = new RelayCommand(() =>
                {
                    ResourcePartViewModel model = new ResourcePartViewModel()
                    {
                        RuleSetViewModel = RuleSetViewModel,
                        ResourcePart = new ResourcePart()
                    };
                    RuleSetViewModel.StartResources.Add(model);
                    SelectedResourcePart = model;
                }));
            }
        }

        public RelayCommand<ResourcePartViewModel> EditResourcePartCommand
        {
            get
            {
                return editResourcePartCommand ?? (editResourcePartCommand = new RelayCommand<ResourcePartViewModel>(resourcePart =>
                {
                    SelectedResourcePart = resourcePart;
                    ViewStack.Push<ResourcePartEditViewModel>()._(_ => { _.ResourcePart = resourcePart; });
                }));
            }
        }

        public RelayCommand RemoveResourcePartCommand
        {
            get
            {
                return removeResourcePartCommand ?? (removeResourcePartCommand = new RelayCommand(() =>
                {
                    RuleSetViewModel.StartResources.Remove(SelectedResourcePart);
                }));
            }
        }

        public ResourcePartViewModel SelectedResourcePart
        {
            get { return selectedResourcePart; }
            set { RaiseSetIfChanged(ref selectedResourcePart, value); }
        }

        public IReactiveDerivedList<ResourcePartViewModel> StartResources
        {
            get { return startResources; }
            private set { RaiseSetIfChanged(ref startResources, value); }
        }

        protected override void OnRuleSetChanged()
        {
            base.OnRuleSetChanged();
            StartResources = RuleSetViewModel.StartResources.CreateDerivedCollection(e => e);
            StartResources.ChangeTrackingEnabled = true;
        }
    }
}
