using System.Linq;
using ReactiveUI;
using RuleSet.Elements;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels.ResourceViewModels;

namespace RuleSetEditor.ViewModels.RuleSetViewModels
{
    public class ResourceListViewModel : RuleSetViewModelBase
    {
        private RelayCommand addResourceCommand;
        private RelayCommand addResourceGroupCommand;
        private RelayCommand<ResourceInfoViewModel> editResourceCommand;
        private RelayCommand removeResourceCommand;
        private IReactiveDerivedList<ResourceInfoViewModel> resources;
        private ResourceInfoViewModel selectedResource;

        public RelayCommand AddResourceCommand
        {
            get
            {
                return addResourceCommand ?? (addResourceCommand = new RelayCommand(() =>
                {
                    AddAndSelectNewResource<ResourceViewModel, Resource>("New Resource");
                }));
            }
        }

        public RelayCommand AddResourceGroupCommand
        {
            get
            {
                return addResourceGroupCommand ?? (addResourceGroupCommand = new RelayCommand(() =>
                {
                    AddAndSelectNewResource<ResourceGroupViewModel, ResourceGroup>("New Resource Group");
                }));
            }
        }

        public RelayCommand<ResourceInfoViewModel> EditResourceCommand
        {
            get
            {
                return editResourceCommand ?? (editResourceCommand = new RelayCommand<ResourceInfoViewModel>(resource =>
                {
                    SelectedResource = resource;
                    if (resource.Resource is ResourceGroupViewModel)
                        ViewStack.Push<ResourceGroupEditViewModel>()._(_ => _.ResourceGroup = (ResourceGroupViewModel)resource.Resource);
                    else
                        ViewStack.Push<ResourceEditViewModel>()._(_ => _.Resource = resource.Resource);
                }));
            }
        }

        public RelayCommand RemoveResourceCommand
        {
            get
            {
                return removeResourceCommand ?? (removeResourceCommand = new RelayCommand(() =>
                {
                    RuleSetViewModel.ElementList.Remove(SelectedResource?.Resource);
                }));
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
            Resources = RuleSetViewModel.ElementList.CreateDerivedCollection(e => new ResourceInfoViewModel() { Resource = (ResourceViewModel)e }, e => e is ResourceViewModel, (l, r) => l.Name.Value.CompareTo(r.Name.Value));
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
            SelectedResource = Resources.SingleOrDefault(r => r.Resource == model);
        }
    }
}
