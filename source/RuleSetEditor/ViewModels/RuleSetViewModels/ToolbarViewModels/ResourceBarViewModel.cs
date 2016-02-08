using System;
using System.Linq;
using ReactiveUI;
using RuleSet.Menus;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ToolbarViewModels
{
    public class ResourceBarViewModel : RuleSetViewModelBase
    {
        private IReactiveDerivedList<ResourceViewModel> availableResources;
        private IDisposable itemsAdded;
        private IDisposable itemsRemoved;
        private ReactiveList<ResourceViewModel> resources;
        private ResourceViewModel selectedResource;

        public RelayCommand AddResourceCommand => new RelayCommand(() =>
        {
            if (Resources.Contains(SelectedResource))
                return;
            Resources.Add(SelectedResource);
        });

        public IReactiveDerivedList<ResourceViewModel> AvailableResources
        {
            get
            {
                return availableResources;
            }
            private set
            {
                RaiseSetIfChanged(ref availableResources, value);
            }
        }

        public RelayCommand RemoveResourceCommand => new RelayCommand(() =>
        {
            Resources.Remove(SelectedResource);
        });

        public ResourceBar ResourceBar
        {
            get
            {
                return RuleSetViewModel.RuleSet.ResourceBar ?? (RuleSetViewModel.RuleSet.ResourceBar = new ResourceBar());
            }
        }

        public ReactiveList<ResourceViewModel> Resources
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
        
        protected override void OnInitialize()
        {
            base.OnInitialize();

            AvailableResources = RuleSetViewModel.ElementList.CreateDerivedCollection(e => (ResourceViewModel)e, e => e is ResourceViewModel, (l, r) => l.Name.Value.CompareTo(r.Name.Value));
            Resources = new ReactiveList<ResourceViewModel>();
            Resources.ChangeTrackingEnabled = true;
        }

        protected override void OnPostInitialize()
        {
            base.OnPostInitialize();

            foreach (var item in ResourceBar.Resources)
            {
                Resources.Add(RuleSetViewModel.ElementList.OfType<ResourceViewModel>().First(e => e.Element == item));
            }

            itemsAdded = Resources.ItemsAdded.Subscribe(r => ResourceBar.Resources.Add(r.Element));
            itemsRemoved = Resources.ItemsRemoved.Subscribe(r => ResourceBar.Resources.Remove(r.Element));
        }
    }
}
