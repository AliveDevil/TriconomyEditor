﻿using RuleSet;

namespace RuleSetEditor.ViewModels.EffectViewModels
{
    public abstract class EffectViewModel : RuleSetViewModelBase
    {
        private Effect effect;

        public Effect Effect
        {
            get
            {
                return effect;
            }
            set
            {
                if (!RaiseSetIfChanged(ref effect, value)) return;
                if (DeferChanged)
                    DeferQueue.Enqueue(OnEffectChanged);
                else
                    OnEffectChanged();
            }
        }

        public abstract override string ToString();

        protected virtual void OnEffectChanged()
        {
        }
    }
}
