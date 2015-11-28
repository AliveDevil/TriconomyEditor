using Reactive.Bindings;
using RuleSet.Menus;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ToolbarViewModels
{
    public class OpenToolbarItemViewModel : ToolbarItemViewModel
    {
        private ReactiveProperty<ToolbarViewModel> toolbarProperty;

        public OpenToolbarItem OpenToolbar => (OpenToolbarItem)MenuItem;

        public ReactiveProperty<ToolbarViewModel> Toolbar
        {
            get { return toolbarProperty; }
            private set { toolbarProperty = value; }
        }
        
        protected override void OnMenuItemChanged()
        {
            base.OnMenuItemChanged();
            Toolbar = ReactiveProperty.FromObject(OpenToolbar,
                t => t.Toolbar,
                t => new ToolbarViewModel() { Toolbar = t, RuleSetViewModel = RuleSetViewModel },
                t => t?.Toolbar);
        }
        
        protected override void OnViewStackChanged()
        {
            Toolbar.Value.ViewStack = ViewStack;
        }
    }
}
