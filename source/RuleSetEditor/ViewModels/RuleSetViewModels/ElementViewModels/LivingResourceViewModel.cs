using System.Linq;
using Reactive.Bindings;
using ReactiveUI;
using RuleSet.Elements;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels
{
    public class LivingResourceViewModel : ElementViewModel<LivingResource>
    {
        private ReactiveProperty<int> amountProperty;
        private ReactiveProperty<bool> autoSpawnProperty;
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

        public ReactiveProperty<bool> AutoSpawn
        {
            get
            {
                return autoSpawnProperty;
            }
            set
            {
                RaiseSetIfChanged(ref autoSpawnProperty, value);
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

            Amount = ReactiveProperty.FromObject(Element, w => w.Amount);
            Amount.PropertyChanged += OnPropertyChanged;
            AutoSpawn = ReactiveProperty.FromObject(Element, w => w.AutoSpawn);
            AutoSpawn.PropertyChanged += OnPropertyChanged;
        }

        protected override void OnPostInitialize()
        {
            base.OnPostInitialize();

            Resources = RuleSetViewModel.ElementList.CreateDerivedCollection(e => (ResourceViewModel)e, e => e is ResourceViewModel, (l, r) => l.Name.Value.CompareTo(r.Name.Value));
            Resources.ChangeTrackingEnabled = true;

            Resource = ReactiveProperty.FromObject(Element,
                r => r.Resource,
                r => Resources.SingleOrDefault(e => e.Element == r),
                r => r?.Element);
            Resource.PropertyChanged += OnPropertyChanged;
        }
    }
}
