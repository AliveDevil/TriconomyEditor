using RuleSet.EventActions;

namespace RuleSetEditor.ViewModels.EventViewModels.EventActionViewModels
{
    public class PlaySoundEventActionViewModel : EventActionViewModel<PlaySoundEventAction>
    {
        private NamedReferenceViewModel source;

        public NamedReferenceViewModel Source
        {
            get
            {
                return source;
            }
            private set
            {
                RaiseSetIfChanged(ref source, value);
            }
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            Source = new NamedReferenceViewModel() { RuleSetViewModel = RuleSetViewModel, ViewStack = ViewStack, NamedReference = EventAction.Source ?? (EventAction.Source = new RuleSet.NamedReference()) };
        }
    }
}
