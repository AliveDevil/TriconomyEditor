using System.Linq;
using Reactive.Bindings;
using ReactiveUI;
using RuleSet.Effects;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels;

namespace RuleSetEditor.ViewModels.EffectViewModels
{
    public class ProduceResourceEffectViewModel : EffectViewModel<ProduceResourceEffect>
    {
        private ReactiveProperty<int> amountProperty;
        private ReactiveProperty<bool> cheatModeProperty;
        private ReactiveProperty<int> delayProperty;
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

        public ReactiveProperty<bool> CheatMode
        {
            get
            {
                return cheatModeProperty;
            }
            private set
            {
                RaiseSetIfChanged(ref cheatModeProperty, value);
            }
        }

        public ReactiveProperty<int> Delay
        {
            get
            {
                return delayProperty;
            }
            private set
            {
                RaiseSetIfChanged(ref delayProperty, value);
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

            Amount = ReactiveProperty.FromObject(Effect, e => e.Amount);
            Delay = ReactiveProperty.FromObject(Effect, e => e.Delay);
            CheatMode = ReactiveProperty.FromObject(Effect, e => e.CheatModeOnly);
        }

        protected override void OnPostInitialize()
        {
            base.OnPostInitialize();

            Resources = RuleSetViewModel.ElementList.CreateDerivedCollection(e => (ResourceViewModel)e, e => e is ResourceViewModel, (l, r) => l.Name.Value.CompareTo(r.Name.Value));
            Resources.ChangeTrackingEnabled = true;

            Resource = ReactiveProperty.FromObject(Effect,
                    r => r.Resource,
                    r => Resources.SingleOrDefault(e => e.Element == r),
                    r => r?.Element);
            Resource.PropertyChanged += OnPropertyChanged;
        }
    }
}
