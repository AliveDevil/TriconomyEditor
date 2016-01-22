using System;
using Reactive.Bindings;
using ReactiveUI;
using RuleSet;
using RuleSet.Elements;
using RuleSetEditor.ViewModels.EffectViewModels;
using RuleSetEditor.ViewModels.RuleSetViewModels.ResourcePartViewModels;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels.BuildingViewModels
{
    public class UpgradeEditViewModel : RuleSetViewModelBase
    {
        private RelayCommand addCostCommand;
        private RelayCommand<Type> addEffectCommand;
        private ReactiveList<ResourcePartViewModel> costs;
        private RelayCommand<ResourcePartViewModel> editCostCommand;
        private RelayCommand<EffectViewModel> editEffectCommand;
        private ReactiveList<EffectViewModel> effectList;
        private ReactiveProperty<int> levelProperty;
        private RelayCommand removeCostCommand;
        private RelayCommand removeEffectCommand;
        private ResourcePartViewModel selectedCost;
        private EffectViewModel selectedEffect;
        private UpgradeViewModel upgrade;

        public RelayCommand AddCostCommand
        {
            get
            {
                return addCostCommand ?? (addCostCommand = new RelayCommand(() =>
                {
                    ResourcePartViewModel model = new ResourcePartViewModel()
                    {
                        RuleSetViewModel = RuleSetViewModel,
                        ResourcePart = new ResourcePart()
                    };
                    Upgrade.Costs.Add(model);
                    SelectedCost = model;
                }));
            }
        }

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

        public ReactiveList<ResourcePartViewModel> Costs
        {
            get { return costs; }
            private set { RaiseSetIfChanged(ref costs, value); }
        }

        public RelayCommand<ResourcePartViewModel> EditCostCommand
        {
            get
            {
                return editCostCommand ?? (editCostCommand = new RelayCommand<ResourcePartViewModel>(resourcePart =>
                {
                    SelectedCost = resourcePart;
                    ViewStack.Push<ResourcePartEditViewModel>()._(_ => { _.ResourcePart = resourcePart; });
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

        public RelayCommand RemoveCostCommand
        {
            get
            {
                return removeCostCommand ?? (removeCostCommand = new RelayCommand(() =>
                {
                    Upgrade.Costs.Remove(SelectedCost);
                }));
            }
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

        public ResourcePartViewModel SelectedCost
        {
            get { return selectedCost; }
            set { RaiseSetIfChanged(ref selectedCost, value); }
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
                Costs = Upgrade.Costs;
                Level = Upgrade.Level;
                EffectList = Upgrade.Effects;
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

        private void AddEffect(Effect effect)
        {
            EffectViewModel model = EffectViewModel.FindViewModel(effect, RuleSetViewModel);

            if (model != null)
            {
                Upgrade.Effects.Add(model);
                ViewStack.Push(EffectViewModel.FindEditViewModel(model, RuleSetViewModel));
            }
        }
    }
}
