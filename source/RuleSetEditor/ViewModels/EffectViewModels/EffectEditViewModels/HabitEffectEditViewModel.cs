namespace RuleSetEditor.ViewModels.EffectViewModels.EffectEditViewModels
{
    public class HabitEffectEditViewModel : RuleSetViewModelBase
    {
        private HabitEffectViewModel habitEffect;

        public HabitEffectViewModel HabitEffect
        {
            get { return habitEffect; }
            set { if (!RaiseSetIfChanged(ref habitEffect, value)) return; }
        }
    }
}
