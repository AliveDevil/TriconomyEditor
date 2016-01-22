using System;
using System.Linq;
using Reactive.Bindings;
using ReactiveUI;
using RuleSet;
using RuleSetEditor.ViewModels.EffectViewModels;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels
{
    public class UpgradeViewModel : RuleSetViewModelBase
    {
        private IDisposable beforeCostAdded;
        private IDisposable beforeCostRemoved;
        private IDisposable beforeEffectAdded;
        private IDisposable beforeEffectRemoved;
        private ReactiveList<ResourcePartViewModel> costs;
        private ReactiveList<EffectViewModel> effects;
        private ReactiveProperty<int> levelProperty;
        private Upgrade upgrade;

        public ReactiveList<ResourcePartViewModel> Costs
        {
            get { return costs; }
            private set { RaiseSetIfChanged(ref costs, value); }
        }

        public ReactiveList<EffectViewModel> Effects
        {
            get { return effects; }
            private set { RaiseSetIfChanged(ref effects, value); }
        }

        public ReactiveProperty<int> Level
        {
            get { return levelProperty; }
            private set { levelProperty = value; }
        }

        public Upgrade Upgrade
        {
            get
            {
                return upgrade;
            }
            set
            {
                RaiseSetIfChanged(ref upgrade, value);
                Level = ReactiveProperty.FromObject(Upgrade, u => u.Level);
                Level.PropertyChanged += OnPropertyChanged;

                Effects = new ReactiveList<EffectViewModel>(Upgrade.Effects.Select(e => EffectViewModel.FindViewModel(e, RuleSetViewModel)).Where(e => e != null))
                {
                    ChangeTrackingEnabled = true
                };
                beforeEffectAdded = Effects.BeforeItemsAdded.Subscribe(e => Upgrade.Effects.Add(e?.Effect));
                beforeEffectRemoved = Effects.BeforeItemsRemoved.Subscribe(e => Upgrade.Effects.Remove(e?.Effect));

                Costs = new ReactiveList<ResourcePartViewModel>(Upgrade.Costs.Select(e => new ResourcePartViewModel() { RuleSetViewModel = RuleSetViewModel, ResourcePart = e }))
                {
                    ChangeTrackingEnabled = true
                };
                beforeCostAdded = Costs.BeforeItemsAdded.Subscribe(e => Upgrade.Costs.Add(e?.ResourcePart));
                beforeCostRemoved = Costs.BeforeItemsRemoved.Subscribe(e => Upgrade.Costs.Remove(e?.ResourcePart));
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Dispose(ref beforeEffectAdded);
                Dispose(ref beforeEffectRemoved);
                Dispose(ref beforeCostAdded);
                Dispose(ref beforeCostRemoved);
                Dispose(ref levelProperty);

                foreach (var item in Effects)
                    item.Dispose();
                Effects.Clear();
                effects = null;
            }
            base.Dispose(disposing);
        }
    }
}
