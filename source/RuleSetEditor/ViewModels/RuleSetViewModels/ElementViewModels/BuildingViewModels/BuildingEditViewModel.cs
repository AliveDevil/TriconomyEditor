using Reactive.Bindings;
using ReactiveUI;
using RuleSet;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels.BuildingViewModels
{
    public class BuildingEditViewModel : RuleSetViewModelBase
    {
        private BuildingViewModel building;
        private RelayCommand<UpgradeViewModel> editUpgradeCommand;
        private ReactiveProperty<string> nameProperty;
        private IReactiveDerivedList<UpgradeViewModel> upgradeList;
        private ReactiveProperty<int> variantsProperty;

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
            }
        }

        public RelayCommand<UpgradeViewModel> EditUpgradeCommand => new RelayCommand<UpgradeViewModel>(u =>
         {
             ViewStack.Push<UpgradeEditViewModel>()._(_ => _.Upgrade = u);
         });

        public ReactiveProperty<string> Name
        {
            get { return nameProperty; }
            private set { RaiseSetIfChanged(ref nameProperty, value); }
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
    }
}
