using Reactive.Bindings;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels.ResourceViewModels
{
    public class ResourceEditViewModel : RuleSetViewModelBase
    {
        private ResourceViewModel resource;

        public ReactiveProperty<string> Name { get; private set; }

        public ResourceViewModel Resource
        {
            get
            {
                return resource;
            }
            set
            {
                if (!RaiseSetIfChanged(ref resource, value))
                    return;
                Name = Resource.Name;
                StackSize = Resource.StackSize;
            }
        }

        public ReactiveProperty<int> StackSize { get; private set; }
    }
}
