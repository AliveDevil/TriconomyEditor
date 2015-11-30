using System;
using System.Linq;
using Reactive.Bindings;
using ReactiveUI;
using RuleSet;
using RuleSet.Effects;
using RuleSetEditor.ViewModels.EffectViewModels;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels
{
    public class UpgradeViewModel : RuleSetViewModelBase
    {
        private IDisposable beforeEffectAdded;
        private IDisposable beforeEffectRemoved;
        private ReactiveList<EffectViewModel> effects;
        private ReactiveProperty<int> levelProperty;
        private Upgrade upgrade;

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
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Dispose(ref beforeEffectAdded);
                Dispose(ref beforeEffectRemoved);
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
