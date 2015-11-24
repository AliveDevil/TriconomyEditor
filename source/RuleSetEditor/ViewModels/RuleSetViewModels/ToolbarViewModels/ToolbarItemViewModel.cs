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
                if (!DeferChanged)
                    OnMenuItemChanged();
                else
                    DeferQueue.Enqueue(OnMenuItemChanged);
            }
        }

        protected virtual void OnMenuItemChanged()
        {
        }
    }
}
