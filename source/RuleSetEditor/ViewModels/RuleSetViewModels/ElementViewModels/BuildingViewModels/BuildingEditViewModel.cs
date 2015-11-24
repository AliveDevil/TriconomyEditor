using Reactive.Bindings;
using ReactiveUI;
using RuleSet;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels.BuildingViewModels
{
    public class BuildingEditViewModel : RuleSetViewModelBase
    {
        private RelayCommand addUpgradeCommand;
        private BuildingViewModel building;
        private RelayCommand<UpgradeInfoViewModel> editUpgradeCommand;
        private ReactiveProperty<string> nameProperty;
        private IReactiveDerivedList<UpgradeInfoViewModel> upgradeList;
        private ReactiveProperty<int> variantsProperty;

        public RelayCommand AddUpgradeCommand
        {
            get
            {
                return addUpgradeCommand ?? (addUpgradeCommand = new RelayCommand(() =>
                {
                    Building.UpgradeList.Add(new UpgradeViewModel() { RuleSetViewModel = RuleSetViewModel, Upgrade = new Upgrade() { Level = 0 } });
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
                UpgradeList = Building.UpgradeList.CreateDerivedCollection(u => new UpgradeInfoViewModel() { RuleSetViewModel = RuleSetViewModel, Upgrade = u }, null, (l, r) => l.Upgrade.Level.Value.CompareTo(r.Upgrade.Level.Value));
                UpgradeList.ChangeTrackingEnabled = true;
            }
        }

        public RelayCommand<UpgradeInfoViewModel> EditUpgradeCommand
        {
            get
            {
                return editUpgradeCommand ?? (editUpgradeCommand = new RelayCommand<UpgradeInfoViewModel>(u =>
                {
                    ViewStack.Push<UpgradeEditViewModel>()._(_ => _.Upgrade = u.Upgrade);
                }));
            }
        }

        public ReactiveProperty<string> Name
        {
            get { return nameProperty; }
            private set { RaiseSetIfChanged(ref nameProperty, value); }
        }

        public IReactiveDerivedList<UpgradeInfoViewModel> UpgradeList
        {
            get { return upgradeList; }
            private set { RaiseSetIfChanged(ref upgradeList, value); }
        }

        public ReactiveProperty<int> Variants
        {
            get { return variantsProperty; }
            private set { RaiseSetIfChanged(ref variantsProperty, value); }
        }
    }
}
