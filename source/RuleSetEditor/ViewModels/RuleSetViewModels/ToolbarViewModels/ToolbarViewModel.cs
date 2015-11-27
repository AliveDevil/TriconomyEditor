using System;
using System.Collections.Generic;
using System.Linq;
using libUIStack;
using Reactive.Bindings;
using ReactiveUI;
using RuleSet.Menus;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ToolbarViewModels
{
    public class ToolbarViewModel : RuleSetViewModelBase
    {
        private RelayCommand addOpenToolbarItemCommand;
        private RelayCommand addPlaceBuildingItemCommand;
        private IView currentView;
        private RelayCommand<ToolbarItemViewModel> editToolbarItemCommand;
        private IDisposable itemAddedSubscription;
        private IDisposable itemsRemovedSubscription;
        private Stack<IView> loadedViews = new Stack<IView>();
        private ReactiveProperty<string> nameProperty;
        private RelayCommand removeToolbarItemCommand;
        private ToolbarItemViewModel selectedToolbarItem;
        private Toolbar toolbar;
        private ReactiveList<ToolbarItemViewModel> toolbarItems;

        public RelayCommand AddOpenToolbarItemCommand
        {
            get
            {
                return addOpenToolbarItemCommand ?? (addOpenToolbarItemCommand = new RelayCommand(() =>
                {
                    AddAndSelectNewToolbarItem<OpenToolbarItemViewModel, OpenToolbarItem>(m =>
                    {
                        m.Toolbar = new Toolbar() { Name = "New Toolbar" };
                    }, null);
                }));
            }
        }

        public RelayCommand AddPlaceBuildingItemCommand
        {
            get
            {
                return addPlaceBuildingItemCommand ?? (addPlaceBuildingItemCommand = new RelayCommand(() =>
                {
                    AddAndSelectNewToolbarItem<PlaceBuildingItemViewModel, PlaceBuildingItem>(null, null);
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
                    SelectedToolbarItem = t;
                }));
            }
        }

        public ReactiveProperty<string> Name
        {
            get { return nameProperty; }
            private set { RaiseSetIfChanged(ref nameProperty, value); }
        }

        public RelayCommand RemoveToolbarItemCommand
        {
            get
            {
                return removeToolbarItemCommand ?? (removeToolbarItemCommand = new RelayCommand(() =>
                {
                    SelectedToolbarItem?.Dispose();
                    ToolbarItems.Remove(SelectedToolbarItem);
                    SelectedToolbarItem = null;
                }));
            }
        }

        public ToolbarItemViewModel SelectedToolbarItem
        {
            get { return selectedToolbarItem; }
            set { RaiseSetIfChanged(ref selectedToolbarItem, value); }
        }

        public Toolbar Toolbar
        {
            get { return toolbar; }
            set { RaiseSetIfChanged(ref toolbar, value); }
        }

        public ReactiveList<ToolbarItemViewModel> ToolbarItems
        {
            get { return toolbarItems; }
            private set { RaiseSetIfChanged(ref toolbarItems, value); }
        }

        public IView View
        {
            get { return currentView; }
            private set { RaiseSetIfChanged(ref currentView, value); }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Name.Dispose();
                itemAddedSubscription.Dispose();
                itemsRemovedSubscription.Dispose();
                foreach (var item in ToolbarItems)
                {
                    item.Dispose();
                }
                ToolbarItems.Clear();
            }
            base.Dispose(disposing);
        }

        protected override void OnRuleSetChanged()
        {
            base.OnRuleSetChanged();

            if (Toolbar == null)
            {
                Toolbar = RuleSetViewModel.RuleSet.Toolbar;
            }

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
