using System.Linq;
using Reactive.Bindings;
using ReactiveUI;
using RuleSet.Conditions;
using RuleSetEditor.ViewModels.RuleSetViewModels.ResearchViewModels;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ConditionViewModels
{
    public class ResearchConditionViewModel : ConditionViewModel<ResearchCondition>
    {
        private ReactiveProperty<ResearchViewModel> research;
        private IReactiveDerivedList<ResearchViewModel> researchList;

        public ReactiveProperty<ResearchViewModel> Research
        {
            get
            {
                return research;
            }
            set
            {
                if (research == value)
                    return;
                research = value;
            }
        }

        public IReactiveDerivedList<ResearchViewModel> ResearchList
        {
            get
            {
                return researchList;
            }
            private set
            {
                RaiseSetIfChanged(ref researchList, value);
            }
        }

        protected override void OnConditionChanged()
        {
            base.OnConditionChanged();
            Research = ReactiveProperty.FromObject(Condition,
                r => r.Research,
                r => ResearchList.SingleOrDefault(e => e.Research == r),
                r => r?.Research);
        }

        protected override void OnRuleSetChanged()
        {
            base.OnRuleSetChanged();
            ResearchList = RuleSetViewModel.Research.CreateDerivedCollection(e => e, null, (l, r) => l.Name.Value.CompareTo(r.Name.Value));
            ResearchList.ChangeTrackingEnabled = true;
        }
    }
}
