using System;
using ReactiveUI;
using RuleSet.EventActions;

namespace RuleSetEditor.ViewModels.EventViewModels.EventActionViewModels
{
    public class TriggerRandomAnimationEventActionViewModel : EventActionViewModel<TriggerRandomAnimationEventAction>
    {
        private IDisposable animationAdded;
        private IDisposable animationListConstructor;
        private IDisposable animationListInitializer;
        private IDisposable animationListPostDisposer;
        private IDisposable animationListPostInitializer;
        private IDisposable animationRemoved;
        private ReactiveList<NamedReferenceViewModel> animations;

        public ReactiveList<NamedReferenceViewModel> Animations
        {
            get
            {
                return animations;
            }
            private set
            {
                RaiseSetIfChanged(ref animations, value);
            }
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            Animations = new ReactiveList<NamedReferenceViewModel>() { ChangeTrackingEnabled = true };
            animationListInitializer = Animations.BeforeItemsAdded.Subscribe(u => u.Initialize());
            animationListPostInitializer = Animations.ItemsAdded.Subscribe(u => u.PostInitialize());
            animationListPostDisposer = Animations.BeforeItemsRemoved.Subscribe(u => u.Dispose());
        }

        protected override void OnPostInitialize()
        {
            base.OnPostInitialize();

            foreach (var item in EventAction.Sources)
            {
                Animations.Add(RuleSetViewModel.Create<NamedReferenceViewModel>(v =>
                {
                    v.NamedReference = item;
                }));
            }

            animationListConstructor = Animations.BeforeItemsAdded.Subscribe(u => u.Construct());
            animationAdded = Animations.BeforeItemsAdded.Subscribe(u => EventAction.Sources.Add(u.NamedReference));
            animationRemoved = Animations.ItemsRemoved.Subscribe(u => EventAction.Sources.Remove(u.NamedReference));
        }
    }
}
