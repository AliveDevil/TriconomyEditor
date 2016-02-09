using System;
using ReactiveUI;
using RuleSet;
using RuleSetEditor.ViewModels.ConditionViewModels;

namespace RuleSetEditor.ViewModels.EventViewModels
{
    public class EventViewModel : RuleSetViewModelBase
    {
        private Event @event;
        private ReactiveList<ConditionViewModel> reset;
        private IDisposable resetAdded;
        private IDisposable resetListConstructor;
        private IDisposable resetListInitializer;
        private IDisposable resetListPostDisposer;
        private IDisposable resetListPostInitializer;
        private IDisposable resetRemoved;
        private ReactiveList<ConditionViewModel> set;
        private IDisposable setAdded;
        private IDisposable setListConstructor;
        private IDisposable setListInitializer;
        private IDisposable setListPostDisposer;
        private IDisposable setListPostInitializer;
        private IDisposable setRemoved;

        public Event Event
        {
            get
            {
                return @event;
            }
            set
            {
                RaiseSetIfChanged(ref @event, value);
            }
        }

        public ReactiveList<ConditionViewModel> Reset
        {
            get
            {
                return reset;
            }
            private set
            {
                RaiseSetIfChanged(ref reset, value);
            }
        }

        public ReactiveList<ConditionViewModel> Set
        {
            get
            {
                return set;
            }
            private set
            {
                RaiseSetIfChanged(ref set, value);
            }
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Set = new ReactiveList<ConditionViewModel>() { ChangeTrackingEnabled = true };
            Reset = new ReactiveList<ConditionViewModel>() { ChangeTrackingEnabled = true };

            setListInitializer = Set.BeforeItemsAdded.Subscribe(u => u.Initialize());
            setListPostInitializer = Set.ItemsAdded.Subscribe(u => u.PostInitialize());
            setListPostDisposer = Set.BeforeItemsRemoved.Subscribe(u => u.Dispose());
            resetListInitializer = Reset.BeforeItemsAdded.Subscribe(u => u.Initialize());
            resetListPostInitializer = Reset.ItemsAdded.Subscribe(u => u.PostInitialize());
            resetListPostDisposer = Reset.BeforeItemsRemoved.Subscribe(u => u.Dispose());
        }

        protected override void OnPostInitialize()
        {
            base.OnPostInitialize();

            foreach (var item in Event.Set)
            {
                Set.Add(ConditionViewModel.FindViewModel(item, RuleSetViewModel));
            }
            foreach (var item in Event.Reset)
            {
                Reset.Add(ConditionViewModel.FindViewModel(item, RuleSetViewModel));
            }

            setListConstructor = Set.BeforeItemsAdded.Subscribe(u => u.Construct());
            setAdded = Set.BeforeItemsAdded.Subscribe(u => Event.Set.Add(u.Condition));
            setRemoved = Set.ItemsRemoved.Subscribe(u => Event.Set.Remove(u.Condition));
            resetListConstructor = Reset.BeforeItemsAdded.Subscribe(u => u.Construct());
            resetAdded = Reset.BeforeItemsAdded.Subscribe(u => Event.Set.Add(u.Condition));
            resetRemoved = Reset.ItemsRemoved.Subscribe(u => Event.Set.Remove(u.Condition));
        }
    }
}
