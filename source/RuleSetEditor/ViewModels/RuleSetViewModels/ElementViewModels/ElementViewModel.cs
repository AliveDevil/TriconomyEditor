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
                if (!RaiseSetIfChanged(ref element, value)) return;
                Name = ReactiveProperty.FromObject(Element, e => e.Name);
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

        public override string ToString()
        {
            return Element.Name;
        }
    }
}
