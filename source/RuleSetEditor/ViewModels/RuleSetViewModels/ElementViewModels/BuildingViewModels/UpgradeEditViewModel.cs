using System;
using Reactive.Bindings;
using ReactiveUI;
using RuleSet;
using RuleSetEditor.ViewModels.EffectViewModels;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels.BuildingViewModels
{
    public class UpgradeEditViewModel : RuleSetViewModelBase
    {
        private RelayCommand<Type> addEffectCommand;
        private RelayCommand<EffectViewModel> editEffectCommand;
        private ReactiveList<EffectViewModel> effectList;
        private ReactiveProperty<int> levelProperty;
        private RelayCommand removeEffectCommand;
        private EffectViewModel selectedEffect;
        private UpgradeViewModel upgrade;

        public RelayCommand<Type> AddEffectCommand
        {
            get
            {
                return addEffectCommand ?? (addEffectCommand = new RelayCommand<Type>(t =>
                {
                    AddEffect((Effect)Activator.CreateInstance(t));
                }));
            }
        }

        public RelayCommand<EffectViewModel> EditEffectCommand
        {
            get
            {
                return editEffectCommand ?? (editEffectCommand = new RelayCommand<EffectViewModel>(e =>
                {
                    ViewStack.Push(EffectViewModel.FindEditViewModel(e, RuleSetViewModel));
                }));
            }
        }

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

        public RelayCommand RemoveEffectCommand
        {
            get
            {
                return removeEffectCommand ?? (removeEffectCommand = new RelayCommand(() =>
                {
                    Upgrade.Effects.Remove(SelectedEffect);
                }));
            }
        }

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
            {
                Upgrade.Effects.Add(model);
                ViewStack.Push(EffectViewModel.FindEditViewModel(model, RuleSetViewModel));
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Dispose(ref addEffectCommand);
                Dispose(ref editEffectCommand);
            }
            base.Dispose(disposing);
        }
    }
}
