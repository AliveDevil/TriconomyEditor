using ReactiveUI;
using RuleSet.Elements;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels;

namespace RuleSetEditor.ViewModels.RuleSetViewModels
{
    public class StartInventoryListViewModel : RuleSetViewModelBase
    {
        private RelayCommand addResourcePartCommand;
        private RelayCommand<ResourcePartViewModel> editResourcePartCommand;
        private RelayCommand removeResourcePartCommand;
        private ResourcePartViewModel selectedResourcePart;
        private IReactiveDerivedList<ResourcePartViewModel> startResources;

        public RelayCommand AddResourcePartCommand => addResourcePartCommand ?? (addResourcePartCommand = new RelayCommand(() =>
        {
            RuleSetViewModel.StartResources.Add(ViewStack.Push(RuleSetViewModel.Create<ResourcePartViewModel>(v =>
            {
                v.ResourcePart = new ResourcePart();
            })));
        }));

        public RelayCommand<ResourcePartViewModel> EditResourcePartCommand => editResourcePartCommand ?? (editResourcePartCommand = new RelayCommand<ResourcePartViewModel>(resourcePart =>
        {
            ViewStack.Push(SelectedResourcePart = resourcePart);
        }));

        public RelayCommand RemoveResourcePartCommand => removeResourcePartCommand ?? (removeResourcePartCommand = new RelayCommand(() =>
        {
            RuleSetViewModel.StartResources.Remove(SelectedResourcePart);
        }));

        public ResourcePartViewModel SelectedResourcePart
        {
            get
            {
                return selectedResourcePart;
            }
            set
            {
                RaiseSetIfChanged(ref selectedResourcePart, value);
            }
        }

        public IReactiveDerivedList<ResourcePartViewModel> StartResources
        {
            get
            {
                return startResources;
            }
            private set
            {
                RaiseSetIfChanged(ref startResources, value);
            }
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            StartResources = RuleSetViewModel.StartResources.CreateDerivedCollection(e => e);
            StartResources.ChangeTrackingEnabled = true;
        }
    }
}
