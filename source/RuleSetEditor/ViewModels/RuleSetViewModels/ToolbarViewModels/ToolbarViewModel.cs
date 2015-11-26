using System;
using System.Collections.Generic;
using System.Linq;
using libUIStack;
using Reactive.Bindings;
using ReactiveUI;
using RuleSet.Menus;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ToolbarViewModels
{
    public class ToolbarViewModel : RuleSetViewModelBase, IViewStack
    {
        private IView currentView;
        private Stack<IView> loadedViews = new Stack<IView>();
        private ReactiveProperty<string> nameProperty;
        private Toolbar toolbar;
        private ReactiveList<ToolbarItemViewModel> toolbarItems;
        private IDisposable itemAddedSubscription;
        private IDisposable itemsRemovedSubscription;

        public ReactiveProperty<string> Name
        {
            get { return nameProperty; }
            private set { RaiseSetIfChanged(ref nameProperty, value); }
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

        public void Clear()
        {
            ClearInternal();
            UpdateCurrentView();
        }

        public bool IsOpen(Type viewType)
        {
            foreach (var view in loadedViews)
                if (view.GetType() == viewType)
                    return true;
            return false;
        }

        public void Pop()
        {
            PopInternal();
            UpdateCurrentView();
        }

        public T Push<T>() where T : IView, new()
        {
            return Push(new T());
        }

        public T Push<T>(T view) where T : IView
        {
            PushInternal(view);
            UpdateCurrentView();
            return view;
        }

        public T Set<T>() where T : IView, new()
        {
            return Set(new T());
        }

        public T Set<T>(T view) where T : IView
        {
            SetInternal(view);
            UpdateCurrentView();
            return view;
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

            Set<ToolbarEditViewModel>();
        }

        private void ClearInternal()
        {
            while (loadedViews.Count > 0)
                PopInternal();
        }

        private void PopInternal()
        {
            loadedViews.Pop().Dispose();
        }

        private void PushInternal(IView view)
        {
            SetProperties(view);
            loadedViews.Push(view);
        }

        private void SetInternal(IView view)
        {
            ClearInternal();
            PushInternal(view);
        }

        private void SetProperties(IView view)
        {
            view.ViewStack = this;
            if (view is RuleSetViewModelBase)
                ((RuleSetViewModelBase)view).RuleSetViewModel = RuleSetViewModel;
            if (view is ToolbarEditViewModel)
                ((ToolbarEditViewModel)view).Toolbar = this;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Name.Dispose();
                itemAddedSubscription.Dispose();
                itemsRemovedSubscription.Dispose();
                ClearInternal();
                foreach (var item in ToolbarItems)
                {
                    item.Dispose();
                }
                ToolbarItems.Clear();
            }
            base.Dispose(disposing);
        }

        private void UpdateCurrentView()
        {
            View = loadedViews.Count > 0 ? loadedViews.Peek() : null;
        }
    }
}
