using System.Linq;
using Reactive.Bindings;
using ReactiveUI;
using RuleSet.Effects;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels;

namespace RuleSetEditor.ViewModels.EffectViewModels
{
    public class WorkplaceEffectViewModel : EffectViewModel<WorkplaceEffect>
    {
        private ReactiveProperty<JobViewModel> jobProperty;
        private IReactiveDerivedList<JobViewModel> jobs;

        public ReactiveProperty<JobViewModel> Job
        {
            get
            {
                return jobProperty;
            }
            private set
            {
                RaiseSetIfChanged(ref jobProperty, value);
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

        protected override void OnPostInitialize()
        {
            base.OnPostInitialize();

            Jobs = RuleSetViewModel.ElementList.CreateDerivedCollection(e => (JobViewModel)e, e => e is JobViewModel, (l, r) => l.Name.Value.CompareTo(r.Name.Value));
            Jobs.ChangeTrackingEnabled = true;

            Job = ReactiveProperty.FromObject(Effect,
                    j => j.Job,
                    j => Jobs.SingleOrDefault(e => e.Element == j),
                    j => j?.Element);
            Job.PropertyChanged += OnPropertyChanged;
        }
    }
}
