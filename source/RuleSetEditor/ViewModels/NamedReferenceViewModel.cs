using Reactive.Bindings;
using RuleSet;

namespace RuleSetEditor.ViewModels
{
    public class NamedReferenceViewModel : RuleSetViewModelBase
    {
        private NamedReference namedReference;

        private ReactiveProperty<string> nameProperty;

        public ReactiveProperty<string> Name
        {
            get
            {
                return nameProperty;
            }
            private set
            {
                RaiseSetIfChanged(ref nameProperty, value);
            }
        }

        public NamedReference NamedReference
        {
            get
            {
                return namedReference;
            }
            set
            {
                RaiseSetIfChanged(ref namedReference, value);
            }
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            Name = ReactiveProperty.FromObject(NamedReference, r => r.Name);
        }
    }
}
