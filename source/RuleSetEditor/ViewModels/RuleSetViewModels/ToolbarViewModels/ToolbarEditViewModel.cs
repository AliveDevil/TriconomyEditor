using RuleSet.Menus;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ToolbarViewModels
{
    public class ToolbarEditViewModel : RuleSetViewModelBase
    {
        private RelayCommand addOpenToolbarItemCommand;
        private RelayCommand addPlaceBuildingItemCommand;
        private RelayCommand<ToolbarItemViewModel> editToolbarItemCommand;
        private ToolbarItemViewModel selectedToolbarItem;
        private ToolbarViewModel toolbar;

        public RelayCommand AddOpenToolbarItemCommand
        {
            get
            {
                return addOpenToolbarItemCommand ?? (addOpenToolbarItemCommand = new RelayCommand(() =>
                {
                    Toolbar.ToolbarItems.Add(new OpenToolbarItemViewModel() { RuleSetViewModel = RuleSetViewModel, MenuItem = new OpenToolbarItem() { Toolbar = new Toolbar() { Name = "New Toolbar" } } });
                }));
            }
        }

        public RelayCommand AddPlaceBuildingItemCommand
        {
            get
            {
                return addPlaceBuildingItemCommand ?? (addPlaceBuildingItemCommand = new RelayCommand(() =>
                {
                    Toolbar.ToolbarItems.Add(new PlaceBuildingItemViewModel() { RuleSetViewModel = RuleSetViewModel, MenuItem = new PlaceBuildingItem() });
                }));
            }
        }

        public RelayCommand<ToolbarItemViewModel> EditToolbarItemCommand
        {
            get
            {
                return editToolbarItemCommand ?? (editToolbarItemCommand = new RelayCommand<ToolbarItemViewModel>(t =>
                {
                    ViewStack.Push(t);
                }));
            }
        }

        public ToolbarItemViewModel SelectedToolbarItem
        {
            get { return selectedToolbarItem; }
            set { RaiseSetIfChanged(ref selectedToolbarItem, value); }
        }

        public ToolbarViewModel Toolbar
        {
            get { return toolbar; }
            set { RaiseSetIfChanged(ref toolbar, value); }
        }
    }
}
