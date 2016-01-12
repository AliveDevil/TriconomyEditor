using System.Linq;
using Reactive.Bindings;
using ReactiveUI;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels;

namespace RuleSetEditor.ViewModels.EffectViewModels.EffectEditViewModels
{
    public class WorkplaceEffectEditViewModel : RuleSetViewModelBase
    {
        private ReactiveProperty<JobViewModel> jobProperty;
        private IReactiveDerivedList<JobViewModel> jobs;
        private WorkplaceEffectViewModel workplaceEffect;

        public ReactiveProperty<JobViewModel> Job
        {
            get { return jobProperty; }
            private set { RaiseSetIfChanged(ref jobProperty, value); }
        }

        public IReactiveDerivedList<JobViewModel> Jobs
        {
            get { return jobs; }
            private set { RaiseSetIfChanged(ref jobs, value); }
        }

        public WorkplaceEffectViewModel WorkplaceEffect
        {
            get
            {
                return workplaceEffect;
            }
            set
            {
                if (!RaiseSetIfChanged(ref workplaceEffect, value)) return;
                Job = ReactiveProperty.FromObject(WorkplaceEffect.WorkplaceEffect,
                    j => j.Job,
                    j => Jobs.SingleOrDefault(e => e.Job == j),
                    j => j?.Job);

                Job.PropertyChanged += OnPropertyChanged;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Dispose(ref jobProperty);
                Dispose(ref jobs);
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
