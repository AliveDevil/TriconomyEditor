using System;
using System.Linq;
using ReactiveUI;
using RuleSet.Menus;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels.ResourceViewModels;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ToolbarViewModels
{
    public class ResourceBarViewModel : RuleSetViewModelBase
    {
        private RelayCommand addResourceCommand;
        private IReactiveDerivedList<ResourceInfoViewModel> availableResources;
        private ResourceBar resourceBar;
        private ReactiveList<ResourceViewModel> resources;
        private ResourceInfoViewModel selectedResource;

        public RelayCommand AddResourceCommand
        {
            get
            {
                return addResourceCommand ?? (addResourceCommand = new RelayCommand(() =>
                {
                    if (Resources.Contains(SelectedResource.Resource)) return;
                    Resources.Add(SelectedResource.Resource);
                }));
            }
        }

        public IReactiveDerivedList<ResourceInfoViewModel> AvailableResources
        {
            get { return availableResources; }
            private set { RaiseSetIfChanged(ref availableResources, value); }
        }

        public ResourceBar ResourceBar
        {
            get { return RuleSetViewModel.RuleSet.ResourceBar; }
        }

        public ReactiveList<ResourceViewModel> Resources
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
            Resources = new ReactiveList<ResourceViewModel>(ResourceBar.Resources.Select(r => (ResourceViewModel)RuleSetViewModel.ElementList.First(e => e is ResourceViewModel && e.Element == r)));
            Resources.BeforeItemsAdded.Subscribe(r => ResourceBar.Resources.Add(r.Resource));
            Resources.BeforeItemsRemoved.Subscribe(r => ResourceBar.Resources.Remove(r.Resource));
        }
    }
}
