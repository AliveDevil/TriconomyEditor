using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RuleSet.Effects;

namespace RuleSetEditor.ViewModels.EffectViewModels
{
    public class DeliverEffectViewModel : EffectViewModel
    {
        public DeliverEffect DeliverEffect => (DeliverEffect)Effect;

        public override string ToString()
        {
            return "Deliver Effect";
        }
    }
}
