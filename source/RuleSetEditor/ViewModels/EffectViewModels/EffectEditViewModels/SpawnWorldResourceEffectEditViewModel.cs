using System.Linq;
using Reactive.Bindings;
using ReactiveUI;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels;

namespace RuleSetEditor.ViewModels.EffectViewModels.EffectEditViewModels
{
    public class SpawnWorldResourceEffectEditViewModel : RuleSetViewModelBase
    {
        private ReactiveProperty<int> amountProperty;
        private ReactiveProperty<int> delayProperty;
        private ReactiveProperty<int> radiusProperty;
        private SpawnWorldResourceEffectViewModel spawnWorldResourceEffect;
        private ReactiveProperty<WorldResourceViewModel> worldResource;
        private IReactiveDerivedList<WorldResourceViewModel> worldResources;

        public ReactiveProperty<int> Amount
        {
            get { return amountProperty; }
            set { amountProperty = value; }
        }

        public ReactiveProperty<int> Delay
        {
            get { return delayProperty; }
            set { delayProperty = value; }
        }

        public ReactiveProperty<int> Radius
        {
            get { return radiusProperty; }
            set { radiusProperty = value; }
        }

        public SpawnWorldResourceEffectViewModel SpawnWorldResourceEffect
        {
            get
            {
                return spawnWorldResourceEffect;
            }
            set
            {
                if (!RaiseSetIfChanged(ref spawnWorldResourceEffect, value))
                    return;
                Amount = ReactiveProperty.FromObject(SpawnWorldResourceEffect.SpawnWorldResourceEffect, e => e.Amount);
                Delay = ReactiveProperty.FromObject(SpawnWorldResourceEffect.SpawnWorldResourceEffect, e => e.Delay);
                Radius = ReactiveProperty.FromObject(SpawnWorldResourceEffect.SpawnWorldResourceEffect, e => e.Radius);
                WorldResource = ReactiveProperty.FromObject(SpawnWorldResourceEffect.SpawnWorldResourceEffect,
                    e => e.WorldResource,
                    e => WorldResources.SingleOrDefault(s => s.WorldResource == e),
                    e => e?.WorldResource);
            }
        }

        public ReactiveProperty<WorldResourceViewModel> WorldResource
        {
            get { return worldResource; }
            private set { RaiseSetIfChanged(ref worldResource, value); }
        }

        public IReactiveDerivedList<WorldResourceViewModel> WorldResources
        {
            get { return worldResources; }
            private set { RaiseSetIfChanged(ref worldResources, value); }
        }

        protected override void OnRuleSetChanged()
        {
            base.OnRuleSetChanged();

            WorldResources = RuleSetViewModel.ElementList.CreateDerivedCollection(
                e => (WorldResourceViewModel)e,
                e => e is WorldResourceViewModel,
                (l, r) => l.Name.Value.CompareTo(r.Name.Value));
            WorldResources.ChangeTrackingEnabled = true;
        }
    }
}
