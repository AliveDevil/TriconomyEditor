using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive.Linq;
using libUIStack;
using Reactive.Bindings;
using ReactiveUI;
using RuleSet.Elements;
using RuleSet.Needs;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels;
using RuleSetEditor.ViewModels.RuleSetViewModels.NeedViewModels;
using RuleSetEditor.ViewModels.RuleSetViewModels.ResearchViewModels;

namespace RuleSetEditor.ViewModels
{
    public class RuleSetViewModel : ViewModelBase, IViewStack
    {
        private IView currentView;
        private IDisposable elementAdded;
        private ReactiveList<ElementViewModel> elementList;
        private IDisposable elementListConstructor;
        private IDisposable elementListInitializer;
        private IDisposable elementListPostDisposer;
        private IDisposable elementListPostInitializer;
        private IDisposable elementRemoved;
        private Stack<IView> loadedViews = new Stack<IView>();
        private ReactiveProperty<string> nameProperty;
        private ReactiveList<NeedViewModel> needs;
        private IDisposable needsAdded;
        private IDisposable needsListConstructor;
        private IDisposable needsListInitializer;
        private IDisposable needsListPostDisposer;
        private IDisposable needsListPostInitializer;
        private IDisposable needsRemoved;
        private RelayCommand<Type> openViewCommand;
        private ReactiveList<ResearchViewModel> research;
        private IDisposable researchAdded;
        private IDisposable researchListConstructor;
        private IDisposable researchListInitializer;
        private IDisposable researchListPostDisposer;
        private IDisposable researchListPostInitializer;
        private IDisposable researchRemoved;
        private RuleSet.RuleSet ruleSet;
        private string sourceFilePath;
        private ReactiveList<ResourcePartViewModel> startInventory;
        private IDisposable startResourceAdded;
        private IDisposable startResourceListConstructor;
        private IDisposable startResourceRemoved;
        private IDisposable startResourcesListInitializer;
        private IDisposable startResourcesListPostDisposer;
        private IDisposable startResourcesListPostInitializer;

        public ReactiveList<ElementViewModel> ElementList
        {
            get
            {
                return elementList;
            }
            private set
            {
                RaiseSetIfChanged(ref elementList, value);
            }
        }

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

        public ReactiveList<NeedViewModel> Needs
        {
            get
            {
                return needs;
            }
            private set
            {
                RaiseSetIfChanged(ref needs, value);
            }
        }

        public RelayCommand<Type> OpenView
        {
            get
            {
                return openViewCommand ?? (openViewCommand = new RelayCommand<Type>(t =>
                {
                    var instance = (RuleSetViewModelBase)Activator.CreateInstance(t);
                    instance.RuleSetViewModel = this;
                    instance.ViewStack = this;
                    instance.Initialize();
                    Set(instance);
                    instance.PostInitialize();
                }));
            }
        }

        public ReactiveList<ResearchViewModel> Research
        {
            get
            {
                return research;
            }
            private set
            {
                RaiseSetIfChanged(ref research, value);
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
            }
        }

        public string SourceFilePath
        {
            get
            {
                return sourceFilePath;
            }
            set
            {
                RaiseSetIfChanged(ref sourceFilePath, value);
            }
        }

        public ReactiveList<ResourcePartViewModel> StartResources
        {
            get
            {
                return startInventory;
            }
            private set
            {
                RaiseSetIfChanged(ref startInventory, value);
            }
        }

        public IView View
        {
            get
            {
                return currentView;
            }
            private set
            {
                RaiseSetIfChanged(ref currentView, value);
            }
        }

        public void Clear()
        {
            ClearInternal();
            UpdateCurrentView();
        }

        public T Create<T>(Action<T> preInitializer) where T : RuleSetViewModelBase, new()
        {
            return new T()
            {
                RuleSetViewModel = this,
                ViewStack = this
            }._(preInitializer);
        }

