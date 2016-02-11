using RuleSet;
using RuleSet.EventActions;
using RuleSetEditor.ViewModels.EventViewModels.EventActionViewModels;

namespace RuleSetEditor.ViewModels.EventViewModels
{
    public class EventActionViewModel : RuleSetViewModelBase
    {
        private EventAction eventAction;

        public EventAction EventAction
        {
            get
            {
                return eventAction;
            }
            set
            {
                RaiseSetIfChanged(ref eventAction, value);
            }
        }

        public static EventActionViewModel Resolve(EventAction eventAction, RuleSetViewModel ruleSetViewModel)
        {
            var viewModel = default(EventActionViewModel);

            if (eventAction is PlayAnimationEventAction)
                viewModel = new PlayAnimationEventActionViewModel();
            else if (eventAction is PlaySoundEventAction)
                viewModel = new PlaySoundEventActionViewModel();
            else if (eventAction is TriggerRandomAnimationEventAction)
                viewModel = new TriggerRandomAnimationEventActionViewModel();
            else if (eventAction is TriggerRandomSoundEventAction)
                viewModel = new TriggerRandomSoundEventActionViewModel();

            if (viewModel != null)
            {
                viewModel.RuleSetViewModel = ruleSetViewModel;
                viewModel.ViewStack = ruleSetViewModel;
                viewModel.EventAction = eventAction;
            }

            return viewModel;
        }
    }

    public class EventActionViewModel<TEventAction> : EventActionViewModel
        where TEventAction : EventAction
    {
        public new TEventAction EventAction
        {
            get
            {
                return (TEventAction)base.EventAction;
            }
            set
            {
                base.EventAction = value;
            }
        }
    }
}
