using System.Linq;
using Reactive.Bindings;
using ReactiveUI;
using RuleSet.Effects;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels;

namespace RuleSetEditor.ViewModels.EffectViewModels
{
    public class GatherResourceEffectViewModel : EffectViewModel<GatherResourceEffect>
    {
        private ReactiveProperty<int> radiusProperty;
        private ReactiveProperty<ResourceViewModel> resource;
        private IReactiveDerivedList<ResourceViewModel> resources;

        public ReactiveProperty<int> Radius
        {
            get
            {
                return radiusProperty;
            }
            private set
            {
                RaiseSetIfChanged(ref radiusProperty, value);
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

            Radius = ReactiveProperty.FromObject(Effect, e => e.Radius);
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
