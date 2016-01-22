using System.Linq;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using ReactiveUI;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.NeedViewModels.EditViewModels
{
    public class ResourceNeedEditViewModel : RuleSetViewModelBase
    {
        private ReactiveProperty<int> amountProperty;
        private ResourceNeedViewModel resourceNeed;
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

        public ResourceNeedViewModel ResourceNeed
        {
            get
            {
                return resourceNeed;
            }
            set
            {
                if (!RaiseSetIfChanged(ref resourceNeed, value))
                    return;
                Amount = ResourceNeed.Amount;
                Resource = ResourceNeed.Resource.ToReactivePropertyAsSynchronized(
                    r => r.Value,
                    w => Resources.SingleOrDefault(r => r == ResourceNeed.Resource.Value),
                    r => r);
            }
        }

        public IReactiveDerivedList<ResourceViewModel> Resources
        {
            get { return resources; }
            private set { RaiseSetIfChanged(ref resources, value); }
        }

        protected override void OnRuleSetChanged()
        {
            base.OnRuleSetChanged();
            Resources = RuleSetViewModel.ElementList.CreateDerivedCollection(e => (ResourceViewModel)e, e => e is ResourceViewModel, (l, r) => l.Name.Value.CompareTo(r.Name.Value));
            Resources.ChangeTrackingEnabled = true;
        }
    }
}
