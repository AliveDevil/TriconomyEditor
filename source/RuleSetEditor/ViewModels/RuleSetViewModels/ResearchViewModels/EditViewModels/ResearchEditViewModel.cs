using System;
using Reactive.Bindings;
using ReactiveUI;
using RuleSet;
using RuleSet.Elements;
using RuleSetEditor.ViewModels.RuleSetViewModels.ConditionViewModels;
using RuleSetEditor.ViewModels.RuleSetViewModels.ResourcePartViewModels;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ResearchViewModels.EditViewModels
{
    public class ResearchEditViewModel : RuleSetViewModelBase
    {
        private RelayCommand<Type> addConditionCommand;
        private RelayCommand addCostCommand;
        private ReactiveList<ConditionViewModel> conditions;
        private ReactiveList<ResourcePartViewModel> costs;
        private RelayCommand<ConditionViewModel> editConditionCommand;
        private RelayCommand<ResourcePartViewModel> editCostCommand;
        private ReactiveProperty<string> name;
        private RelayCommand removeConditionCommand;
        private RelayCommand removeCostCommand;
        private ResearchViewModel research;
        private ConditionViewModel selectedCondition;
        private ResourcePartViewModel selectedCost;
        private ReactiveProperty<int> time;

        public RelayCommand<Type> AddConditionCommand
        {
            get
            {
                return addConditionCommand ?? (addConditionCommand = new RelayCommand<Type>(t =>
                {
                    var condition = (Condition)Activator.CreateInstance(t);
                    var viewModel = ConditionViewModel.FindViewModel(condition, RuleSetViewModel);
                    Research.Conditions.Add(viewModel);
                    SelectedCondition = viewModel;
                    ViewStack.Push(ConditionViewModel.FindEditViewModel(viewModel, RuleSetViewModel));
                    //ResourcePartViewModel model = new ResourcePartViewModel()
                    //{
                    //    RuleSetViewModel = RuleSetViewModel,
                    //    ResourcePart = new ResourcePart()
                    //};
                    //Research.Costs.Add(model);
                    //SelectedCost = model;
                }));
            }
        }

        public RelayCommand AddCostCommand
        {
            get
            {
                return addCostCommand ?? (addCostCommand = new RelayCommand(() =>
                {
                    ResourcePartViewModel model = new ResourcePartViewModel()
                    {
                        RuleSetViewModel = RuleSetViewModel,
                        ResourcePart = new ResourcePart()
                    };
                    Research.Costs.Add(model);
                    SelectedCost = model;
                }));
            }
        }

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

        public RelayCommand<ConditionViewModel> EditConditionCommand
        {
            get
            {
                return editConditionCommand ?? (editConditionCommand = new RelayCommand<ConditionViewModel>(condition =>
                {
                    SelectedCondition = condition;
                    ViewStack.Push(ConditionViewModel.FindEditViewModel(condition, RuleSetViewModel));
                }));
            }
        }

        public RelayCommand<ResourcePartViewModel> EditCostCommand
        {
            get
            {
                return editCostCommand ?? (editCostCommand = new RelayCommand<ResourcePartViewModel>(resourcePart =>
                {
                    SelectedCost = resourcePart;
                    ViewStack.Push<ResourcePartEditViewModel>()._(_ =>
                    {
                        _.ResourcePart = resourcePart;
                    });
                }));
            }
        }

        public ReactiveProperty<string> Name
        {
            get
            {
                return name;
            }
            set
            {
                RaiseSetIfChanged(ref name, value);
            }
        }

        public RelayCommand RemoveConditionCommand
        {
            get
            {
                return removeCostCommand ?? (removeCostCommand = new RelayCommand(() =>
                {
                    Research.Conditions.Remove(SelectedCondition);
                }));
            }
        }

        public RelayCommand RemoveCostCommand
        {
            get
            {
                return removeCostCommand ?? (removeCostCommand = new RelayCommand(() =>
                {
                    Research.Costs.Remove(SelectedCost);
                }));
            }
        }

        public ResearchViewModel Research
        {
            get
            {
                return research;
            }
            set
            {
                if (!RaiseSetIfChanged(ref research, value))
                    return;
                Name = Research.Name;
                Time = Research.Time;
                Costs = Research.Costs;
                Conditions = Research.Conditions;
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
                return time;
            }
            set
            {
                if (time == value)
                    return;
                time = value;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Dispose(ref addCostCommand);
                Dispose(ref addConditionCommand);
                Dispose(ref editCostCommand);
                Dispose(ref editConditionCommand);
                Dispose(ref removeCostCommand);
                Dispose(ref removeConditionCommand);
            }
            base.Dispose(disposing);
        }
    }
}
