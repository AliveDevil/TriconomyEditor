using RuleSet.Menus;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ToolbarViewModels
{
    public class ToolbarItemViewModel : RuleSetViewModelBase
    {
        private ToolbarItem menuItem;

        public ToolbarItem MenuItem
        {
            get
            {
                return menuItem;
            }
            set
            {
                RaiseSetIfChanged(ref menuItem, value);
                OnMenuItemChanged();
            }
        }

        protected virtual void OnMenuItemChanged()
        {
        }
    }

    public class ToolbarItemViewModel<T> : ToolbarItemViewModel
        where T : ToolbarItem
    {
        public new T MenuItem
        {
            get
            {
                return (T)base.MenuItem;
            }
            set
            {
                base.MenuItem = value;
            }
        }
    }
}
