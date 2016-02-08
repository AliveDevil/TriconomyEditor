namespace RuleSetEditor.ViewModels
{
    public abstract class RuleSetViewModelBase : ViewModelBase
    {
        private RuleSetViewModel ruleSetViewModel;

        public RuleSetViewModel RuleSetViewModel
        {
            get
            {
                return ruleSetViewModel;
            }
            set
            {
                if (!RaiseSetIfChanged(ref ruleSetViewModel, value))
                    return;
                OnRuleSetChanged();
            }
        }

        public void Construct()
        {
            OnConstruct();
        }

        public void Initialize()
        {
            OnInitialize();
        }

        public void PostInitialize()
        {
            OnPostInitialize();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected virtual void OnConstruct()
        {
        }

        protected virtual void OnInitialize()
        {
        }

        protected virtual void OnPostInitialize()
        {
        }

        protected virtual void OnRuleSetChanged()
        {
        }
    }
}
