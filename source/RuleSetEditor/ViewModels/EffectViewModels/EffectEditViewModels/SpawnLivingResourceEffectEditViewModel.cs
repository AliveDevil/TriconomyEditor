using System.Linq;
using Reactive.Bindings;
using ReactiveUI;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels;

namespace RuleSetEditor.ViewModels.EffectViewModels.EffectEditViewModels
{
    public class SpawnLivingResourceEffectEditViewModel : RuleSetViewModelBase
    {
        private ReactiveProperty<int> amountProperty;
        private ReactiveProperty<int> delayProperty;
        private ReactiveProperty<LivingResourceViewModel> livingResource;
        private IReactiveDerivedList<LivingResourceViewModel> livingResources;
        private ReactiveProperty<int> radiusProperty;
        private SpawnLivingResourceEffectViewModel spawnWorldResourceEffect;

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

        public SpawnLivingResourceEffectViewModel SpawnLivingResourceEffect
        {
            get
            {
                return spawnWorldResourceEffect;
            }
            set
            {
                if (!RaiseSetIfChanged(ref spawnWorldResourceEffect, value))
                    return;
                Amount = ReactiveProperty.FromObject(SpawnLivingResourceEffect.Effect, e => e.Amount);
                Delay = ReactiveProperty.FromObject(SpawnLivingResourceEffect.Effect, e => e.Delay);
                Radius = ReactiveProperty.FromObject(SpawnLivingResourceEffect.Effect, e => e.Radius);
                LivingResource = ReactiveProperty.FromObject(SpawnLivingResourceEffect.Effect,
                    e => e.LivingResource,
                    e => LivingResources.SingleOrDefault(s => s.Element == e),
                    e => e?.Element);
            }
        }

        protected override void OnRuleSetChanged()
        {
            base.OnRuleSetChanged();

            LivingResources = RuleSetViewModel.ElementList.CreateDerivedCollection(
                e => (LivingResourceViewModel)e,
                e => e is LivingResourceViewModel,
                (l, r) => l.Name.Value.CompareTo(r.Name.Value));
            LivingResources.ChangeTrackingEnabled = true;
        }
    }
}
