namespace RuleSetEditor.ViewModels.EffectViewModels.EffectEditViewModels
{
    public class WorkplaceEffectEditViewModel : RuleSetViewModelBase
    {
        private WorkplaceEffectViewModel workplaceEffect;

        public WorkplaceEffectViewModel WorkplaceEffect
        {
            get { return workplaceEffect; }
            set { if (!RaiseSetIfChanged(ref workplaceEffect, value)) return; }
        }
    }
}
