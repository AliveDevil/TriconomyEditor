using ReactiveUI;
using RuleSet.Elements;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels;

namespace RuleSetEditor.ViewModels.RuleSetViewModels
{
    public class ResourceListViewModel : RuleSetViewModelBase
    {
        private RelayCommand addResourceCommand;
        private RelayCommand addResourceGroupCommand;
        private RelayCommand<ResourceViewModel> editResourceCommand;
        private RelayCommand removeResourceCommand;
        private IReactiveDerivedList<ResourceViewModel> resources;
        private ResourceViewModel selectedResource;

        public RelayCommand AddResourceCommand => addResourceCommand ?? (addResourceCommand = new RelayCommand(() =>
        {
            AddAndSelectNewResource<ResourceViewModel, Resource>("New Resource");
        }));

        public RelayCommand AddResourceGroupCommand => addResourceGroupCommand ?? (addResourceGroupCommand = new RelayCommand(() =>
        {
            AddAndSelectNewResource<ResourceGroupViewModel, ResourceGroup>("New Resource Group");
        }));

        public RelayCommand<ResourceViewModel> EditResourceCommand => editResourceCommand ?? (editResourceCommand = new RelayCommand<ResourceViewModel>(resource =>
        {
            ViewStack.Push(SelectedResource = resource);
        }));

        public RelayCommand RemoveResourceCommand => removeResourceCommand ?? (removeResourceCommand = new RelayCommand(() =>
        {
            RuleSetViewModel.ElementList.Remove(SelectedResource);
        }));

        public IReactiveDerivedList<ResourceViewModel> Resources
        {
            get
            {
                return resources;
            }
            private set
            {
                RaiseSetIfChanged(ref resources, value);
            }
        }

        public ResourceViewModel SelectedResource
        {
            get
            {
                return selectedResource;
            }
            set
            {
                RaiseSetIfChanged(ref selectedResource, value);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Dispose(ref resources);
                AddResourceCommand.Dispose();
                AddResourceGroupCommand.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Resources = RuleSetViewModel.ElementList.CreateDerivedCollection(
                e => (ResourceViewModel)e,
                e => e is ResourceViewModel,
                (l, r) => l.Name.Value.CompareTo(r.Name.Value));
            Resources.ChangeTrackingEnabled = true;
        }

        private void AddAndSelectNewResource<TViewModel, TItem>(string name)
            where TViewModel : ResourceViewModel, new()
            where TItem : Resource, new()
        {
            RuleSetViewModel.ElementList.Add(ViewStack.Push(RuleSetViewModel.Create<TViewModel>(v =>
            {
                v.Element = new TItem() { Name = name };
            })));
        }
    }
}
