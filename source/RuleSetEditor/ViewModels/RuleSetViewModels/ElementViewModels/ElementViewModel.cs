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
                if (!RaiseSetIfChanged(ref element, value))
                    return;
                Name = ReactiveProperty.FromObject(Element, e => e.Name);
                if (!DeferChanged)
                    OnElementChanged();
                else
                    DeferQueue.Enqueue(OnElementChanged);
            }
        }

        public ReactiveProperty<string> Name
        {
            get
            {
                return nameProperty;
            }
            private set
            {
                Dispose(ref nameProperty);
                if (!RaiseSetIfChanged(ref nameProperty, value)) return;
                Name.PropertyChanged += OnPropertyChanged;
            }
        }

        public override string ToString()
        {
            return Element.Name;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Name.Dispose();
                Name = null;
            }
            base.Dispose(disposing);
        }

        protected virtual void OnElementChanged()
        {
        }
    }
}
