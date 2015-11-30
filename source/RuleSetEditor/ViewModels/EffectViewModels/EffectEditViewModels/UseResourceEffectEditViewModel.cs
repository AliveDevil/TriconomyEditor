namespace RuleSetEditor.ViewModels.EffectViewModels.EffectEditViewModels
{
    public class UseResourceEffectEditViewModel : RuleSetViewModelBase
    {
        private UseResourceEffectViewModel useResourceEffect;

        public UseResourceEffectViewModel UseResourceEffect
        {
            get { return useResourceEffect; }
            set { if (!RaiseSetIfChanged(ref useResourceEffect, value)) return; }
        }
    }
}
