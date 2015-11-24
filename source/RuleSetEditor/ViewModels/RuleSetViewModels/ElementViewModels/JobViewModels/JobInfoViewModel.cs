using Reactive.Bindings;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels.JobViewModels
{
    public class JobInfoViewModel : RuleSetViewModelBase
    {
        private ReadOnlyReactiveProperty<string> nameProperty;
        private JobViewModel job;

        public ReadOnlyReactiveProperty<string> Name
        {
            get { return nameProperty; }
            private set { RaiseSetIfChanged(ref nameProperty, value); }
        }

        public JobViewModel Job
        {
            get
            {
                return job;
            }
            set
            {
                RaiseSetIfChanged(ref job, value);
                Name = Job.Name.ToReadOnlyReactiveProperty();
            }
        }

        public override string ToString()
        {
            return Name.Value;
        }
    }
}
