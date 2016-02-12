using System.Collections.Generic;
using Reactive.Bindings;
using RuleSet;
using RuleSet.Conditions;

namespace RuleSetEditor.ViewModels.ConditionViewModels
{
    public class DayTimeConditionViewModel : ConditionViewModel<DayTimeCondition>
    {
        private ReactiveProperty<DayTime> dayTime;

        public static Dictionary<DayTime, string> Options
        {
            get;
        } = new Dictionary<RuleSet.DayTime, string>()
        {
            { RuleSet.DayTime.Rise, "Rise" },
            { RuleSet.DayTime.Day, "Day" },
            { RuleSet.DayTime.Set, "Set" },
            { RuleSet.DayTime.Night, "Night" },
        };

        public ReactiveProperty<DayTime> DayTime
        {
            get
            {
                return dayTime;
            }
            private set
            {
                RaiseSetIfChanged(ref dayTime, value);
            }
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            DayTime = ReactiveProperty.FromObject(Condition, c => c.DayTime);
        }
    }
}
