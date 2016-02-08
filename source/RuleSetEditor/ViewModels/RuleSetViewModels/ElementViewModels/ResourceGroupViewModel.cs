using System;
using System.Linq;
using ReactiveUI;
using RuleSet.Elements;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels
{
    public class ResourceGroupViewModel : ResourceViewModel<ResourceGroup>
    {
        private RelayCommand addResourceCommand;
        private IReactiveDerivedList<ResourceViewModel> availableResources;
        private IDisposable itemsAdded;
        private IDisposable itemsRemoved;
        private ReactiveList<ResourceViewModel> resourcesList;
        private ResourceViewModel selectedResource;

        public RelayCommand AddResourceCommand => addResourceCommand ?? (addResourceCommand = new RelayCommand(() =>
        {
            if (ResourceList.Contains(SelectedResource))
                return;
            ResourceList.Add(SelectedResource);
        }));

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

        public ReactiveList<ResourceViewModel> ResourceList
        {
            get
            {
                return resourcesList;
            }
            private set
            {
                RaiseSetIfChanged(ref resourcesList, value);
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

            AvailableResources = RuleSetViewModel.ElementList.CreateDerivedCollection(
                e => (ResourceViewModel)e,
                e => e is ResourceViewModel,
                (l, r) => l.Name.Value.CompareTo(r.Name.Value));
            AvailableResources.ChangeTrackingEnabled = true;
        }

        protected override void OnPostInitialize()
        {
            base.OnPostInitialize();

            ResourceList = new ReactiveList<ResourceViewModel>(Element.Resources.Select(r => AvailableResources.OfType<ResourceViewModel>().First(e => e.Element == r)));
            ResourceList.ChangeTrackingEnabled = true;

            itemsAdded = ResourceList.ItemsAdded.Subscribe(r => Element.Resources.Add(r.Element));
            itemsRemoved = ResourceList.ItemsRemoved.Subscribe(r => Element.Resources.Remove(r.Element));
        }
    }
}
