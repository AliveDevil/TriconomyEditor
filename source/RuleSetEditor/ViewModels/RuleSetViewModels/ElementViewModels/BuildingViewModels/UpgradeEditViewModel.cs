using System;
using Reactive.Bindings;
using ReactiveUI;
using RuleSet;
using RuleSet.Effects;
using RuleSetEditor.ViewModels.EffectViewModels;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels.BuildingViewModels
{
    public class UpgradeEditViewModel : RuleSetViewModelBase
    {
        private ReactiveList<EffectViewModel> effectList;
        private ReactiveProperty<int> levelProperty;
        private EffectViewModel selectedEffect;
        private UpgradeViewModel upgrade;

        public RelayCommand<Type> AddEffectCommand => new RelayCommand<Type>(t =>
        {
            AddEffect((Effect)Activator.CreateInstance(t));
        });

        public ReactiveList<EffectViewModel> EffectList
        {
            get { return effectList; }
            private set { RaiseSetIfChanged(ref effectList, value); }
        }

        public ReactiveProperty<int> Level
        {
            get { return levelProperty; }
            private set { RaiseSetIfChanged(ref levelProperty, value); }
        }

        public RelayCommand RemoveEffectCommand => new RelayCommand(() =>
        {
            Upgrade.Effects.Remove(SelectedEffect);
        });

        public EffectViewModel SelectedEffect
        {
            get { return selectedEffect; }
            set { RaiseSetIfChanged(ref selectedEffect, value); }
        }

        public UpgradeViewModel Upgrade
        {
            get
            {
                return upgrade;
            }
            set
            {
                if (!RaiseSetIfChanged(ref upgrade, value)) return;
                Level = Upgrade.Level;
                EffectList = Upgrade.Effects;
            }
        }

        private void AddEffect(Effect e)
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

            if (model != null)
                Upgrade.Effects.Add(model);
        }
    }
}
