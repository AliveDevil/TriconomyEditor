using RuleSet.Effects;

namespace RuleSetEditor.ViewModels.EffectViewModels
{
    public class AddRecipeEffectViewModel : EffectViewModel
    {
        public AddRecipeEffect AddRecipeEffect => (AddRecipeEffect)Effect;

        public override string ToString()
        {
            return "Add Recipe";
        }
    }
}
