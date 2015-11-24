using System.Linq;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using ReactiveUI;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels.ResourceViewModels;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels.WorldResourceViewModels
{
    public class WorldResourceEditViewModel : RuleSetViewModelBase
    {
        private ReactiveProperty<int> amountPropety;
        private ReactiveProperty<string> nameProperty;
        private ReactiveProperty<ResourceInfoViewModel> resourceProperty;
        private IReactiveDerivedList<ResourceInfoViewModel> resources;
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

        public ReactiveProperty<ResourceInfoViewModel> Resource
        {
            get { return resourceProperty; }
            private set { RaiseSetIfChanged(ref resourceProperty, value); }
        }

        public IReactiveDerivedList<ResourceInfoViewModel> Resources
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
                RaiseSetIfChanged(ref worldResource, value);
                Name = WorldResource.Name;
                Amount = WorldResource.Amount;
                Resource = WorldResource.Resource.ToReactivePropertyAsSynchronized(w => w.Value, w => Resources.SingleOrDefault(r => r.Resource == worldResource.Resource.Value), w => w?.Resource);
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
            Resources = RuleSetViewModel.ElementList.CreateDerivedCollection(e => new ResourceInfoViewModel() { Resource = (ResourceViewModel)e, RuleSetViewModel = RuleSetViewModel }, e => e is ResourceViewModel);
        }
    }
}
