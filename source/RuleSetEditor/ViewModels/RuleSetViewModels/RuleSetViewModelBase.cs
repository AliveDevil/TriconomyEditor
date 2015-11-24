using System;
using System.Collections.Generic;

namespace RuleSetEditor.ViewModels.RuleSetViewModels
{
    public abstract class RuleSetViewModelBase : ViewModelBase
    {
        private bool deferChanged;
        private RuleSetViewModel ruleSetViewModel;

        public bool DeferChanged
        {
            get { return deferChanged; }
            set
            {
                RaiseSetIfChanged(ref deferChanged, value);
                if (!DeferChanged)
                    while (DeferQueue.Count > 0)
                        DeferQueue.Dequeue()();
            }
        }

        public RuleSetViewModel RuleSetViewModel
        {
            get
            {
                return ruleSetViewModel;
            }
            set
            {
                RaiseSetIfChanged(ref ruleSetViewModel, value);
                if (!DeferChanged)
                    OnRuleSetChanged();
                else
                    DeferQueue.Enqueue(OnRuleSetChanged);
            }
        }

        protected Queue<Action> DeferQueue { get; } = new Queue<Action>();

        protected virtual void OnRuleSetChanged()
        {
        }
    }
}
