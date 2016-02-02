namespace RuleSetEditor.ViewModels.RuleSetViewModels.ConditionViewModels.EditViewModels
{
    public class ConditionEditViewModel : RuleSetViewModelBase
    {
        private ConditionViewModel condition;

        public ConditionViewModel Condition
        {
            get
            {
                return condition;
            }
            set
            {
                if (!RaiseSetIfChanged(ref condition, value))
                    return;
                ConditionChanged();
            }
        }

        protected void ConditionChanged()
        {
            OnConditionChanged();
        }

        protected virtual void OnConditionChanged()
        {
        }
    }

    public class ConditionEditViewModel<T> : ConditionEditViewModel where T : ConditionViewModel
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