        public void Initialize()
        {
            Name = ReactiveProperty.FromObject(RuleSet, r => r.Name);

            ElementList = new ReactiveList<ElementViewModel>() { ChangeTrackingEnabled = true };
            elementListInitializer = ElementList.BeforeItemsAdded.Subscribe(e => e.Initialize());
            elementListPostDisposer = ElementList.ItemsRemoved.Subscribe(e => e.Dispose());

            Needs = new ReactiveList<NeedViewModel>() { ChangeTrackingEnabled = true };
            needsListInitializer = Needs.BeforeItemsAdded.Subscribe(n => n.Initialize());
            needsListPostDisposer = Needs.BeforeItemsRemoved.Subscribe(n => n.Dispose());

            Research = new ReactiveList<ResearchViewModel>() { ChangeTrackingEnabled = true };
            researchListInitializer = Research.BeforeItemsAdded.Subscribe(n => n.Initialize());
            researchListPostDisposer = Research.BeforeItemsRemoved.Subscribe(n => n.Dispose());

            StartResources = new ReactiveList<ResourcePartViewModel>() { ChangeTrackingEnabled = true };
            startResourcesListInitializer = StartResources.BeforeItemsAdded.Subscribe(n => n.Initialize());
            startResourcesListPostDisposer = StartResources.BeforeItemsRemoved.Subscribe(n => n.Dispose());
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

        public void PostInitialize()
        {
            foreach (var item in RuleSet.Elements)
            {
                ElementViewModel viewModel = null;
                if (item is ResourceGroup)
                    viewModel = new ResourceGroupViewModel();
                else if (item is WorldResource)
                    viewModel = new WorldResourceViewModel();
                else if (item is LivingResource)
                    viewModel = new LivingResourceViewModel();
                else if (item is Building)
                    viewModel = new BuildingViewModel();
                else if (item is Job)
                    viewModel = new JobViewModel();
                else if (item is Resource)
                    viewModel = new ResourceViewModel();
                else
                    continue;

                viewModel.RuleSetViewModel = this;
                viewModel.ViewStack = this;
                viewModel.Element = item;
                ElementList.Add(viewModel);
            }

            elementListPostInitializer = ElementList.ItemsAdded.Subscribe(e => e.PostInitialize());
            elementListConstructor = ElementList.BeforeItemsAdded.Subscribe(e => e.Construct());
            elementAdded = ElementList.BeforeItemsAdded.Subscribe(e => RuleSet.Elements.Add(e?.Element));
            elementRemoved = ElementList.ItemsRemoved.Subscribe(e => RuleSet.Elements.Remove(e?.Element));

            foreach (var item in RuleSet.Needs)
            {
                NeedViewModel viewModel = null;

                if (item is ResourceNeed)
                    viewModel = new ResourceNeedViewModel();
                else if (item is BuildingNeed)
                    viewModel = new BuildingNeedViewModel();
                else
                    continue;

                viewModel.RuleSetViewModel = this;
                viewModel.ViewStack = this;
                viewModel.Need = item;

                Needs.Add(viewModel);
            }

            needsListPostInitializer = Needs.ItemsAdded.Subscribe(n => n.PostInitialize());
            needsListConstructor = Needs.BeforeItemsAdded.Subscribe(e => e.Construct());
            needsAdded = Needs.BeforeItemsAdded.Subscribe(e => RuleSet.Needs.Add(e?.Need));
            needsRemoved = Needs.ItemsRemoved.Subscribe(e => RuleSet.Needs.Remove(e?.Need));

            foreach (var item in RuleSet.Research)
            {
                Research.Add(new ResearchViewModel()
                {
                    RuleSetViewModel = this,
                    ViewStack = this,
                    Research = item
                });
            }

            researchListPostInitializer = Research.ItemsAdded.Subscribe(n => n.PostInitialize());
            researchListConstructor = Research.BeforeItemsAdded.Subscribe(e => e.Construct());
            researchAdded = Research.BeforeItemsAdded.Subscribe(e => RuleSet.Research.Add(e?.Research));
            researchRemoved = Research.ItemsRemoved.Subscribe(e => RuleSet.Research.Remove(e?.Research));

            foreach (var item in RuleSet.StartResources)
            {
                StartResources.Add(new ResourcePartViewModel()
                {
                    RuleSetViewModel = this,
                    ViewStack = this,
                    ResourcePart = item
                });
            }

            startResourcesListPostInitializer = StartResources.ItemsAdded.Subscribe(n => n.PostInitialize());
            startResourceListConstructor = StartResources.BeforeItemsAdded.Subscribe(e => e.Construct());
            startResourceAdded = StartResources.BeforeItemsAdded.Subscribe(e => RuleSet.StartResources.Add(e?.ResourcePart));
            startResourceRemoved = StartResources.ItemsRemoved.Subscribe(e => RuleSet.StartResources.Remove(e?.ResourcePart));

            foreach (var item in ElementList)
                item.PostInitialize();
            foreach (var item in Needs)
                item.PostInitialize();
            foreach (var item in Research)
                item.PostInitialize();
            foreach (var item in StartResources)
                item.PostInitialize();

            Set(Create<RuleSetViewModels.LandingPageViewModel>(v =>
            {
                v.Initialize();
            }));
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

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
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
            loadedViews.Push(view);
        }

        private void SetInternal(IView view)
        {
            ClearInternal();
            PushInternal(view);
        }

        private void UpdateCurrentView()
        {
            View = loadedViews.Count > 0 ? loadedViews.Peek() : null;
            OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(View)));
        }
    }
}
