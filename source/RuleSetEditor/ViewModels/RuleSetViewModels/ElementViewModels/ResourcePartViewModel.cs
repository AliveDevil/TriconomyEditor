using System.Linq;
using Reactive.Bindings;
using ReactiveUI;
using RuleSet.Elements;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels
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
            }
        }

        public IReactiveDerivedList<ResourceViewModel> Resources
        {
            get { return resources; }
            private set { RaiseSetIfChanged(ref resources, value); }
        }
        
        protected override void OnInitialize()
        {
            base.OnInitialize();

            Amount = ReactiveProperty.FromObject(ResourcePart, p => p.Amount);
            Amount.PropertyChanged += OnPropertyChanged;
        }
        protected override void OnPostInitialize()
        {
            base.OnPostInitialize();

            Resources = RuleSetViewModel.ElementList.CreateDerivedCollection(e => (ResourceViewModel)e, e => e is ResourceViewModel, (l, r) => l.Name.Value.CompareTo(r.Name.Value));
            Resources.ChangeTrackingEnabled = true;

            Resource = ReactiveProperty.FromObject(ResourcePart,
                p => p.Resource,
                p => Resources.SingleOrDefault(e => e.Element == p),
                p => p?.Element);
            Resource.PropertyChanged += OnPropertyChanged;
        }
    }
}
