using System;
using Reactive.Bindings;
using ReactiveUI;
using RuleSet;
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

        public RelayCommand<EffectViewModel> EditEffectCommand => new RelayCommand<EffectViewModel>(e =>
        {
            ViewStack.Push(EffectViewModel.FindEditViewModel(e, RuleSetViewModel));
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

        private void AddEffect(Effect effect)
        {
            EffectViewModel model = EffectViewModel.FindViewModel(effect, RuleSetViewModel);

            if (model != null)
                Upgrade.Effects.Add(model);
        }
    }
}
