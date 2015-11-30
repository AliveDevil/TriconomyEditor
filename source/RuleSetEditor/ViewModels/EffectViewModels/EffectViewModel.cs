﻿using RuleSet;
using RuleSet.Effects;
using RuleSetEditor.ViewModels.EffectViewModels.EffectEditViewModels;

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

        public static EffectViewModel FindViewModel(Effect effect, RuleSetViewModel ruleSetViewModel)
        {
            EffectViewModel model = null;

            if (effect is AddRecipeEffect) model = new AddRecipeEffectViewModel();
            else if (effect is ExtendSettlerAmountEffect) model = new ExtendSettlerAmountEffectViewModel();
            else if (effect is ExtendStorageEffect) model = new ExtendStorageEffectViewModel();
            else if (effect is GatherResourceEffect) model = new GatherResourceEffectViewModel();
            else if (effect is HabitEffect) model = new HabitEffectViewModel();
            else if (effect is StorageEffect) model = new StorageEffectViewModel();
            else if (effect is UseResourceEffect) model = new UseResourceEffectViewModel();
            else if (effect is WorkplaceEffect) model = new WorkplaceEffectViewModel();

            if (model != null)
            {
                model.RuleSetViewModel = ruleSetViewModel;
                model.Effect = effect;
            }

            return model;
        }

        public static RuleSetViewModelBase FindEditViewModel(EffectViewModel effectViewModel, RuleSetViewModel ruleSetViewModel)
        {
            RuleSetViewModelBase viewModelBase = null;

            if (effectViewModel is AddRecipeEffectViewModel) viewModelBase = new AddRecipeEffectEditViewModel() { RuleSetViewModel = ruleSetViewModel, AddRecipeEffect = (AddRecipeEffectViewModel)effectViewModel };
            else if (effectViewModel is ExtendSettlerAmountEffectViewModel) viewModelBase = new ExtendSettlerAmountEffectEditViewModel() { RuleSetViewModel = ruleSetViewModel, ExtendSettlerAmountEffect = (ExtendSettlerAmountEffectViewModel)effectViewModel };

            if (viewModelBase != null)
                viewModelBase.RuleSetViewModel = ruleSetViewModel;

            return viewModelBase;
        }
    }
}
