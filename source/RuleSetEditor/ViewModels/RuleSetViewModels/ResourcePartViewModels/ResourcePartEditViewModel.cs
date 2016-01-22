using Reactive.Bindings;
using ReactiveUI;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ResourcePartViewModels
{
    public class ResourcePartEditViewModel : RuleSetViewModelBase
    {
        private ReactiveProperty<int> amountProperty;
        private ResourcePartViewModel resourcePart;
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

        public ResourcePartViewModel ResourcePart
        {
            get
            {
                return resourcePart;
            }
            set
            {
                if (!RaiseSetIfChanged(ref resourcePart, value))
                    return;
                Amount = ResourcePart.Amount;
                Amount.PropertyChanged += OnPropertyChanged;
                Resource = ResourcePart.Resource;
                Resource.PropertyChanged += OnPropertyChanged;
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
                Amount.PropertyChanged -= OnPropertyChanged;
                Resource.PropertyChanged -= OnPropertyChanged;

                Resources?.Dispose();
                resources = null;
            }
            base.Dispose(disposing);
        }

        protected override void OnRuleSetChanged()
        {
            base.OnRuleSetChanged();
            Resources = RuleSetViewModel.ElementList.CreateDerivedCollection(e => (ResourceViewModel)e, e => e is ResourceViewModel, (l, r) => l.Name.Value.CompareTo(r.Name.Value));
        }
    }
}
