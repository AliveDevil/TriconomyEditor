using Reactive.Bindings;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels.JobViewModels
{
    public class JobEditViewModel : RuleSetViewModelBase
    {
        private JobViewModel job;
        private ReactiveProperty<string> nameProperty;

        public JobViewModel Job
        {
            get
            {
                return job;
            }
            set
            {
                RaiseSetIfChanged(ref job, value);
                Name = Job.Name;
            }
        }

        public ReactiveProperty<string> Name
        {
            get { return nameProperty; }
            private set { RaiseSetIfChanged(ref nameProperty, value); }
        }
    }
}
