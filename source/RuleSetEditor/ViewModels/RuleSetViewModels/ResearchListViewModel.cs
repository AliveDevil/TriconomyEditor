using ReactiveUI;
using RuleSet;
using RuleSetEditor.ViewModels.RuleSetViewModels.ResearchViewModels;
using RuleSetEditor.ViewModels.RuleSetViewModels.ResearchViewModels.EditViewModels;

namespace RuleSetEditor.ViewModels.RuleSetViewModels
{
    public class ResearchListViewModel : RuleSetViewModelBase
    {
        private RelayCommand addResearchCommand;
        private RelayCommand<ResearchViewModel> editResearchCommand;
        private IReactiveDerivedList<ResearchViewModel> research;
        private RelayCommand removeResearchCommand;
        private ResearchViewModel selectedResearch;

        public RelayCommand AddResearchCommand
        {
            get
            {
                return addResearchCommand ?? (addResearchCommand = new RelayCommand(() =>
                {
                    ResearchViewModel model = new ResearchViewModel() { RuleSetViewModel = RuleSetViewModel, Research = new Research() { Name = "New Research" } };
                    RuleSetViewModel.Research.Add(model);
                    SelectedResearch = model;
                }));
            }
        }

        public RelayCommand<ResearchViewModel> EditResearchCommand
        {
            get
            {
                return editResearchCommand ?? (editResearchCommand = new RelayCommand<ResearchViewModel>(research =>
                {
                    SelectedResearch = research;
                    ViewStack.Push<ResearchEditViewModel>()._(_ =>
                    {
                        _.RuleSetViewModel = RuleSetViewModel;
                        _.Research = research;
                    });
                }));
            }
        }

        public IReactiveDerivedList<ResearchViewModel> Research
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

        public RelayCommand RemoveResearchCommand
        {
            get
            {
                return removeResearchCommand ?? (removeResearchCommand = new RelayCommand(() =>
                {
                    RuleSetViewModel.Research.Remove(SelectedResearch);
                }));
            }
        }

        public ResearchViewModel SelectedResearch
        {
            get
            {
                return selectedResearch;
            }
            set
            {
                RaiseSetIfChanged(ref selectedResearch, value);
            }
        }

        protected override void OnRuleSetChanged()
        {
            base.OnRuleSetChanged();
            Research = RuleSetViewModel.Research.CreateDerivedCollection(e => e);
            Research.ChangeTrackingEnabled = true;
        }
    }
}
