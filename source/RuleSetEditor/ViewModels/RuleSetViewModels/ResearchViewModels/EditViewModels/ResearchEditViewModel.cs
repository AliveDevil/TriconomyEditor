using Reactive.Bindings;
using ReactiveUI;
using RuleSet.Elements;
using RuleSetEditor.ViewModels.RuleSetViewModels.ResourcePartViewModels;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ResearchViewModels.EditViewModels
{
    public class ResearchEditViewModel : RuleSetViewModelBase
    {
        private RelayCommand addCostCommand;
        private ReactiveList<ResourcePartViewModel> costs;
        private RelayCommand<ResourcePartViewModel> editCostCommand;
        private ReactiveProperty<string> name;
        private RelayCommand removeCostCommand;
        private ResearchViewModel research;
        private ResourcePartViewModel selectedCost;
        private ReactiveProperty<int> time;

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
                Dispose(ref editCostCommand);
                Dispose(ref removeCostCommand);
            }
            base.Dispose(disposing);
        }
    }
}
