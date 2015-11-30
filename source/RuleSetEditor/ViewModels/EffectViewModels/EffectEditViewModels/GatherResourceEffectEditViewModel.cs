namespace RuleSetEditor.ViewModels.EffectViewModels.EffectEditViewModels
{
    public class GatherResourceEffectEditViewModel : RuleSetViewModelBase
    {
        private GatherResourceEffectViewModel gatherResourceEffect;

        public GatherResourceEffectViewModel GatherResourceEffect
        {
            get { return gatherResourceEffect; }
            set { if (!RaiseSetIfChanged(ref gatherResourceEffect, value)) return; }
        }

        protected override void OnRuleSetChanged()
        {
            base.OnRuleSetChanged();
        }
    }
}
