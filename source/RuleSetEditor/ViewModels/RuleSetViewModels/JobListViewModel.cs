using ReactiveUI;
using RuleSet.Elements;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels;

namespace RuleSetEditor.ViewModels.RuleSetViewModels
{
    public class JobListViewModel : RuleSetViewModelBase
    {
        private RelayCommand addJobCommand;
        private RelayCommand<JobViewModel> editJobCommand;
        private IReactiveDerivedList<JobViewModel> jobs;
        private RelayCommand removeJobCommand;
        private JobViewModel selectedJob;

        public RelayCommand AddJobCommand
        {
            get
            {
                return addJobCommand ?? (addJobCommand = new RelayCommand(() =>
                {
                    JobViewModel model = new JobViewModel()
                    {
                        RuleSetViewModel = RuleSetViewModel,
                        Element = new Job()
                        {
                            Name = "New Job"
                        }
                    };
                    RuleSetViewModel.ElementList.Add(model);
                    ViewStack.Push(SelectedJob = model);
                }));
            }
        }

        public RelayCommand<JobViewModel> EditJobCommand
        {
            get
            {
                return editJobCommand ?? (editJobCommand = new RelayCommand<JobViewModel>(job =>
                {
                    ViewStack.Push(SelectedJob = job);
                }));
            }
        }

        public IReactiveDerivedList<JobViewModel> Jobs
        {
            get
            {
                return jobs;
            }
            private set
            {
                RaiseSetIfChanged(ref jobs, value);
            }
        }

        public RelayCommand RemoveJobCommand
        {
            get
            {
                return removeJobCommand ?? (removeJobCommand = new RelayCommand(() =>
               {
                   RuleSetViewModel.ElementList.Remove(SelectedJob);
               }));
            }
        }

        public JobViewModel SelectedJob
        {
            get
            {
                return selectedJob;
            }
            set
            {
                RaiseSetIfChanged(ref selectedJob, value);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Dispose(ref jobs);
                Dispose(ref addJobCommand);
                Dispose(ref editJobCommand);
                Dispose(ref removeJobCommand);
            }
            base.Dispose(disposing);
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Jobs = RuleSetViewModel.ElementList.CreateDerivedCollection(e => (JobViewModel)e, e => e is JobViewModel, (l, r) => l.Name.Value.CompareTo(r.Name.Value));
            Jobs.ChangeTrackingEnabled = true;
        }
    }
}
