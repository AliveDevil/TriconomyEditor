using Reactive.Bindings;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels.BuildingViewModels
{
    public class UpgradeEditViewModel : RuleSetViewModelBase
    {
        private ReactiveProperty<int> levelProperty;
        private UpgradeViewModel upgrade;

        public ReactiveProperty<int> Level
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
                Level = Upgrade.Level;
            }
        }
    }
}
