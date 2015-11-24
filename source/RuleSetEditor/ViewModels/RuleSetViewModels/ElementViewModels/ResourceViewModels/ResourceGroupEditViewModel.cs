using Reactive.Bindings;
using ReactiveUI;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels.ResourceViewModels
{
    public class ResourceGroupEditViewModel : RuleSetViewModelBase
    {
        private RelayCommand addResourceCommand;
        private IReactiveDerivedList<ResourceInfoViewModel> availableResources;
        private ReactiveProperty<string> nameProperty;
        private ResourceGroupViewModel resourceGroup;
        private IReactiveDerivedList<ResourceInfoViewModel> resources;
        private ResourceInfoViewModel selectedResource;

        public RelayCommand AddResourceCommand
        {
            get
            {
                return addResourceCommand ?? (addResourceCommand = new RelayCommand(() =>
                {
                    if (ResourceGroup.ResourceList.Contains(SelectedResource.Resource)) return;
                    ResourceGroup.ResourceList.Add(SelectedResource.Resource);
                }));
            }
        }

        public IReactiveDerivedList<ResourceInfoViewModel> AvailableResources
        {
            get { return availableResources; }
            private set { RaiseSetIfChanged(ref availableResources, value); }
        }

        public ReactiveProperty<string> Name
        {
            get { return nameProperty; }
            private set { RaiseSetIfChanged(ref nameProperty, value); }
        }

        public ResourceGroupViewModel ResourceGroup
        {
            get
            {
                return resourceGroup;
            }
            set
            {
                RaiseSetIfChanged(ref resourceGroup, value);
                Name = ResourceGroup.Name;
                Resources = ResourceGroup.ResourceList.CreateDerivedCollection(r => new ResourceInfoViewModel() { RuleSetViewModel = RuleSetViewModel, Resource = r });
            }
        }

        public IReactiveDerivedList<ResourceInfoViewModel> Resources
        {
            get { return resources; }
            private set { RaiseSetIfChanged(ref resources, value); }
        }

        public ResourceInfoViewModel SelectedResource
        {
            get { return selectedResource; }
            set { RaiseSetIfChanged(ref selectedResource, value); }
        }

        protected override void OnRuleSetChanged()
        {
            base.OnRuleSetChanged();
            AvailableResources = RuleSetViewModel.ElementList.CreateDerivedCollection(e => new ResourceInfoViewModel() { Resource = (ResourceViewModel)e, RuleSetViewModel = RuleSetViewModel }, e => e is ResourceViewModel);
        }
    }
}
