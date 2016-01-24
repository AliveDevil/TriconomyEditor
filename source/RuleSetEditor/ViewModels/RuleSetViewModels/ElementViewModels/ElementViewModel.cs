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
                if (!RaiseSetIfChanged(ref nameProperty, value))
                    return;
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
                Dispose(ref nameProperty);
            }
            base.Dispose(disposing);
        }

        protected virtual void OnElementChanged()
        {
        }
    }

    public class ElementViewModel<TElement> : ElementViewModel
        where TElement : Element
    {
        public new TElement Element
        {
            get { return (TElement)base.Element; }
            set { base.Element = value; }
        }
    }
}
