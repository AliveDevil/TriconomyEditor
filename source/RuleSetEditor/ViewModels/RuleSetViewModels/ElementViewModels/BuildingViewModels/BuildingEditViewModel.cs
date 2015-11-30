﻿using System;
using Reactive.Bindings;
using ReactiveUI;
using RuleSet;
using RuleSetEditor.ViewModels.EffectViewModels;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels.BuildingViewModels
{
    public class BuildingEditViewModel : RuleSetViewModelBase
    {
        private BuildingViewModel building;
        private IReactiveDerivedList<EffectViewModel> effectList;
        private ReactiveProperty<string> nameProperty;
        private EffectViewModel selectedEffect;
        private UpgradeViewModel selectedUpgrade;
        private IReactiveDerivedList<UpgradeViewModel> upgradeList;
        private ReactiveProperty<int> variantsProperty;

        public RelayCommand<Type> AddEffectCommand => new RelayCommand<Type>(t =>
        {
            AddEffect((Effect)Activator.CreateInstance(t));
        });

        public RelayCommand AddUpgradeCommand => new RelayCommand(() =>
        {
            Building.UpgradeList.Add(new UpgradeViewModel()
            {
                RuleSetViewModel = RuleSetViewModel,
                Upgrade = new Upgrade() { Level = 0 }
            });
        });

        public BuildingViewModel Building
        {
            get
            {
                return building;
            }
            set
            {
                RaiseSetIfChanged(ref building, value);
                Name = Building.Name;
                Variants = Building.Variants;
                UpgradeList = Building.UpgradeList.CreateDerivedCollection(u => u, null, (l, r) => l.Level.Value.CompareTo(r.Level.Value));
                UpgradeList.ChangeTrackingEnabled = true;
                EffectList = Building.EffectList.CreateDerivedCollection(e => e, null);
                EffectList.ChangeTrackingEnabled = true;
            }
        }

        public RelayCommand<EffectViewModel> EditEffectCommand => new RelayCommand<EffectViewModel>(e =>
        {
            ViewStack.Push(EffectViewModel.FindEditViewModel(e, RuleSetViewModel));
        });

        public RelayCommand<UpgradeViewModel> EditUpgradeCommand => new RelayCommand<UpgradeViewModel>(u =>
                 {
                     ViewStack.Push<UpgradeEditViewModel>()._(_ => _.Upgrade = u);
                 });

        public IReactiveDerivedList<EffectViewModel> EffectList
        {
            get { return effectList; }
            private set { RaiseSetIfChanged(ref effectList, value); }
        }

        public ReactiveProperty<string> Name
        {
            get { return nameProperty; }
            private set { RaiseSetIfChanged(ref nameProperty, value); }
        }

        public RelayCommand RemoveEffectCommand => new RelayCommand(() =>
        {
            Building.EffectList.Remove(SelectedEffect);
        });

        public RelayCommand RemoveUpgradeCommand => new RelayCommand(() =>
        {
            Building.UpgradeList.Remove(SelectedUpgrade);
        });

        public EffectViewModel SelectedEffect
        {
            get { return selectedEffect; }
            set { RaiseSetIfChanged(ref selectedEffect, value); }
        }

        public UpgradeViewModel SelectedUpgrade
        {
            get { return selectedUpgrade; }
            set { RaiseSetIfChanged(ref selectedUpgrade, value); }
        }

        public IReactiveDerivedList<UpgradeViewModel> UpgradeList
        {
            get { return upgradeList; }
            private set { RaiseSetIfChanged(ref upgradeList, value); }
        }

        public ReactiveProperty<int> Variants
        {
            get { return variantsProperty; }
            private set { RaiseSetIfChanged(ref variantsProperty, value); }
        }

        private void AddEffect(Effect effect)
        {
            EffectViewModel model = EffectViewModel.FindViewModel(effect, RuleSetViewModel);

            if (model != null)
                Building.EffectList.Add(model);
        }
    }
}
