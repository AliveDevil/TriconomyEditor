﻿using System;
using System.Collections.Generic;

namespace RuleSetEditor.ViewModels
{
    public abstract class RuleSetViewModelBase : ViewModelBase
    {
        private bool deferChanged;
        private RuleSetViewModel ruleSetViewModel;

        public bool DeferChanged
        {
            get
            {
                return deferChanged;
            }
            set
            {
                if (!RaiseSetIfChanged(ref deferChanged, value)) return;
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
                if (!RaiseSetIfChanged(ref ruleSetViewModel, value)) return;
                if (!DeferChanged)
                    OnRuleSetChanged();
                else
                    DeferQueue.Enqueue(OnRuleSetChanged);
            }
        }

        protected Queue<Action> DeferQueue { get; } = new Queue<Action>();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DeferQueue.Clear();
            }
            base.Dispose(disposing);
        }

        protected virtual void OnRuleSetChanged()
        {
        }
    }
}