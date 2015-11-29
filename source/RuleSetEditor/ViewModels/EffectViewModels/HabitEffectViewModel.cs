using RuleSet.Effects;

namespace RuleSetEditor.ViewModels.EffectViewModels
{
    public class HabitEffectViewModel : EffectViewModel
    {
        public HabitEffect HabitEffect => (HabitEffect)Effect;

        public override string ToString()
        {
            return "Habit";
        }
    }
}
