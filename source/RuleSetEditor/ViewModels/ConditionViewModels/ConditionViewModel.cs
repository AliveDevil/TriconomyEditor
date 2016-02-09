using RuleSet;
using RuleSet.Conditions;

namespace RuleSetEditor.ViewModels.ConditionViewModels
{
    public class ConditionViewModel : RuleSetViewModelBase
    {
        private Condition condition;

        public Condition Condition
        {
            get
            {
                return condition;
            }
            set
            {
                if (!RaiseSetIfChanged(ref condition, value))
                    return;
            }
        }

        public static ConditionViewModel FindViewModel(Condition condition, RuleSetViewModel ruleSetViewModel)
        {
            ConditionViewModel model = null;
            if (condition is DateTimeCondition)
                model = new DateTimeConditionViewModel();
            else if (condition is DayTimeCondition)
                model = new DayTimeConditionViewModel();
            if (condition is ElementNearByCondition)
                model = new ElementNearByConditionViewModel();
            else if (condition is ExistingBuildingCondition)
                model = new ExistingBuildingConditionViewModel();
            else if (condition is LevelCondition)
                model = new LevelConditionViewModel();
            else if (condition is ResearchCondition)
                model = new ResearchConditionViewModel();
            else if (condition is TraverseCondition)
                model = new TraverseConditionViewModel();
            else if (condition is TriggerCondition)
                model = new TriggerConditionViewModel();
            else if (condition is WorkingCondition)
                model = new WorkingConditionViewModel();

            if (model != null)
            {
                model.RuleSetViewModel = ruleSetViewModel;
                model.ViewStack = ruleSetViewModel;
                model.Condition = condition;
            }

            return model;
        }

        protected virtual void OnConditionChanged()
        {
        }
    }

    public class ConditionViewModel<T> : ConditionViewModel where T : Condition
    {
        public new T Condition
        {
            get
            {
                return (T)base.Condition;
            }
            set
            {
                base.Condition = value;
            }
        }
    }
}
