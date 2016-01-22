using System.Linq;
using Reactive.Bindings;
using ReactiveUI;
using RuleSet.Needs;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.NeedViewModels
{
    public class ResourceNeedViewModel : NeedViewModel<ResourceNeed>
    {
        private ReactiveProperty<int> amountProperty;
        private ReactiveProperty<ResourceViewModel> resource;
        private IReactiveDerivedList<ResourceViewModel> resources;

        public ReactiveProperty<int> Amount
        {
            get { return amountProperty; }
            private set { RaiseSetIfChanged(ref amountProperty, value); }
        }

        public ReactiveProperty<ResourceViewModel> Resource
        {
            get { return resource; }
            private set { RaiseSetIfChanged(ref resource, value); }
        }

        public IReactiveDerivedList<ResourceViewModel> Resources
        {
            get { return resources; }
            private set { RaiseSetIfChanged(ref resources, value); }
        }

        public override string ToString()
        {
            return $"Need Resource {{{Resource?.Value?.Name?.Value ?? "None"}}}";
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Resource?.Value?.Dispose();
                Resource?.Dispose();
                Resources.Dispose();

                resources = null;
                resource = null;
            }
            base.Dispose(disposing);
        }

        protected override void OnElementChanged()
        {
            base.OnElementChanged();
            Amount = ReactiveProperty.FromObject(Need,
                r => r.Amount);
            Resource = ReactiveProperty.FromObject(Need,
                r => r.Resource,
                r => Resources.SingleOrDefault(e => e.Resource == r),
                r => r?.Resource);
            Resource.PropertyChanged += OnPropertyChanged;
        }

        protected override void OnRuleSetChanged()
        {
            base.OnRuleSetChanged();
            Resources = RuleSetViewModel.ElementList.CreateDerivedCollection(e => (ResourceViewModel)e, e => e is ResourceViewModel);
        }
    }
}
