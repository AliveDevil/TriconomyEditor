using Reactive.Bindings;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels.BuildingViewModels
{
    public class UpgradeInfoViewModel : RuleSetViewModelBase
    {
        private ReadOnlyReactiveProperty<int> levelProperty;
        private UpgradeViewModel upgrade;

        public ReadOnlyReactiveProperty<int> Level
        {
            get { return levelProperty; }
            private set { RaiseSetIfChanged(ref levelProperty, value); }
        }

        public UpgradeViewModel Upgrade
        {
            get
            {
                return upgrade;
            }
            set
            {
                RaiseSetIfChanged(ref upgrade, value);
                Level = Upgrade.Level.ToReadOnlyReactiveProperty();
            }
        }
    }
}
