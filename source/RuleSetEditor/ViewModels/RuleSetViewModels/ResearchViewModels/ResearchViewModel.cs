using System;
using System.Linq;
using Reactive.Bindings;
using ReactiveUI;
using RuleSet;
using RuleSetEditor.ViewModels.RuleSetViewModels.ConditionViewModels;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ResearchViewModels
{
    public class ResearchViewModel : RuleSetViewModelBase
    {
        private IDisposable beforeConditionAdded;
        private IDisposable beforeConditionRemoved;
        private IDisposable beforeCostAdded;
        private IDisposable beforeCostRemoved;
        private ReactiveList<ConditionViewModel> conditions;
        private ReactiveList<ResourcePartViewModel> costs;
        private ReactiveProperty<string> name;
        private Research research;
        private ReactiveProperty<int> timeProperty;

        public ReactiveList<ConditionViewModel> Conditions
        {
            get
            {
                return conditions;
            }
            private set
            {
                RaiseSetIfChanged(ref conditions, value);
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

        public ReactiveProperty<string> Name
        {
            get
            {
                return name;
            }
            private set
            {
                RaiseSetIfChanged(ref name, value);
            }
        }

        public Research Research
        {
            get
            {
                return research;
            }
            set
            {
                if (!RaiseSetIfChanged(ref research, value))
                    return;

                if (!DeferChanged)
                    OnElementChanged();
                else
                    DeferQueue.Enqueue(OnElementChanged);
            }
        }

        public ReactiveProperty<int> Time
        {
            get
            {
                return timeProperty;
            }
            private set
            {
                RaiseSetIfChanged(ref timeProperty, value);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Dispose(ref beforeConditionAdded);
                Dispose(ref beforeConditionRemoved);
                Dispose(ref beforeCostAdded);
                Dispose(ref beforeCostRemoved);
                Dispose(ref timeProperty);

                foreach (var item in Conditions)
                    item.Dispose();
                foreach (var item in Costs)
                    item.Dispose();

                Conditions.Clear();
                Costs.Clear();

                conditions = null;
                costs = null;
            }
            base.Dispose(disposing);
        }

        private void OnElementChanged()
        {
            Name = ReactiveProperty.FromObject(Research, r => r.Name);
            Name.PropertyChanged += OnPropertyChanged;

            Conditions = new ReactiveList<ConditionViewModel>(Research.Conditions.Select(e => ConditionViewModel.FindViewModel(e, RuleSetViewModel)))
            {
                ChangeTrackingEnabled = true
            };
            beforeConditionAdded = Conditions.BeforeItemsAdded.Subscribe(e => Research.Conditions.Add(e?.Condition));
            beforeConditionRemoved = Conditions.BeforeItemsRemoved.Subscribe(e => Research.Conditions.Remove(e?.Condition));

            Costs = new ReactiveList<ResourcePartViewModel>(Research.Costs.Select(e => new ResourcePartViewModel()
            {
                RuleSetViewModel = RuleSetViewModel,
                ResourcePart = e
            }))
            {
                ChangeTrackingEnabled = true
            };
            beforeCostAdded = Costs.BeforeItemsAdded.Subscribe(e => Research.Costs.Add(e?.ResourcePart));
            beforeCostRemoved = Costs.BeforeItemsRemoved.Subscribe(e => Research.Costs.Remove(e?.ResourcePart));
        }
    }
}
