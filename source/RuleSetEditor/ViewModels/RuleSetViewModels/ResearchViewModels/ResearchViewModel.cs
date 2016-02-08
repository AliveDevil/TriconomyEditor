using System;
using Reactive.Bindings;
using ReactiveUI;
using RuleSet;
using RuleSet.Elements;
using RuleSetEditor.ViewModels.RuleSetViewModels.ConditionViewModels;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ResearchViewModels
{
    public class ResearchViewModel : RuleSetViewModelBase
    {
        private RelayCommand<Type> addConditionCommand;
        private RelayCommand addCostCommand;
        private IDisposable beforeConditionAdded;
        private IDisposable beforeConditionRemoved;
        private IDisposable beforeCostAdded;
        private IDisposable beforeCostRemoved;
        private IDisposable conditionAdded;
        private IDisposable conditionListConstructor;
        private IDisposable conditionListInitializer;
        private IDisposable conditionListPostDisposer;
        private IDisposable conditionListPostInitializer;
        private IDisposable conditionRemoved;
        private ReactiveList<ConditionViewModel> conditions;
        private IDisposable costAdded;
        private IDisposable costListConstructor;
        private IDisposable costListInitializer;
        private IDisposable costListPostDisposer;
        private IDisposable costListPostInitializer;
        private IDisposable costRemoved;
        private ReactiveList<ResourcePartViewModel> costs;
        private RelayCommand<ConditionViewModel> editConditionCommand;
        private RelayCommand<ResourcePartViewModel> editCostCommand;
        private ReactiveProperty<string> name;
        private RelayCommand removeConditionCommand;
        private RelayCommand removeCostCommand;
        private Research research;
        private ConditionViewModel selectedCondition;
        private ResourcePartViewModel selectedCost;
        private ReactiveProperty<int> timeProperty;

        public RelayCommand<Type> AddConditionCommand => addConditionCommand ?? (addConditionCommand = new RelayCommand<Type>(t =>
        {
            var condition = (Condition)Activator.CreateInstance(t);
            var viewModel = ConditionViewModel.FindViewModel(condition, RuleSetViewModel);
            Conditions.Add(ViewStack.Push(SelectedCondition = viewModel));
        }));

        public RelayCommand AddCostCommand => addCostCommand ?? (addCostCommand = new RelayCommand(() =>
        {
            Costs.Add(ViewStack.Push(RuleSetViewModel.Create<ResourcePartViewModel>(f =>
            {
                f.ResourcePart = new ResourcePart();
            })));
        }));

        public ReactiveList<ConditionViewModel> Conditions
        {
            get
            {
                return conditions;
            }
            private set
            {
                RaiseSetIfChanged(ref conditions, value);
            }
        }

        public ReactiveList<ResourcePartViewModel> Costs
        {
            get
            {
                return costs;
            }
            private set
            {
                RaiseSetIfChanged(ref costs, value);
            }
        }

        public RelayCommand<ConditionViewModel> EditConditionCommand => editConditionCommand ?? (editConditionCommand = new RelayCommand<ConditionViewModel>(condition =>
        {
            ViewStack.Push(SelectedCondition = condition);
        }));

        public RelayCommand<ResourcePartViewModel> EditCostCommand => editCostCommand ?? (editCostCommand = new RelayCommand<ResourcePartViewModel>(resourcePart =>
        {
            ViewStack.Push(SelectedCost = resourcePart);
        }));

        public ReactiveProperty<string> Name
        {
            get
            {
                return name;
            }
            private set
            {
                RaiseSetIfChanged(ref name, value);
            }
        }

        public RelayCommand RemoveConditionCommand => removeConditionCommand ?? (removeConditionCommand = new RelayCommand(() =>
        {
            Conditions.Remove(SelectedCondition);
        }));

        public RelayCommand RemoveCostCommand => removeCostCommand ?? (removeCostCommand = new RelayCommand(() =>
        {
            Costs.Remove(SelectedCost);
        }));

        public Research Research
        {
            get
            {
                return research;
            }
            set
            {
                if (!RaiseSetIfChanged(ref research, value))
                    return;
            }
        }

        public ConditionViewModel SelectedCondition
        {
            get
            {
                return selectedCondition;
            }
            set
            {
                RaiseSetIfChanged(ref selectedCondition, value);
            }
        }

        public ResourcePartViewModel SelectedCost
        {
            get
            {
                return selectedCost;
            }
            set
            {
                RaiseSetIfChanged(ref selectedCost, value);
            }
        }

        public ReactiveProperty<int> Time
        {
            get
            {
                return timeProperty;
            }
            private set
            {
                RaiseSetIfChanged(ref timeProperty, value);
            }
        }
        
        protected override void OnInitialize()
        {
            base.OnInitialize();

            Name = ReactiveProperty.FromObject(Research, r => r.Name);
            Name.PropertyChanged += OnPropertyChanged;

            Time = ReactiveProperty.FromObject(Research, r => r.Time);

            Conditions = new ReactiveList<ConditionViewModel>() { ChangeTrackingEnabled = true };
            conditionListInitializer = Conditions.BeforeItemsAdded.Subscribe(c => c.Initialize());
            conditionListPostInitializer = Conditions.ItemsAdded.Subscribe(c => c.PostInitialize());
            conditionListPostDisposer = Conditions.BeforeItemsRemoved.Subscribe(c => c.Dispose());

            Costs = new ReactiveList<ResourcePartViewModel>() { ChangeTrackingEnabled = true };
            costListInitializer = Costs.BeforeItemsAdded.Subscribe(c => c.Initialize());
            costListPostInitializer = Costs.ItemsAdded.Subscribe(c => c.PostInitialize());
            costListPostDisposer = Costs.BeforeItemsRemoved.Subscribe(c => c.Dispose());
        }

        protected override void OnPostInitialize()
        {
            base.OnPostInitialize();
            foreach (var item in Research.Conditions)
            {
                Conditions.Add(ConditionViewModel.FindViewModel(item, RuleSetViewModel));
            }
            conditionListConstructor = Conditions.BeforeItemsAdded.Subscribe(c => c.Construct());
            conditionAdded = Conditions.BeforeItemsAdded.Subscribe(c => Research.Conditions.Add(c.Condition));
            conditionRemoved = Conditions.ItemsRemoved.Subscribe(c => Research.Conditions.Remove(c.Condition));

            foreach (var item in Research.Costs)
            {
                Costs.Add(RuleSetViewModel.Create<ResourcePartViewModel>(v =>
                {
                    v.ResourcePart = item;
                }));
            }
            costListConstructor = Costs.BeforeItemsAdded.Subscribe(c => c.Construct());
            costAdded = Costs.BeforeItemsAdded.Subscribe(c => Research.Costs.Add(c.ResourcePart));
            costRemoved = Costs.ItemsRemoved.Subscribe(c => Research.Costs.Remove(c.ResourcePart));
        }
    }
}
