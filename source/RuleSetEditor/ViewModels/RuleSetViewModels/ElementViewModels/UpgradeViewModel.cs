using Reactive.Bindings;
using RuleSet;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels
{
    public class UpgradeViewModel : RuleSetViewModelBase
    {
        private ReactiveProperty<int> levelProperty;
        private Upgrade upgrade;

        public ReactiveProperty<int> Level
        {
            get { return levelProperty; }
            private set { levelProperty = value; }
        }

        public Upgrade Upgrade
        {
            get
            {
                return upgrade;
            }
            set
            {
                RaiseSetIfChanged(ref upgrade, value);
                Level = ReactiveProperty.FromObject(Upgrade, u => u.Level);
                Level.PropertyChanged += OnPropertyChanged;
            }
        }
    }
}
