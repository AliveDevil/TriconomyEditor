using System.Linq;
using Reactive.Bindings;
using ReactiveUI;
using RuleSet.Elements;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels
{
    public class WorldResourceViewModel : ElementViewModel
    {
        private ReactiveProperty<int> amountProperty;
        private ReactiveProperty<ResourceViewModel> resource;
        private IReactiveDerivedList<ResourceViewModel> resources;
        private ReactiveProperty<int> variantsProperty;

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

        public ReactiveProperty<int> Variants
        {
            get { return variantsProperty; }
            private set { RaiseSetIfChanged(ref variantsProperty, value); }
        }

        public WorldResource WorldResource => (WorldResource)Element;

        protected override void OnElementChanged()
        {
            base.OnElementChanged();
            Amount = ReactiveProperty.FromObject(WorldResource, w => w.Amount);
            Resource = ReactiveProperty.FromObject(WorldResource,
                r => r.Resource,
                r => Resources.SingleOrDefault(e => e.Resource == r),
                r => r?.Resource);
            Variants = ReactiveProperty.FromObject(WorldResource, r => r.Variants);
        }

        protected override void OnRuleSetChanged()
        {
            base.OnRuleSetChanged();
            Resources = RuleSetViewModel.ElementList.CreateDerivedCollection(e => (ResourceViewModel)e, e => e is ResourceViewModel);
        }
    }
}
