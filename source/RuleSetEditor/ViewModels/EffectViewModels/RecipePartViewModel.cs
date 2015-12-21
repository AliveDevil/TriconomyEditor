using System.Linq;
using Reactive.Bindings;
using ReactiveUI;
using RuleSet.Elements;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels;

namespace RuleSetEditor.ViewModels.EffectViewModels
{
    public class RecipePartViewModel : RuleSetViewModelBase
    {
        private ReactiveProperty<int> amountProperty;
        private RecipePart recipePart;
        private ReactiveProperty<ResourceViewModel> resource;
        private IReactiveDerivedList<ResourceViewModel> resources;

        public ReactiveProperty<int> Amount
        {
            get { return amountProperty; }
            private set { RaiseSetIfChanged(ref amountProperty, value); }
        }

        public RecipePart RecipePart
        {
            get
            {
                return recipePart;
            }
            set
            {
                if (!RaiseSetIfChanged(ref recipePart, value)) return;
                Amount = ReactiveProperty.FromObject(RecipePart, r => r.Amount);
                Resource = ReactiveProperty.FromObject(RecipePart,
                    r => r.Resource,
                    r => Resources.SingleOrDefault(e => e.Resource == r),
                    r => r?.Resource);
            }
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

        protected override void OnRuleSetChanged()
        {
            base.OnRuleSetChanged();

            Resources = RuleSetViewModel.ElementList.CreateDerivedCollection(e => (ResourceViewModel)e, e => e is ResourceViewModel, (l, r) => l.Name.Value.CompareTo(r.Name.Value));
            Resources.ChangeTrackingEnabled = true;
        }
    }
}
