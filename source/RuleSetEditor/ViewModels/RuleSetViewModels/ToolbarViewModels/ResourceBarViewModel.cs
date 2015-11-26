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
        private RelayCommand removeResourceCommand;
        private ReactiveList<ResourceInfoViewModel> resources;
        private ResourceInfoViewModel selectedResource;

        public RelayCommand AddResourceCommand
        {
            get
            {
                return addResourceCommand ?? (addResourceCommand = new RelayCommand(() =>
                {
                    if (Resources.Contains(SelectedResource)) return;
                    Resources.Add(SelectedResource);
                }));
            }
        }

        public IReactiveDerivedList<ResourceInfoViewModel> AvailableResources
        {
            get { return availableResources; }
            private set { RaiseSetIfChanged(ref availableResources, value); }
        }

        public RelayCommand RemoveResourceCommand
        {
            get
            {
                return removeResourceCommand ?? (removeResourceCommand = new RelayCommand(() =>
                {
                    Resources.Remove(SelectedResource);
                }));
            }
        }

        public ResourceBar ResourceBar
        {
            get { return RuleSetViewModel.RuleSet.ResourceBar; }
        }

        public ReactiveList<ResourceInfoViewModel> Resources
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
            AvailableResources = RuleSetViewModel.ElementList.CreateDerivedCollection(e => new ResourceInfoViewModel()
            {
                Resource = (ResourceViewModel)e,
                RuleSetViewModel = RuleSetViewModel
            }, e => e is ResourceViewModel, (l, r) => l.Name.Value.CompareTo(r.Name.Value));
            Resources = new ReactiveList<ResourceInfoViewModel>(ResourceBar.Resources.Select(r => new ResourceInfoViewModel()
            {
                Resource = (ResourceViewModel)RuleSetViewModel.ElementList.FirstOrDefault(e => e is ResourceViewModel && e.Element == r),
                RuleSetViewModel = RuleSetViewModel
            }).Where(r => r.Resource != null));
            Resources.BeforeItemsAdded.Subscribe(r => ResourceBar.Resources.Add(r.Resource.Resource));
            Resources.BeforeItemsRemoved.Subscribe(r => ResourceBar.Resources.Remove(r.Resource.Resource));
        }
    }
}
