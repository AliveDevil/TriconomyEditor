namespace RuleSetEditor.ViewModels.EffectViewModels.EffectEditViewModels
{
    public class ResearchEffectEditViewModel : RuleSetViewModelBase
    {
        private ResearchEffectViewModel researchEffect;

        public ResearchEffectViewModel ResearchEffect
        {
            get
            {
                return researchEffect;
            }
            set
            {
                if (!RaiseSetIfChanged(ref researchEffect, value))
                    return;
            }
        }
    }
}
