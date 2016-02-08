using System.Linq;
using Reactive.Bindings;
using ReactiveUI;
using RuleSet.Effects;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels;

namespace RuleSetEditor.ViewModels.EffectViewModels
{
    public class SpawnLivingResourceEffectViewModel : EffectViewModel<SpawnLivingResourceEffect>
    {
        private ReactiveProperty<int> amountProperty;
        private ReactiveProperty<int> delayProperty;
        private ReactiveProperty<LivingResourceViewModel> livingResource;
        private IReactiveDerivedList<LivingResourceViewModel> livingResources;
        private ReactiveProperty<int> radiusProperty;

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

        public ReactiveProperty<LivingResourceViewModel> LivingResource
        {
            get
            {
                return livingResource;
            }
            private set
            {
                RaiseSetIfChanged(ref livingResource, value);
            }
        }

        public IReactiveDerivedList<LivingResourceViewModel> LivingResources
        {
            get
            {
                return livingResources;
            }
            private set
            {
                RaiseSetIfChanged(ref livingResources, value);
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

            LivingResources = RuleSetViewModel.ElementList.CreateDerivedCollection(
                e => (LivingResourceViewModel)e,
                e => e is LivingResourceViewModel,
                (l, r) => l.Name.Value.CompareTo(r.Name.Value));
            LivingResources.ChangeTrackingEnabled = true;

            LivingResource = ReactiveProperty.FromObject(Effect,
                e => e.LivingResource,
                e => LivingResources.SingleOrDefault(s => s.Element == e),
                e => e?.Element);
            LivingResource.PropertyChanged += OnPropertyChanged;
        }
    }
}
