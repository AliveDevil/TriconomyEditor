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
        private IReactiveDerivedList<ResourceViewModel> availableResources;
        private IDisposable beforeItemsAdded;
        private IDisposable beforeItemsRemoved;
        private ReactiveList<ResourceViewModel> resources;
        private ResourceViewModel selectedResource;

        public RelayCommand AddResourceCommand => new RelayCommand(() =>
        {
            if (Resources.Contains(SelectedResource)) return;
            Resources.Add(SelectedResource);
        });

        public IReactiveDerivedList<ResourceViewModel> AvailableResources
        {
            get { return availableResources; }
            private set { RaiseSetIfChanged(ref availableResources, value); }
        }

        public RelayCommand RemoveResourceCommand => new RelayCommand(() =>
        {
            Resources.Remove(SelectedResource);
        });

        public ResourceBar ResourceBar
        {
            get { return RuleSetViewModel.RuleSet.ResourceBar ?? (RuleSetViewModel.RuleSet.ResourceBar = new ResourceBar()); }
        }

        public ReactiveList<ResourceViewModel> Resources
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
                Dispose(ref beforeItemsAdded);
                Dispose(ref beforeItemsRemoved);
                Dispose(ref availableResources);
                Resources.Clear();

                AddResourceCommand.Dispose();
                RemoveResourceCommand.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void OnRuleSetChanged()
        {
            base.OnRuleSetChanged();
            AvailableResources = RuleSetViewModel.ElementList.CreateDerivedCollection(e => (ResourceViewModel)e, e => e is ResourceViewModel, (l, r) => l.Name.Value.CompareTo(r.Name.Value));
            Resources = new ReactiveList<ResourceViewModel>(ResourceBar.Resources.Select(r => (ResourceViewModel)RuleSetViewModel.ElementList.FirstOrDefault(e => e is ResourceViewModel && e.Element == r)).Where(r => r.Resource != null));
            beforeItemsAdded = Resources.BeforeItemsAdded.Subscribe(r => ResourceBar.Resources.Add(r.Resource));
            beforeItemsRemoved = Resources.BeforeItemsRemoved.Subscribe(r => ResourceBar.Resources.Remove(r.Resource));
        }
    }
}
