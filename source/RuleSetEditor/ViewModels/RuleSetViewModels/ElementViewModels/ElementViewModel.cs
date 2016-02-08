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
        
        protected virtual void OnElementChanged()
        {
        }

        protected override void OnInitialize()
        {
            Name = ReactiveProperty.FromObject(Element, e => e.Name);
        }
    }

    public class ElementViewModel<TElement> : ElementViewModel
        where TElement : Element
    {
        public new TElement Element
        {
            get
            {
                return (TElement)base.Element;
            }
            set
            {
                base.Element = value;
            }
        }
    }
}
