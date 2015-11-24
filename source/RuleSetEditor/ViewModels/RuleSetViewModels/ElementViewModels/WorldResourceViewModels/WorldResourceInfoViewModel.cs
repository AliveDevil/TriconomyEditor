using Reactive.Bindings;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels.WorldResourceViewModels
{
    public class WorldResourceInfoViewModel : RuleSetViewModelBase
    {
        private ReadOnlyReactiveProperty<string> nameProperty;
        private ReadOnlyReactiveProperty<ResourceViewModel> resourceProperty;
        private ReadOnlyReactiveProperty<int> variantsProperty;
        private WorldResourceViewModel worldResource;

        public ReadOnlyReactiveProperty<string> Name
        {
            get { return nameProperty; }
            private set { RaiseSetIfChanged(ref nameProperty, value); }
        }

        public ReadOnlyReactiveProperty<ResourceViewModel> Resource
        {
            get { return resourceProperty; }
            private set { RaiseSetIfChanged(ref resourceProperty, value); }
        }

        public ReadOnlyReactiveProperty<int> Variants
        {
            get { return variantsProperty; }
            private set { RaiseSetIfChanged(ref variantsProperty, value); }
        }

        public WorldResourceViewModel WorldResource
        {
            get
            {
                return worldResource;
            }
            set
            {
                RaiseSetIfChanged(ref worldResource, value);
                Name = WorldResource.Name.ToReadOnlyReactiveProperty();
                Name.PropertyChanged += OnPropertyChanged;
                Resource = WorldResource.Resource.ToReadOnlyReactiveProperty();
                Resource.PropertyChanged += OnPropertyChanged;
                Variants = WorldResource.Variants.ToReadOnlyReactiveProperty();
                Variants.PropertyChanged += OnPropertyChanged;
            }
        }

        public override string ToString()
        {
            return $"{Name.Value} {{{Resource?.Value?.Resource?.Name}}}";
        }
    }
}
