using ReactiveUI;
using RuleSet;
using RuleSetEditor.ViewModels.RuleSetViewModels.ResearchViewModels;

namespace RuleSetEditor.ViewModels.RuleSetViewModels
{
    public class ResearchListViewModel : RuleSetViewModelBase
    {
        private RelayCommand addResearchCommand;
        private RelayCommand<ResearchViewModel> editResearchCommand;
        private RelayCommand removeResearchCommand;
        private IReactiveDerivedList<ResearchViewModel> research;
        private ResearchViewModel selectedResearch;

        public RelayCommand AddResearchCommand => addResearchCommand ?? (addResearchCommand = new RelayCommand(() =>
        {
            RuleSetViewModel.Research.Add(ViewStack.Push(RuleSetViewModel.Create<ResearchViewModel>(v =>
            {
                v.Research = new Research() { Name = "New Research" };
            })));
        }));

        public RelayCommand<ResearchViewModel> EditResearchCommand => editResearchCommand ?? (editResearchCommand = new RelayCommand<ResearchViewModel>(research =>
        {
            ViewStack.Push(SelectedResearch = research);
        }));

        public RelayCommand RemoveResearchCommand => removeResearchCommand ?? (removeResearchCommand = new RelayCommand(() =>
        {
            RuleSetViewModel.Research.Remove(SelectedResearch);
        }));

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

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Research = RuleSetViewModel.Research.CreateDerivedCollection(e => e, null, (l, r) => l.Name.Value.CompareTo(r.Name.Value));
            Research.ChangeTrackingEnabled = true;
        }
    }
}
