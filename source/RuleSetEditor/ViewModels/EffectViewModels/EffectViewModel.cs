using RuleSet;
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

        public static RuleSetViewModelBase FindEditViewModel(EffectViewModel effectViewModel, RuleSetViewModel ruleSetViewModel)
        {
            RuleSetViewModelBase viewModelBase = null;

            if (effectViewModel is AddRecipeEffectViewModel)
                viewModelBase = new AddRecipeEffectEditViewModel()
                {
                    RuleSetViewModel = ruleSetViewModel,
                    AddRecipeEffect = (AddRecipeEffectViewModel)effectViewModel
                };
            else if (effectViewModel is AssemblyPointEffectViewModel)
                viewModelBase = new AssemblyPointEffectEditViewModel()
                {
                    RuleSetViewModel = ruleSetViewModel,
                    AssemblyPointEffect = (AssemblyPointEffectViewModel)effectViewModel
                };
            else if (effectViewModel is DeliverEffectViewModel)
                viewModelBase = new DeliverEffectEditViewModel()
                {
                    RuleSetViewModel = ruleSetViewModel,
                    DeliverEffect = (DeliverEffectViewModel)effectViewModel
                };
            else if (effectViewModel is ExtendSettlerAmountEffectViewModel)
                viewModelBase = new ExtendSettlerAmountEffectEditViewModel()
                {
                    RuleSetViewModel = ruleSetViewModel,
                    ExtendSettlerAmountEffect = (ExtendSettlerAmountEffectViewModel)effectViewModel
                };
            else if (effectViewModel is ExtendStorageEffectViewModel)
                viewModelBase = new ExtendStorageEffectEditViewModel()
                {
                    RuleSetViewModel = ruleSetViewModel,
                    ExtendStorageEffect = (ExtendStorageEffectViewModel)effectViewModel
                };
            else if (effectViewModel is GatherResourceEffectViewModel)
                viewModelBase = new GatherResourceEffectEditViewModel()
                {
                    RuleSetViewModel = ruleSetViewModel,
                    GatherResourceEffect = (GatherResourceEffectViewModel)effectViewModel
                };
            else if (effectViewModel is HabitEffectViewModel)
                viewModelBase = new HabitEffectEditViewModel()
                {
                    RuleSetViewModel = ruleSetViewModel,
                    HabitEffect = (HabitEffectViewModel)effectViewModel
                };
            else if (effectViewModel is ProduceResourceEffectViewModel)
                viewModelBase = new ProduceResourceEffectEditViewModel()
                {
                    RuleSetViewModel = ruleSetViewModel,
                    ProduceResourceEffect = (ProduceResourceEffectViewModel)effectViewModel
                };
            else if (effectViewModel is SpawnWorldResourceEffectViewModel)
                viewModelBase = new SpawnWorldResourceEffectEditViewModel()
                {
                    RuleSetViewModel = ruleSetViewModel,
                    SpawnWorldResourceEffect = (SpawnWorldResourceEffectViewModel)effectViewModel
                };
            else if (effectViewModel is StorageEffectViewModel)
                viewModelBase = new StorageEffectEditViewModel()
                {
                    RuleSetViewModel = ruleSetViewModel,
                    StorageEffect = (StorageEffectViewModel)effectViewModel
                };
            else if (effectViewModel is UseResourceEffectViewModel)
                viewModelBase = new UseResourceEffectEditViewModel()
                {
                    RuleSetViewModel = ruleSetViewModel,
                    UseResourceEffect = (UseResourceEffectViewModel)effectViewModel
                };
            else if (effectViewModel is WorkplaceEffectViewModel)
                viewModelBase = new WorkplaceEffectEditViewModel()
                {
                    RuleSetViewModel = ruleSetViewModel,
                    WorkplaceEffect = (WorkplaceEffectViewModel)effectViewModel
                };

            return viewModelBase;
        }

        public static EffectViewModel FindViewModel(Effect effect, RuleSetViewModel ruleSetViewModel)
        {
            EffectViewModel model = null;

            if (effect is AddRecipeEffect) model = new AddRecipeEffectViewModel();
            else if (effect is AssemblyPointEffect) model = new AssemblyPointEffectViewModel();
            else if (effect is DeliverEffect) model = new DeliverEffectViewModel();
            else if (effect is ExtendSettlerAmountEffect) model = new ExtendSettlerAmountEffectViewModel();
            else if (effect is ExtendStorageEffect) model = new ExtendStorageEffectViewModel();
            else if (effect is GatherResourceEffect) model = new GatherResourceEffectViewModel();
            else if (effect is HabitEffect) model = new HabitEffectViewModel();
            else if (effect is ProduceResourceEffect) model = new ProduceResourceEffectViewModel();
            else if (effect is SpawnWorldResourceEffect) model = new SpawnWorldResourceEffectViewModel();
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

        //public abstract override string ToString();

        protected virtual void OnEffectChanged()
        {
        }
    }

    public abstract class EffectViewModel<TEffect> : EffectViewModel
        where TEffect : Effect
    {
        public new TEffect Effect
        {
            get { return (TEffect)base.Effect; }
            set { base.Effect = value; }
        }
    }
}
