using System;
using Reactive.Bindings;
using RuleSet.Conditions;

namespace RuleSetEditor.ViewModels.ConditionViewModels
{
    public class DateTimeConditionViewModel : ConditionViewModel<DateTimeCondition>
    {
        private ReactiveProperty<DateTime> dateTimeProperty;

        public ReactiveProperty<DateTime> DateTimeProperty
        {
            get
            {
                return dateTimeProperty;
            }
            private set
            {
                RaiseSetIfChanged(ref dateTimeProperty, value);
            }
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            DateTimeProperty = ReactiveProperty.FromObject(Condition, c => c.DateTime);
        }
    }
}
