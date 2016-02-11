using Reactive.Bindings;
using RuleSet.EventActions;

namespace RuleSetEditor.ViewModels.EventViewModels.EventActionViewModels
{
    public class PlayAnimationEventActionViewModel : EventActionViewModel<PlayAnimationEventAction>
    {
        private NamedReferenceViewModel animation;

        public NamedReferenceViewModel Animation
        {
            get
            {
                return animation;
            }
            private set
            {
                RaiseSetIfChanged(ref animation, value);
            }
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            Animation = new NamedReferenceViewModel() { RuleSetViewModel = RuleSetViewModel, ViewStack = ViewStack, NamedReference = EventAction.Animation ?? (EventAction.Animation = new RuleSet.NamedReference()) };
        }
    }
}
