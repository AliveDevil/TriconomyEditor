using RuleSet;
using RuleSet.Conditions;
using RuleSetEditor.ViewModels.RuleSetViewModels.ConditionViewModels.EditViewModels;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ConditionViewModels
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
                if (!DeferChanged)
                    OnConditionChanged();
                else
                    DeferQueue.Enqueue(OnConditionChanged);
            }
        }

        public static RuleSetViewModelBase FindEditViewModel(ConditionViewModel effectViewModel, RuleSetViewModel ruleSetViewModel)
        {
            ConditionEditViewModel viewModelBase = null;

            if (effectViewModel is ElementNearByConditionViewModel)
                viewModelBase = new ElementNearByConditionEditViewModel();
            else if (effectViewModel is ExistingBuildingConditionViewModel)
                viewModelBase = new ExistingBuildingConditionEditViewModel();
            else if (effectViewModel is ResearchConditionViewModel)
                viewModelBase = new ResearchConditionEditViewModel();

            if (viewModelBase != null)
            {
                viewModelBase.RuleSetViewModel = ruleSetViewModel;
                viewModelBase.Condition = effectViewModel;
            }

            return viewModelBase;
        }

        public static ConditionViewModel FindViewModel(Condition condition, RuleSetViewModel ruleSetViewModel)
        {
            ConditionViewModel model = null;

            if (condition is ElementNearByCondition)
                model = new ElementNearByConditionViewModel();
            else if (condition is ExistingBuildingCondition)
                model = new ExistingBuildingConditionViewModel();
            else if (condition is ResearchCondition)
                model = new ResearchConditionViewModel();

            if (model != null)
            {
                model.RuleSetViewModel = ruleSetViewModel;
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
