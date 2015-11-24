using Reactive.Bindings;
using RuleSet;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels
{
    public class ElementViewModel : RuleSetViewModelBase
    {
        private Element element;
        private ReactiveProperty<string> nameProperty;

        public Element Element
        {
            get
            {
                return element;
            }
            set
            {
                RaiseSetIfChanged(ref element, value);
                Name = ReactiveProperty.FromObject(Element, e => e.Name);
                Name.PropertyChanged += OnPropertyChanged;
                if (!DeferChanged)
                    OnElementChanged();
                else
                    DeferQueue.Enqueue(OnElementChanged);
            }
        }

        public ReactiveProperty<string> Name
        {
            get { return nameProperty; }
            private set { RaiseSetIfChanged(ref nameProperty, value); }
        }

        protected virtual void OnElementChanged()
        {
        }
    }
}
