using System.Linq;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using ReactiveUI;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels.WorldResourceViewModels
{
    public class WorldResourceEditViewModel : RuleSetViewModelBase
    {
        private ReactiveProperty<int> amountPropety;
        private ReactiveProperty<string> nameProperty;
        private ReactiveProperty<ResourceViewModel> resourceProperty;
        private IReactiveDerivedList<ResourceViewModel> resources;
        private ReactiveProperty<int> variantsProperty;
        private WorldResourceViewModel worldResource;

        public ReactiveProperty<int> Amount
        {
            get { return amountPropety; }
            private set { RaiseSetIfChanged(ref amountPropety, value); }
        }

        public ReactiveProperty<string> Name
        {
            get { return nameProperty; }
            private set { RaiseSetIfChanged(ref nameProperty, value); }
        }

        public ReactiveProperty<ResourceViewModel> Resource
        {
            get { return resourceProperty; }
            private set { RaiseSetIfChanged(ref resourceProperty, value); }
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

        public WorldResourceViewModel WorldResource
        {
            get
            {
                return worldResource;
            }
            set
            {
                if (!RaiseSetIfChanged(ref worldResource, value))
                    return;
                Name = WorldResource.Name;
                Amount = WorldResource.Amount;
                Resource = WorldResource.Resource.ToReactivePropertyAsSynchronized(w => w.Value, w => Resources.SingleOrDefault(r => r == worldResource.Resource.Value), w => w);
                Variants = WorldResource.Variants;
            }
        }

        public override string ToString()
        {
            return $"{Name.Value} {{{Resource?.Value?.Resource?.Name}}}";
        }

        protected override void OnRuleSetChanged()
        {
            base.OnRuleSetChanged();
            Resources = RuleSetViewModel.ElementList.CreateDerivedCollection(e => (ResourceViewModel)e, e => e is ResourceViewModel, (l, r) => l.Name.Value.CompareTo(r.Name.Value));
            Resources.ChangeTrackingEnabled = true;
        }
    }
}
