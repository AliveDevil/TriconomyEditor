using System.Linq;
using ReactiveUI;
using RuleSet.Elements;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels.JobViewModels;

namespace RuleSetEditor.ViewModels.RuleSetViewModels
{
    public class JobListViewModel : RuleSetViewModelBase
    {
        private IReactiveDerivedList<JobViewModel> jobs;
        private JobViewModel selectedJob;

        public RelayCommand AddJobCommand => new RelayCommand(() =>
        {
            JobViewModel model = new JobViewModel() { RuleSetViewModel = RuleSetViewModel, Element = new Job() { Name = "New Job" } };
            RuleSetViewModel.ElementList.Add(model);
            SelectedJob = model;
        });

        public RelayCommand<JobViewModel> EditJobCommand => new RelayCommand<JobViewModel>(job =>
        {
            SelectedJob = job;
            ViewStack.Push<JobEditViewModel>()._(_ => _.Job = job);
        });

        public IReactiveDerivedList<JobViewModel> Jobs
        {
            get { return jobs; }
            private set { RaiseSetIfChanged(ref jobs, value); }
        }

        public RelayCommand RemoveJobCommand => new RelayCommand(() =>
        {
            RuleSetViewModel.ElementList.Remove(SelectedJob);
        });

        public JobViewModel SelectedJob
        {
            get { return selectedJob; }
            set { RaiseSetIfChanged(ref selectedJob, value); }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Dispose(ref jobs);
                AddJobCommand.Dispose();
                EditJobCommand.Dispose();
                RemoveJobCommand.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void OnRuleSetChanged()
        {
            base.OnRuleSetChanged();
            Jobs = RuleSetViewModel.ElementList.CreateDerivedCollection(e => (JobViewModel)e, e => e is JobViewModel, (l, r) => l.Name.Value.CompareTo(r.Name.Value));
            Jobs.ChangeTrackingEnabled = true;
        }
    }
}
