using ReactiveUI;
using RuleSet.Elements;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels.JobViewModels;

namespace RuleSetEditor.ViewModels.RuleSetViewModels
{
    public class JobListViewModel : RuleSetViewModelBase
    {
        private RelayCommand addJobCommand;
        private RelayCommand<JobInfoViewModel> editJobCommand;
        private IReactiveDerivedList<JobInfoViewModel> jobs;
        private RelayCommand removeJobCommand;
        private JobInfoViewModel selectedJob;

        public RelayCommand AddJobCommand
        {
            get
            {
                return addJobCommand ?? (addJobCommand = new RelayCommand(() =>
                {
                    RuleSetViewModel.ElementList.Add(new JobViewModel() { RuleSetViewModel = RuleSetViewModel, Element = new Job() { Name = "New Job" } });
                }));
            }
        }

        public RelayCommand<JobInfoViewModel> EditJobCommand
        {
            get
            {
                return editJobCommand ?? (editJobCommand = new RelayCommand<JobInfoViewModel>(job =>
                {
                    ViewStack.Push<JobEditViewModel>()._(_ => _.Job = job.Job);
                }));
            }
        }

        public IReactiveDerivedList<JobInfoViewModel> Jobs
        {
            get { return jobs; }
            private set { RaiseSetIfChanged(ref jobs, value); }
        }

        public RelayCommand RemoveJobCommand
        {
            get
            {
                return removeJobCommand ?? (removeJobCommand = new RelayCommand(() =>
                {
                    RuleSetViewModel.ElementList.Remove(SelectedJob?.Job);
                }));
            }
        }

        public JobInfoViewModel SelectedJob
        {
            get { return selectedJob; }
            set { RaiseSetIfChanged(ref selectedJob, value); }
        }

        protected override void OnRuleSetChanged()
        {
            base.OnRuleSetChanged();
            Jobs = RuleSetViewModel.ElementList.CreateDerivedCollection(e => new JobInfoViewModel() { Job = (JobViewModel)e }, e => e is JobViewModel);
        }
    }
}
