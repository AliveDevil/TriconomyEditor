using Reactive.Bindings;
using RuleSet.Elements;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels
{
    public class ResourceViewModel : ElementViewModel<Resource>
    {
        private ReactiveProperty<int> stackSizeProperty;

        public ReactiveProperty<int> StackSize
        {
            get { return stackSizeProperty; }
            set { stackSizeProperty = value; }
        }

        protected override void OnElementChanged()
        {
            StackSize = ReactiveProperty.FromObject(Element, e => e.StackSize);
        }
    }
}
