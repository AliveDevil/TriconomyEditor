using ReactiveUI;
using RuleSet.Elements;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels.ResourceViewModels;

namespace RuleSetEditor.ViewModels.RuleSetViewModels
{
    public class ResourceListViewModel : RuleSetViewModelBase
    {
        private IReactiveDerivedList<ResourceViewModel> resources;
        private ResourceViewModel selectedResource;

        public RelayCommand AddResourceCommand => new RelayCommand(() =>
        {
            AddAndSelectNewResource<ResourceViewModel, Resource>("New Resource");
        });

        public RelayCommand AddResourceGroupCommand => new RelayCommand(() =>
        {
            AddAndSelectNewResource<ResourceGroupViewModel, ResourceGroup>("New Resource Group");
        });

        public RelayCommand<ResourceViewModel> EditResourceCommand => new RelayCommand<ResourceViewModel>(resource =>
        {
            SelectedResource = resource;
            if (resource is ResourceGroupViewModel)
                ViewStack.Push<ResourceGroupEditViewModel>()._(_ => { _.ResourceGroup = (ResourceGroupViewModel)resource; });
            else
                ViewStack.Push<ResourceEditViewModel>()._(_ => { _.Resource = resource; });
        });

        public RelayCommand RemoveResourceCommand => new RelayCommand(() =>
        {
            RuleSetViewModel.ElementList.Remove(SelectedResource);
        });

        public IReactiveDerivedList<ResourceViewModel> Resources
        {
            get { return resources; }
            private set { RaiseSetIfChanged(ref resources, value); }
        }

        public ResourceViewModel SelectedResource
        {
            get { return selectedResource; }
            set { RaiseSetIfChanged(ref selectedResource, value); }
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

        protected override void OnRuleSetChanged()
        {
            base.OnRuleSetChanged();
            Resources = RuleSetViewModel.ElementList.CreateDerivedCollection(e => (ResourceViewModel)e, e => e is ResourceViewModel, (l, r) => l.Name.Value.CompareTo(r.Name.Value));
        }

        private void AddAndSelectNewResource<TViewModel, TItem>(string name)
            where TViewModel : ResourceViewModel, new()
            where TItem : Resource, new()
        {
            TViewModel model = new TViewModel()
            {
                RuleSetViewModel = RuleSetViewModel,
                Element = new TItem()
                {
                    Name = name
                }
            };
            RuleSetViewModel.ElementList.Add(model);
            SelectedResource = model;
        }
    }
}
