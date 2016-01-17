namespace RuleSetEditor.ViewModels.EffectViewModels.EffectEditViewModels
{
    public class AssemblyPointEffectEditViewModel : RuleSetViewModelBase
    {
        private AssemblyPointEffectViewModel assemblyPointEffect;

        public AssemblyPointEffectViewModel AssemblyPointEffect
        {
            get
            {
                return assemblyPointEffect;
            }
            set
            {
                if (!RaiseSetIfChanged(ref assemblyPointEffect, value)) return;
            }
        }
    }
}
