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
            get
            {
                return amountProperty;
            }
            private set
            {
                RaiseSetIfChanged(ref amountProperty, value);
            }
        }

        public ReactiveProperty<ResourceViewModel> Resource
        {
            get
            {
                return resource;
            }
            private set
            {
                RaiseSetIfChanged(ref resource, value);
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
        
        protected override void OnInitialize()
        {
            base.OnInitialize();

            Amount = ReactiveProperty.FromObject(Need,
                r => r.Amount);
        }

        protected override void OnPostInitialize()
        {
            base.OnPostInitialize();

            Resources = RuleSetViewModel.ElementList.CreateDerivedCollection(e => (ResourceViewModel)e, e => e is ResourceViewModel, (l, r) => l.Name.Value.CompareTo(r.Name.Value));
            Resources.ChangeTrackingEnabled = true;

            Resource = ReactiveProperty.FromObject(Need,
                r => r.Resource,
                r => Resources.SingleOrDefault(e => e.Element == r),
                r => r?.Element);
            Resource.PropertyChanged += OnPropertyChanged;
        }
    }
}
