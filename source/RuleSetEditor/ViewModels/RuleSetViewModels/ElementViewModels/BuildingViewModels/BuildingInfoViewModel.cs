using Reactive.Bindings;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels.BuildingViewModels
{
    public class BuildingInfoViewModel : RuleSetViewModelBase
    {
        private BuildingViewModel building;
        private ReadOnlyReactiveProperty<string> nameProperty;

        public BuildingViewModel Building
        {
            get
            {
                return building;
            }
            set
            {
                RaiseSetIfChanged(ref building, value);
                Name = Building.Name.ToReadOnlyReactiveProperty();
            }
        }

        public ReadOnlyReactiveProperty<string> Name
        {
            get { return nameProperty; }
            private set { RaiseSetIfChanged(ref nameProperty, value); }
        }

        public override string ToString()
        {
            return Name.Value;
        }
    }
}
