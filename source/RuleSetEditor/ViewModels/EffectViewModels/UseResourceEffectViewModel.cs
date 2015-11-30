using System.Linq;
using Reactive.Bindings;
using ReactiveUI;
using RuleSet.Effects;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels;

namespace RuleSetEditor.ViewModels.EffectViewModels
{
    public class UseResourceEffectViewModel : EffectViewModel
    {
        public UseResourceEffect UseResourceEffect => (UseResourceEffect)Effect;

        public override string ToString()
        {
            return "Use Resource";
        }
    }
}
