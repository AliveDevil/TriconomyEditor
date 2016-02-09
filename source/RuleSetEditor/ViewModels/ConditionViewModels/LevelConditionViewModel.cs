using Reactive.Bindings;
using RuleSet.Conditions;

namespace RuleSetEditor.ViewModels.ConditionViewModels
{
    public class LevelConditionViewModel : ConditionViewModel<LevelCondition>
    {
        private ReactiveProperty<int> levelProperty;

        public ReactiveProperty<int> Level
        {
            get
            {
                return levelProperty;
            }
            private set
            {
                RaiseSetIfChanged(ref levelProperty, value);
            }
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Level = ReactiveProperty.FromObject(Condition, c => c.Level);
        }
    }
}
