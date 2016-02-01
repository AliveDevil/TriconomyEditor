using System.Linq;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using ReactiveUI;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels.LivingResourceViewModels
{
    public class LivingResourceEditViewModel : RuleSetViewModelBase
    {
        private ReactiveProperty<int> amountProperty;
        private ReactiveProperty<bool> autoSpawnProperty;
        private LivingResourceViewModel livingResource;
        private ReactiveProperty<string> nameProperty;
        private ReactiveProperty<ResourceViewModel> resourceProperty;
        private IReactiveDerivedList<ResourceViewModel> resources;
        private ReactiveProperty<int> variantsProperty;

        public ReactiveProperty<int> Amount
        {
            get
            {
                return amountProperty;
            }
            private set
            {
                RaiseSetIfChanged(ref amountProperty, value);
            }
        }

        public ReactiveProperty<bool> AutoSpawn
        {
            get
            {
                return autoSpawnProperty;
            }
            private set
            {
                RaiseSetIfChanged(ref autoSpawnProperty, value);
            }
        }

        public LivingResourceViewModel LivingResource
        {
            get
            {
                return livingResource;
            }
            set
            {
                if (!RaiseSetIfChanged(ref livingResource, value))
                    return;
                Name = LivingResource.Name;
                Amount = LivingResource.Amount;
                AutoSpawn = LivingResource.AutoSpawn;
                Resource = LivingResource.Resource.ToReactivePropertyAsSynchronized(w => w.Value, w => Resources.SingleOrDefault(r => r == livingResource.Resource.Value), w => w);
            }
        }

        public ReactiveProperty<string> Name
        {
            get
            {
                return nameProperty;
            }
            private set
            {
                RaiseSetIfChanged(ref nameProperty, value);
            }
        }

        public ReactiveProperty<ResourceViewModel> Resource
        {
            get
            {
                return resourceProperty;
            }
            private set
            {
                RaiseSetIfChanged(ref resourceProperty, value);
            }
        }

        public IReactiveDerivedList<ResourceViewModel> Resources
        {
            get
            {
                return resources;
            }
            private set
            {
                RaiseSetIfChanged(ref resources, value);
            }
        }

        public ReactiveProperty<int> Variants
        {
            get
            {
                return variantsProperty;
            }
            private set
            {
                RaiseSetIfChanged(ref variantsProperty, value);
            }
        }

        public override string ToString()
        {
            return $"{Name.Value}";
        }

        protected override void OnRuleSetChanged()
        {
            base.OnRuleSetChanged();
            Resources = RuleSetViewModel.ElementList.CreateDerivedCollection(e => (ResourceViewModel)e, e => e is ResourceViewModel, (l, r) => l.Name.Value.CompareTo(r.Name.Value));
            Resources.ChangeTrackingEnabled = true;
        }
    }
}
