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
        private ReactiveList<EffectViewModel> effectList;
        private ReactiveProperty<int> levelProperty;
        private Upgrade upgrade;

        public ReactiveList<EffectViewModel> EffectList
        {
            get { return effectList; }
            private set { RaiseSetIfChanged(ref effectList, value); }
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

                EffectList = new ReactiveList<EffectViewModel>(Upgrade.Effects.Select(e =>
                {
                    EffectViewModel model = null;

                    if (e is AddRecipeEffect) model = new AddRecipeEffectViewModel();
                    else if (e is ExtendSettlerAmountEffect) model = new ExtendSettlerAmountEffectViewModel();
                    else if (e is ExtendStorageEffect) model = new ExtendStorageEffectViewModel();
                    else if (e is GatherResourceEffect) model = new GatherResourceEffectViewModel();
                    else if (e is HabitEffect) model = new HabitEffectViewModel();
                    else if (e is StorageEffect) model = new StorageEffectViewModel();
                    else if (e is UseResourceEffect) model = new UseResourceEffectViewModel();
                    else if (e is WorkplaceEffect) model = new WorkplaceEffectViewModel();

                    if (model != null)
                    {
                        model.RuleSetViewModel = RuleSetViewModel;
                        model.Effect = e;
                    }

                    return model;
                }).Where(e => e != null))
                {
                    ChangeTrackingEnabled = true
                };
                beforeEffectAdded = EffectList.BeforeItemsAdded.Subscribe(e => Upgrade.Effects.Add(e.Effect));
                beforeEffectRemoved = EffectList.BeforeItemsRemoved.Subscribe(e => Upgrade.Effects.Remove(e.Effect));
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Dispose(ref beforeEffectAdded);
                Dispose(ref beforeEffectRemoved);
                Dispose(ref levelProperty);

                foreach (var item in EffectList)
                    item.Dispose();
                EffectList.Clear();
                effectList = null;
            }
            base.Dispose(disposing);
        }
    }
}
