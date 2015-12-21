using System.Linq;
using Reactive.Bindings;
using ReactiveUI;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels;

namespace RuleSetEditor.ViewModels.EffectViewModels.EffectEditViewModels
{
    public class GatherResourceEffectEditViewModel : RuleSetViewModelBase
    {
        private GatherResourceEffectViewModel gatherResourceEffect;
        private ReactiveProperty<int> radiusProperty;
        private ReactiveProperty<ResourceViewModel> resource;
        private IReactiveDerivedList<ResourceViewModel> resources;

        public GatherResourceEffectViewModel GatherResourceEffect
        {
            get
            {
                return gatherResourceEffect;
            }
            set
            {
                if (!RaiseSetIfChanged(ref gatherResourceEffect, value)) return;
                Radius = ReactiveProperty.FromObject(GatherResourceEffect.GatherResourceEffect, e => e.Radius);
                Resource = ReactiveProperty.FromObject(GatherResourceEffect.GatherResourceEffect,
                    r => r.Resource,
                    r => Resources.SingleOrDefault(e => e.Resource == r),
                    r => r?.Resource);
            }
        }

        public ReactiveProperty<int> Radius
        {
            get { return radiusProperty; }
            private set { RaiseSetIfChanged(ref radiusProperty, value); }
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
