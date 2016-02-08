using System.Linq;
using Reactive.Bindings;
using ReactiveUI;
using RuleSet.Effects;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels;

namespace RuleSetEditor.ViewModels.EffectViewModels
{
    public class SpawnWorldResourceEffectViewModel : EffectViewModel<SpawnWorldResourceEffect>
    {
        private ReactiveProperty<int> amountProperty;
        private ReactiveProperty<int> delayProperty;
        private ReactiveProperty<int> radiusProperty;
        private ReactiveProperty<WorldResourceViewModel> worldResource;
        private IReactiveDerivedList<WorldResourceViewModel> worldResources;

        public ReactiveProperty<int> Amount
        {
            get
            {
                return amountProperty;
            }
            set
            {
                amountProperty = value;
            }
        }

        public ReactiveProperty<int> Delay
        {
            get
            {
                return delayProperty;
            }
            set
            {
                delayProperty = value;
            }
        }

        public ReactiveProperty<int> Radius
        {
            get
            {
                return radiusProperty;
            }
            set
            {
                radiusProperty = value;
            }
        }

        public ReactiveProperty<WorldResourceViewModel> WorldResource
        {
            get
            {
                return worldResource;
            }
            private set
            {
                RaiseSetIfChanged(ref worldResource, value);
            }
        }

        public IReactiveDerivedList<WorldResourceViewModel> WorldResources
        {
            get
            {
                return worldResources;
            }
            private set
            {
                RaiseSetIfChanged(ref worldResources, value);
            }
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Amount = ReactiveProperty.FromObject(Effect, e => e.Amount);
            Delay = ReactiveProperty.FromObject(Effect, e => e.Delay);
            Radius = ReactiveProperty.FromObject(Effect, e => e.Radius);
        }

        protected override void OnPostInitialize()
        {
            base.OnPostInitialize();

            WorldResources = RuleSetViewModel.ElementList.CreateDerivedCollection(
                e => (WorldResourceViewModel)e,
                e => e is WorldResourceViewModel,
                (l, r) => l.Name.Value.CompareTo(r.Name.Value));
            WorldResources.ChangeTrackingEnabled = true;

            WorldResource = ReactiveProperty.FromObject(Effect,
                    e => e.WorldResource,
                    e => WorldResources.SingleOrDefault(s => s.Element == e),
                    e => e?.Element);
            WorldResource.PropertyChanged += OnPropertyChanged;
        }
    }
}
