using System;
using Reactive.Bindings;
using ReactiveUI;
using RuleSet.Menus;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ToolbarViewModels
{
    public class ToolbarViewModel : RuleSetViewModelBase
    {
        private ReactiveProperty<string> nameProperty;
        private ToolbarItemViewModel selectedToolbarItem;
        private Toolbar toolbar;
        private IDisposable toolbarAdded;
        private ReactiveList<ToolbarItemViewModel> toolbarItems;
        private IDisposable toolbarListConstructor;
        private IDisposable toolbarListInitializer;
        private IDisposable toolbarListPostDisposer;
        private IDisposable toolbarListPostInitializer;
        private IDisposable toolbarRemoved;

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
            get
            {
                return nameProperty;
            }
            private set
            {
                RaiseSetIfChanged(ref nameProperty, value);
            }
        }

        public RelayCommand RemoveToolbarItemCommand => new RelayCommand(() =>
        {
            SelectedToolbarItem?.Dispose();
            ToolbarItems.Remove(SelectedToolbarItem);
            SelectedToolbarItem = null;
        });

        public ToolbarItemViewModel SelectedToolbarItem
        {
            get
            {
                return selectedToolbarItem;
            }
            set
            {
                RaiseSetIfChanged(ref selectedToolbarItem, value);
            }
        }

        public Toolbar Toolbar
        {
            get
            {
                return toolbar ?? (Toolbar = RuleSetViewModel.RuleSet.Toolbar ?? (RuleSetViewModel.RuleSet.Toolbar = new Toolbar() { Name = "Default" }));
            }
            set
            {
                RaiseSetIfChanged(ref toolbar, value);
            }
        }

        public ReactiveList<ToolbarItemViewModel> ToolbarItems
        {
            get
            {
                return toolbarItems;
            }
            private set
            {
                RaiseSetIfChanged(ref toolbarItems, value);
            }
        }
        
        protected override void OnInitialize()
        {
            base.OnInitialize();

            Name = ReactiveProperty.FromObject(Toolbar, t => t.Name);
            ToolbarItems = new ReactiveList<ToolbarItemViewModel>();
            ToolbarItems.ChangeTrackingEnabled = true;
            toolbarListInitializer = ToolbarItems.BeforeItemsAdded.Subscribe(e => e.Initialize());
            toolbarListPostInitializer = ToolbarItems.ItemsAdded.Subscribe(e => e.PostInitialize());
            toolbarListPostDisposer = ToolbarItems.ItemsRemoved.Subscribe(e => e.Dispose());
        }

        protected override void OnPostInitialize()
        {
            base.OnPostInitialize();

            foreach (var item in Toolbar.Items)
            {
                ToolbarItemViewModel menuItem = null;
                if (item is PlaceBuildingItem)
                    menuItem = new PlaceBuildingItemViewModel();
                else if (item is OpenToolbarItem)
                    menuItem = new OpenToolbarItemViewModel();
                else
                    continue;

                menuItem.RuleSetViewModel = RuleSetViewModel;
                menuItem.ViewStack = RuleSetViewModel;
                menuItem.MenuItem = item;
                ToolbarItems.Add(menuItem);
            }
            toolbarListConstructor = ToolbarItems.BeforeItemsAdded.Subscribe(e => e.Construct());
            toolbarAdded = ToolbarItems.BeforeItemsAdded.Subscribe(e => Toolbar.Items.Add(e?.MenuItem));
            toolbarRemoved = ToolbarItems.ItemsRemoved.Subscribe(e => Toolbar.Items.Remove(e?.MenuItem));
        }

        private TViewModel AddAndSelectNewToolbarItem<TViewModel, TItem>(Action<TItem> itemAction, Action<TViewModel> viewModelAction)
            where TViewModel : ToolbarItemViewModel, new()
            where TItem : ToolbarItem, new()
        {
            TItem item = new TItem();
            itemAction?.Invoke(item);
            TViewModel model = RuleSetViewModel.Create<TViewModel>(v =>
            {
                v.MenuItem = item;
            });
            viewModelAction?.Invoke(model);
            ToolbarItems.Add(model);
            SelectedToolbarItem = model;
            return model;
        }
    }
}
