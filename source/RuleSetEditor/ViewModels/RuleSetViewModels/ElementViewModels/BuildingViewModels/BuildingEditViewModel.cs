using System;
using Reactive.Bindings;
using ReactiveUI;
using RuleSet;
using RuleSetEditor.ViewModels.EffectViewModels;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels.BuildingViewModels
{
    public class BuildingEditViewModel : RuleSetViewModelBase
    {
        private RelayCommand addUpgradeCommand;
        private BuildingViewModel building;
        private RelayCommand<UpgradeViewModel> editUpgradeCommand;
        private ReactiveProperty<string> nameProperty;
        private RelayCommand removeUpgradeCommand;
        private UpgradeViewModel selectedUpgrade;
        private IReactiveDerivedList<UpgradeViewModel> upgradeList;
        private ReactiveProperty<int> variantsProperty;

        public RelayCommand AddUpgradeCommand
        {
            get
            {
                return addUpgradeCommand ?? (addUpgradeCommand = new RelayCommand(() =>
                {
                    Upgrade upgrade = new Upgrade() { Level = 0 };
                    UpgradeViewModel viewModel = new UpgradeViewModel()
                    {
                        RuleSetViewModel = RuleSetViewModel,
                        Upgrade = upgrade
                    };
                    Building.UpgradeList.Add(viewModel);
                    ViewStack.Push<UpgradeEditViewModel>()._(_ => _.Upgrade = viewModel);
                }));
            }
        }

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
            }
        }

        public RelayCommand<UpgradeViewModel> EditUpgradeCommand
        {
            get
            {
                return editUpgradeCommand ?? (editUpgradeCommand = new RelayCommand<UpgradeViewModel>(u =>
                {
                    ViewStack.Push<UpgradeEditViewModel>()._(_ => _.Upgrade = u);
                }));
            }
        }

        public ReactiveProperty<string> Name
        {
            get { return nameProperty; }
            private set { RaiseSetIfChanged(ref nameProperty, value); }
        }

        public RelayCommand RemoveUpgradeCommand
        {
            get
            {
                return removeUpgradeCommand ?? (removeUpgradeCommand = new RelayCommand(() =>
                {
                    Building.UpgradeList.Remove(SelectedUpgrade);
                }));
            }
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Dispose(ref upgradeList);
                Dispose(ref nameProperty);
                Dispose(ref variantsProperty);
                Dispose(ref addUpgradeCommand);
                Dispose(ref editUpgradeCommand);
                Dispose(ref removeUpgradeCommand);
            }
            base.Dispose(disposing);
        }
    }
}
