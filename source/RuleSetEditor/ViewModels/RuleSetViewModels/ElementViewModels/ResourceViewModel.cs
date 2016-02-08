using Reactive.Bindings;
using RuleSet.Elements;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels
{
    public class ResourceViewModel : ElementViewModel<Resource>
    {
        private ReactiveProperty<int> stackSizeProperty;

        public ReactiveProperty<int> StackSize
        {
            get
            {
                return stackSizeProperty;
            }
            set
            {
                stackSizeProperty = value;
            }
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            StackSize = ReactiveProperty.FromObject(Element, e => e.StackSize);
        }
    }   

    public class ResourceViewModel<T> : ResourceViewModel
        where T : Resource
    {
        public new T Element
        {
            get
            {
                return (T)base.Element;
            }
            set
            {
                base.Element = value;
            }
        }
    }
}
