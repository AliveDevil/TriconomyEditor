using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using libUIStack;
using Reactive.Bindings;
using ReactiveUI;
using RuleSet.Elements;
using RuleSetEditor.ViewModels.RuleSetViewModels;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels;
using RuleSetEditor.ViewModels.RuleSetViewModels.ToolbarViewModels;

namespace RuleSetEditor.ViewModels
{
    public class RuleSetViewModel : ViewModelBase, IViewStack
    {
        private IView currentView;
        private ReactiveList<ElementViewModel> elementList;
        private Stack<IView> loadedViews = new Stack<IView>();
        private ReactiveProperty<string> nameProperty;
        private RelayCommand openToolbarCommand;
        private RelayCommand<Type> openViewCommand;
        private ReactiveProperty<ResourceBarViewModel> resourceBarProperty;
        private RuleSet.RuleSet ruleSet;
        private string sourceFilePath;
        private ReactiveProperty<ToolbarViewModel> toolbarProperty;

        public ReactiveList<ElementViewModel> ElementList
        {
            get { return elementList; }
            private set { RaiseSetIfChanged(ref elementList, value); }
        }

        public ReactiveProperty<string> Name
        {
            get { return nameProperty; }
            private set { RaiseSetIfChanged(ref nameProperty, value); }
        }

        public RelayCommand OpenToolbar
        {
            get
            {
                return openToolbarCommand ?? (openToolbarCommand = new RelayCommand(() =>
                {
                    Set(Toolbar.Value);
                }));
            }
        }

        public RelayCommand<Type> OpenView
        {
            get
            {
                return openViewCommand ?? (openViewCommand = new RelayCommand<Type>(t =>
                {
                    Set((ViewModelBase)Activator.CreateInstance(t));
                }));
            }
        }

        public RuleSet.RuleSet RuleSet
        {
            get
            {
                return ruleSet;
            }
            set
            {
                RaiseSetIfChanged(ref ruleSet, value);
                Name = ReactiveProperty.FromObject(RuleSet, r => r.Name);

                ElementList = new ReactiveList<ElementViewModel>(RuleSet.Elements.Select(element =>
                {
                    ElementViewModel viewModel = null;
                    if (element is ResourceGroup) viewModel = new ResourceGroupViewModel();
                    else if (element is WorldResource) viewModel = new WorldResourceViewModel();
                    else if (element is Building) viewModel = new BuildingViewModel();
                    else if (element is Job) viewModel = new JobViewModel();
                    else if (element is Resource) viewModel = new ResourceViewModel();

                    if (viewModel != null)
                    {
                        viewModel.DeferChanged = true;
                        viewModel.RuleSetViewModel = this;
                        viewModel.Element = element;
                    }

                    return viewModel;
                }).Where(e => e != null));
                ElementList.ChangeTrackingEnabled = true;

                foreach (var item in ElementList)
                    item.DeferChanged = false;

                ElementList.BeforeItemsAdded.Subscribe(e => RuleSet.Elements.Add(e.Element));
                ElementList.BeforeItemsRemoved.Subscribe(e => RuleSet.Elements.Remove(e?.Element));

                Set<RuleSetViewModels.LandingPageViewModel>();
            }
        }

        public string SourceFilePath
        {
            get { return sourceFilePath; }
            set { RaiseSetIfChanged(ref sourceFilePath, value); }
        }

        public ReactiveProperty<ToolbarViewModel> Toolbar
        {
            get { return toolbarProperty; }
            private set { RaiseSetIfChanged(ref toolbarProperty, value); }
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
                ((RuleSetViewModelBase)view).RuleSetViewModel = this;
        }

        private void UpdateCurrentView()
        {
            View = loadedViews.Count > 0 ? loadedViews.Peek() : null;
            OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(View)));
        }
    }
}
