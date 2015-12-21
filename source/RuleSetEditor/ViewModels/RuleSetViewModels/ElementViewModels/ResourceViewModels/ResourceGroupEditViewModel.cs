using Reactive.Bindings;
using ReactiveUI;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels.ResourceViewModels
{
    public class ResourceGroupEditViewModel : RuleSetViewModelBase
    {
        private RelayCommand addResourceCommand;
        private IReactiveDerivedList<ResourceViewModel> availableResources;
        private ResourceGroupViewModel resourceGroup;
        private ResourceViewModel selectedResource;

        public RelayCommand AddResourceCommand
        {
            get
            {
                return addResourceCommand ?? (addResourceCommand = new RelayCommand(() =>
                {
                    if (ResourceGroup.ResourceList.Contains(SelectedResource)) return;
                    ResourceGroup.ResourceList.Add(SelectedResource);
                }));
            }
        }

        public IReactiveDerivedList<ResourceViewModel> AvailableResources
        {
            get { return availableResources; }
            private set { RaiseSetIfChanged(ref availableResources, value); }
        }

        public ReactiveProperty<string> Name { get; private set; }

        public ResourceGroupViewModel ResourceGroup
        {
            get
            {
                return resourceGroup;
            }
            set
            {
                if (!RaiseSetIfChanged(ref resourceGroup, value)) return;
                Name = ResourceGroup.Name;
                ResourceList = ResourceGroup.ResourceList;
            }
        }

        public ReactiveList<ResourceViewModel> ResourceList { get; private set; }

        public ResourceViewModel SelectedResource
        {
            get { return selectedResource; }
            set { RaiseSetIfChanged(ref selectedResource, value); }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }

        protected override void OnRuleSetChanged()
        {
            base.OnRuleSetChanged();
            AvailableResources = RuleSetViewModel.ElementList.CreateDerivedCollection(e => (ResourceViewModel)e, e => e is ResourceViewModel, (l, r) => l.Name.Value.CompareTo(r.Name.Value));
        }
    }
}
