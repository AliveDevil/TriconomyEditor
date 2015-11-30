namespace RuleSetEditor.ViewModels.EffectViewModels.EffectEditViewModels
{
    public class AddRecipeEffectEditViewModel : RuleSetViewModelBase
    {
        private AddRecipeEffectViewModel addRecipeEffect;

        public AddRecipeEffectViewModel AddRecipeEffect
        {
            get { return addRecipeEffect; }
            set { if (!RaiseSetIfChanged(ref addRecipeEffect, value)) return; }
        }
    }
}
