using RuleSet;
using RuleSet.Effects;

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
                if (!RaiseSetIfChanged(ref effect, value))
                    return;
            }
        }
        
        public static EffectViewModel FindViewModel(Effect effect, RuleSetViewModel ruleSetViewModel)
        {
            EffectViewModel model = null;

            if (effect is AddRecipeEffect)
                model = new AddRecipeEffectViewModel();
            else if (effect is AssemblyPointEffect)
                model = new AssemblyPointEffectViewModel();
            else if (effect is DeliverEffect)
                model = new DeliverEffectViewModel();
            else if (effect is ExtendSettlerAmountEffect)
                model = new ExtendSettlerAmountEffectViewModel();
            else if (effect is ExtendStorageEffect)
                model = new ExtendStorageEffectViewModel();
            else if (effect is GatherResourceEffect)
                model = new GatherResourceEffectViewModel();
            else if (effect is HabitEffect)
                model = new HabitEffectViewModel();
            else if (effect is ProduceResourceEffect)
                model = new ProduceResourceEffectViewModel();
            else if (effect is ResearchEffect)
                model = new ResearchEffectViewModel();
            else if (effect is SpawnLivingResourceEffect)
                model = new SpawnLivingResourceEffectViewModel();
            else if (effect is SpawnWorldResourceEffect)
                model = new SpawnWorldResourceEffectViewModel();
            else if (effect is StorageEffect)
                model = new StorageEffectViewModel();
            else if (effect is UseResourceEffect)
                model = new UseResourceEffectViewModel();
            else if (effect is WorkplaceEffect)
                model = new WorkplaceEffectViewModel();

            if (model != null)
            {
                model.RuleSetViewModel = ruleSetViewModel;
                model.ViewStack = ruleSetViewModel;
                model.Effect = effect;
            }

            return model;
        }
    }

    public abstract class EffectViewModel<TEffect> : EffectViewModel
        where TEffect : Effect
    {
        public new TEffect Effect
        {
            get
            {
                return (TEffect)base.Effect;
            }
            set
            {
                base.Effect = value;
            }
        }
    }
}
