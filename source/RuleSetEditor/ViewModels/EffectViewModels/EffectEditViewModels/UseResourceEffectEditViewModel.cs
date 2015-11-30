using Reactive.Bindings;
using ReactiveUI;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels;

namespace RuleSetEditor.ViewModels.EffectViewModels.EffectEditViewModels
{
    public class UseResourceEffectEditViewModel : RuleSetViewModelBase
    {
        private ReactiveProperty<int> amountProperty;
        private ReactiveProperty<ResourceViewModel> resource;
        private IReactiveDerivedList<ResourceViewModel> resources;
        private UseResourceEffectViewModel useResourceEffect;

        public ReactiveProperty<int> Amount
        {
            get { return amountProperty; }
            private set { RaiseSetIfChanged(ref amountProperty, value); }
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

        public UseResourceEffectViewModel UseResourceEffect
        {
            get
            {
                return useResourceEffect;
            }
            set
            {
                if (!RaiseSetIfChanged(ref useResourceEffect, value)) return;
                Amount = UseResourceEffect.Amount;
                Resource = UseResourceEffect.Resource;
                Resources = UseResourceEffect.Resources;
            }
        }
    }
}
