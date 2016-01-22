using System.Linq;
using Reactive.Bindings;
using ReactiveUI;
using RuleSet.Elements;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels;

namespace RuleSetEditor.ViewModels.RuleSetViewModels
{
    public class ResourcePartViewModel : RuleSetViewModelBase
    {
        private ReactiveProperty<int> amountProperty;
        private ResourcePart resourcePart;
        private ReactiveProperty<ResourceViewModel> resourceProperty;
        private IReactiveDerivedList<ResourceViewModel> resources;

        public ReactiveProperty<int> Amount
        {
            get { return amountProperty; }
            private set { RaiseSetIfChanged(ref amountProperty, value); }
        }

        public ReactiveProperty<ResourceViewModel> Resource
        {
            get { return resourceProperty; }
            private set { RaiseSetIfChanged(ref resourceProperty, value); }
        }

        public ResourcePart ResourcePart
        {
            get
            {
                return resourcePart;
            }
            set
            {
                if (!RaiseSetIfChanged(ref resourcePart, value))
                    return;
                if (!DeferChanged)
                    OnResourcePartChanged();
                else
                    DeferQueue.Enqueue(OnResourcePartChanged);
            }
        }

        public IReactiveDerivedList<ResourceViewModel> Resources
        {
            get { return resources; }
            private set { RaiseSetIfChanged(ref resources, value); }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Amount?.Dispose();
                Resource?.Value?.Dispose();
                Resource?.Dispose();
                Resources?.Dispose();

                resources = null;
                amountProperty = null;
                resourceProperty = null;
            }
            base.Dispose(disposing);
        }

        protected override void OnRuleSetChanged()
        {
            base.OnRuleSetChanged();
            Resources = RuleSetViewModel.ElementList.CreateDerivedCollection(e => (ResourceViewModel)e, e => e is ResourceViewModel);
        }

        private void OnResourcePartChanged()
        {
            Amount = ReactiveProperty.FromObject(ResourcePart, p => p.Amount);
            Amount.PropertyChanged += OnPropertyChanged;
            Resource = ReactiveProperty.FromObject(ResourcePart,
                p => p.Resource,
                p => Resources.SingleOrDefault(e => e.Resource == p),
                p => p?.Resource);
            Resource.PropertyChanged += OnPropertyChanged;
        }
    }
}
