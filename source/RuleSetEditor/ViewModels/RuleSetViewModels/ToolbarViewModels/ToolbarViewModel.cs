using System;
using System.Linq;
using Reactive.Bindings;
using ReactiveUI;
using RuleSet.Menus;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ToolbarViewModels
{
    public class ToolbarViewModel : RuleSetViewModelBase
    {
        private IDisposable itemAddedSubscription;
        private IDisposable itemsRemovedSubscription;
        private ReactiveProperty<string> nameProperty;
        private ToolbarItemViewModel selectedToolbarItem;
        private Toolbar toolbar;
        private ReactiveList<ToolbarItemViewModel> toolbarItems;

        public RelayCommand AddOpenToolbarItemCommand => new RelayCommand(() =>
        {
            AddAndSelectNewToolbarItem<OpenToolbarItemViewModel, OpenToolbarItem>(m =>
            {
                m.Toolbar = new Toolbar() { Name = "New Toolbar" };
            }, null);
        });

        public RelayCommand AddPlaceBuildingItemCommand => new RelayCommand(() =>
        {
            AddAndSelectNewToolbarItem<PlaceBuildingItemViewModel, PlaceBuildingItem>(null, null);
        });

        public RelayCommand<ToolbarItemViewModel> EditToolbarItemCommand => new RelayCommand<ToolbarItemViewModel>(t =>
        {
            ViewStack.Push(t);
            SelectedToolbarItem = t;
        });

        public ReactiveProperty<string> Name
        {
            get { return nameProperty; }
            private set { RaiseSetIfChanged(ref nameProperty, value); }
        }

        public RelayCommand RemoveToolbarItemCommand => new RelayCommand(() =>
        {
            SelectedToolbarItem?.Dispose();
            ToolbarItems.Remove(SelectedToolbarItem);
            SelectedToolbarItem = null;
        });

        public ToolbarItemViewModel SelectedToolbarItem
        {
            get { return selectedToolbarItem; }
            set { RaiseSetIfChanged(ref selectedToolbarItem, value); }
        }

        public Toolbar Toolbar
        {
            get { return toolbar ?? (Toolbar = RuleSetViewModel.RuleSet.Toolbar ?? (RuleSetViewModel.RuleSet.Toolbar = new Toolbar() { Name = "Default" })); }
            set { RaiseSetIfChanged(ref toolbar, value); }
        }

        public ReactiveList<ToolbarItemViewModel> ToolbarItems
        {
            get { return toolbarItems; }
            private set { RaiseSetIfChanged(ref toolbarItems, value); }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Dispose(ref itemAddedSubscription);
                Dispose(ref itemAddedSubscription);
                ToolbarItems.Clear();

                Dispose(ref nameProperty);
                AddOpenToolbarItemCommand.Dispose();
                AddPlaceBuildingItemCommand.Dispose();
                EditToolbarItemCommand.Dispose();
                RemoveToolbarItemCommand.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void OnRuleSetChanged()
        {
            base.OnRuleSetChanged();
            Name = ReactiveProperty.FromObject(Toolbar, t => t.Name);
            ToolbarItems = new ReactiveList<ToolbarItemViewModel>(Toolbar.Items.Select(i =>
            {
                ToolbarItemViewModel menuItem = null;
                if (i is PlaceBuildingItem)
                    menuItem = new PlaceBuildingItemViewModel();
                else if (i is OpenToolbarItem)
                    menuItem = new OpenToolbarItemViewModel();

                if (menuItem != null)
                {
                    menuItem.DeferChanged = true;
                    menuItem.RuleSetViewModel = RuleSetViewModel;
                    menuItem.MenuItem = i;
                }

                return menuItem;
            }).Where(i => i != null));

            foreach (var item in ToolbarItems)
                item.DeferChanged = false;

            itemAddedSubscription = ToolbarItems.BeforeItemsAdded.Subscribe(i => Toolbar.Items.Add(i.MenuItem));
            itemsRemovedSubscription = ToolbarItems.BeforeItemsRemoved.Subscribe(i => Toolbar.Items.Remove(i.MenuItem));
        }

        private TViewModel AddAndSelectNewToolbarItem<TViewModel, TItem>(Action<TItem> itemAction, Action<TViewModel> viewModelAction)
            where TViewModel : ToolbarItemViewModel, new()
            where TItem : ToolbarItem, new()
        {
            TItem item = new TItem();
            itemAction?.Invoke(item);
            TViewModel model = new TViewModel() { RuleSetViewModel = RuleSetViewModel, MenuItem = item };
            viewModelAction?.Invoke(model);
            ToolbarItems.Add(model);
            SelectedToolbarItem = model;
            return model;
        }
    }
}
