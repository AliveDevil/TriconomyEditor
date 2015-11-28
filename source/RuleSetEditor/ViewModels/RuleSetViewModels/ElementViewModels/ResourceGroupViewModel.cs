using System;
using System.Linq;
using ReactiveUI;
using RuleSet.Elements;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels
{
    public class ResourceGroupViewModel : ResourceViewModel
    {
        private IDisposable beforeItemsAdded;
        private IDisposable beforeItemsRemoved;
        private ReactiveList<ResourceViewModel> resourcesList;

        public ResourceGroup ResourceGroup => (ResourceGroup)Element;

        public ReactiveList<ResourceViewModel> ResourceList
        {
            get { return resourcesList; }
            private set { RaiseSetIfChanged(ref resourcesList, value); }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                beforeItemsAdded.Dispose();
                beforeItemsRemoved.Dispose();
                ResourceList.Clear();

                resourcesList = null;
            }
            base.Dispose(disposing);
        }

        protected override void OnElementChanged()
        {
            base.OnElementChanged();
            ResourceList = new ReactiveList<ResourceViewModel>(ResourceGroup.Resources.Select(r => (ResourceViewModel)RuleSetViewModel.ElementList.First(e => e is ResourceViewModel && e.Element == r)));
            ResourceList.ChangeTrackingEnabled = true;
            beforeItemsAdded = ResourceList.BeforeItemsAdded.Subscribe(r => ResourceGroup.Resources.Add(r.Resource));
            beforeItemsRemoved = ResourceList.BeforeItemsRemoved.Subscribe(r => ResourceGroup.Resources.Remove(r.Resource));
        }
    }
}
