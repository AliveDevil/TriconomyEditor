using RuleSet.Menus;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ToolbarViewModels
{
    public class ToolbarEditViewModel : RuleSetViewModelBase
    {
        private RelayCommand addOpenToolbarItemCommand;
        private RelayCommand addPlaceBuildingItemCommand;
        private RelayCommand<ToolbarItemViewModel> editToolbarItemCommand;
        private RelayCommand removeToolbarItemCommand;
        private ToolbarItemViewModel selectedToolbarItem;
        private ToolbarViewModel toolbar;

        public RelayCommand AddOpenToolbarItemCommand
        {
            get
            {
                return addOpenToolbarItemCommand ?? (addOpenToolbarItemCommand = new RelayCommand(() =>
                {
                    AddAndSelectNewToolbarItem<OpenToolbarItemViewModel, OpenToolbarItem>();
                }));
            }
        }

        public RelayCommand AddPlaceBuildingItemCommand
        {
            get
            {
                return addPlaceBuildingItemCommand ?? (addPlaceBuildingItemCommand = new RelayCommand(() =>
                {
                    AddAndSelectNewToolbarItem<PlaceBuildingItemViewModel, PlaceBuildingItem>();
                }));
            }
        }

        private void AddAndSelectNewToolbarItem<TViewModel, TItem>()
            where TViewModel : ToolbarItemViewModel, new()
            where TItem : ToolbarItem, new()
        {
            TViewModel model = new TViewModel() { RuleSetViewModel = RuleSetViewModel, MenuItem = new TItem() };
            Toolbar.ToolbarItems.Add(model);
            SelectedToolbarItem = model;
        }

        public RelayCommand<ToolbarItemViewModel> EditToolbarItemCommand
        {
            get
            {
                return editToolbarItemCommand ?? (editToolbarItemCommand = new RelayCommand<ToolbarItemViewModel>(t =>
                {
                    SelectedToolbarItem = t;
                    ViewStack.Push(t);
                }));
            }
        }

        public RelayCommand RemoveToolbarItemCommand
        {
            get
            {
                return removeToolbarItemCommand ?? (removeToolbarItemCommand = new RelayCommand(() =>
                {
                    Toolbar.ToolbarItems.Remove(SelectedToolbarItem);
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
