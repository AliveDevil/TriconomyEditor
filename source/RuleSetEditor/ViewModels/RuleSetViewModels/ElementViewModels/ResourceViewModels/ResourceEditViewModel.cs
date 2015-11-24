using Reactive.Bindings;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels.ResourceViewModels
{
    public class ResourceEditViewModel : RuleSetViewModelBase
    {
        private ReactiveProperty<string> nameProperty;
        private ResourceViewModel resource;

        public ReactiveProperty<string> Name
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
                Name = Resource.Name;
            }
        }
    }
}
