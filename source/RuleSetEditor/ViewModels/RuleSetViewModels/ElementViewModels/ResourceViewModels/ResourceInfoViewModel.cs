using Reactive.Bindings;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels.ResourceViewModels
{
    public class ResourceInfoViewModel : RuleSetViewModelBase
    {
        private ReadOnlyReactiveProperty<string> nameProperty;
        private ResourceViewModel resource;

        public ReadOnlyReactiveProperty<string> Name
        {
            get { return nameProperty; }
            private set { RaiseSetIfChanged(ref nameProperty, value); }
        }

        public ResourceViewModel Resource
        {
            get
            {
                return resource;
            }
            set
            {
                RaiseSetIfChanged(ref resource, value);
                Name = Resource.Name.ToReadOnlyReactiveProperty();
            }
        }

        public override string ToString()
        {
            return Name.Value;
        }
    }
}
