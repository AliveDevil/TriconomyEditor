using System;
using System.Collections.Generic;
using ReactiveUI;
using RuleSet;
using RuleSetEditor.ViewModels.ConditionViewModels;

namespace RuleSetEditor.ViewModels.EventViewModels
{
    public class EventViewModel : RuleSetViewModelBase
    {
        private Event @event;
        private RelayCommand<Type> addEventActionCommand;
        private RelayCommand<Type> addResetCommand;
        private RelayCommand<Type> addSetCommand;
        private RelayCommand<ConditionViewModel> editCommand;
        private RelayCommand<EventActionViewModel> editEventActionCommand;
        private IDisposable eventActionAdded;
        private IDisposable eventActionListConstructor;
        private IDisposable eventActionListInitializer;
        private IDisposable eventActionListPostDisposer;
        private IDisposable eventActionListPostInitializer;
        private IDisposable eventActionRemoved;
        private ReactiveList<EventActionViewModel> eventActions;
        private RelayCommand removeEventActionCommand;
        private RelayCommand removeResetCommand;
        private RelayCommand removeSetCommand;
        private ReactiveList<ConditionViewModel> reset;
        private IDisposable resetAdded;
        private IDisposable resetListConstructor;
        private IDisposable resetListInitializer;
        private IDisposable resetListPostDisposer;
        private IDisposable resetListPostInitializer;
        private IDisposable resetRemoved;
        private EventActionViewModel selectedEventAction;
        private ConditionViewModel selectedReset;
        private ConditionViewModel selectedSet;
        private ReactiveList<ConditionViewModel> set;
        private IDisposable setAdded;
        private IDisposable setListConstructor;
        private IDisposable setListInitializer;
        private IDisposable setListPostDisposer;
        private IDisposable setListPostInitializer;
        private IDisposable setRemoved;

        public RelayCommand<Type> AddEventActionCommand
        {
            get
            {
                return addEventActionCommand ?? (addEventActionCommand = new RelayCommand<Type>(t =>
                {
                    AddItem(EventActions, EventActionViewModel.Resolve(New<EventAction>(t), RuleSetViewModel));
                }));
            }
        }

        public RelayCommand<Type> AddResetCommand
        {
            get
            {
                return addResetCommand ?? (addResetCommand = new RelayCommand<Type>(t =>
                {
                    AddItem(Reset, ConditionViewModel.FindViewModel(New<Condition>(t), RuleSetViewModel));
                }));
            }
        }

        public RelayCommand<Type> AddSetCommand
        {
            get
            {
                return addSetCommand ?? (addSetCommand = new RelayCommand<Type>(t =>
                {
                    AddItem(Set, ConditionViewModel.FindViewModel(New<Condition>(t), RuleSetViewModel));
                }));
            }
        }

        public RelayCommand<ConditionViewModel> EditCommand
        {
            get
            {
                return editCommand ?? (editCommand = new RelayCommand<ConditionViewModel>(c =>
                {
                    ViewStack.Push(c);
                }));
            }
        }

        public RelayCommand<EventActionViewModel> EditEventActionCommand
        {
            get
            {
                return editEventActionCommand ?? (editEventActionCommand = new RelayCommand<EventActionViewModel>(viewModel =>
                {
                    ViewStack.Push(viewModel);
                }));
            }
        }

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

        public ReactiveList<EventActionViewModel> EventActions
        {
            get
            {
                return eventActions;
            }
            private set
            {
                RaiseSetIfChanged(ref eventActions, value);
            }
        }

        public RelayCommand RemoveEventActionCommand
        {
            get
            {
                return removeEventActionCommand ?? (removeEventActionCommand = new RelayCommand(() =>
                {
                    EventActions.Remove(SelectedEventAction);
                }));
            }
        }

        public RelayCommand RemoveResetCommand
        {
            get
            {
                return removeResetCommand ?? (removeResetCommand = new RelayCommand(() =>
                {
                    Reset.Remove(SelectedReset);
                }));
            }
        }

        public RelayCommand RemoveSetCommand
        {
            get
            {
                return removeSetCommand ?? (removeSetCommand = new RelayCommand(() =>
                {
                    Set.Remove(SelectedSet);
                }));
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

        public EventActionViewModel SelectedEventAction
        {
            get
            {
                return selectedEventAction;
            }
            set
            {
                RaiseSetIfChanged(ref selectedEventAction, value);
            }
        }

        public ConditionViewModel SelectedReset
        {
            get
            {
                return selectedReset;
            }
            set
            {
                RaiseSetIfChanged(ref selectedReset, value);
            }
        }

        public ConditionViewModel SelectedSet
        {
            get
            {
                return selectedSet;
            }
            set
            {
                RaiseSetIfChanged(ref selectedSet, value);
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
            EventActions = new ReactiveList<EventActionViewModel>() { ChangeTrackingEnabled = true };

            setListInitializer = Set.BeforeItemsAdded.Subscribe(u => u.Initialize());
            setListPostInitializer = Set.ItemsAdded.Subscribe(u => u.PostInitialize());
            setListPostDisposer = Set.BeforeItemsRemoved.Subscribe(u => u.Dispose());
            resetListInitializer = Reset.BeforeItemsAdded.Subscribe(u => u.Initialize());
            resetListPostInitializer = Reset.ItemsAdded.Subscribe(u => u.PostInitialize());
            resetListPostDisposer = Reset.BeforeItemsRemoved.Subscribe(u => u.Dispose());
            eventActionListInitializer = EventActions.BeforeItemsAdded.Subscribe(u => u.Initialize());
            eventActionListPostInitializer = EventActions.ItemsAdded.Subscribe(u => u.PostInitialize());
            eventActionListPostDisposer = EventActions.BeforeItemsRemoved.Subscribe(u => u.Dispose());
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
            foreach (var item in Event.Actions)
            {
                EventActions.Add(EventActionViewModel.Resolve(item, RuleSetViewModel));
            }

            setListConstructor = Set.BeforeItemsAdded.Subscribe(u => u.Construct());
            setAdded = Set.BeforeItemsAdded.Subscribe(u => Event.Set.Add(u.Condition));
            setRemoved = Set.ItemsRemoved.Subscribe(u => Event.Set.Remove(u.Condition));
            resetListConstructor = Reset.BeforeItemsAdded.Subscribe(u => u.Construct());
            resetAdded = Reset.BeforeItemsAdded.Subscribe(u => Event.Set.Add(u.Condition));
            resetRemoved = Reset.ItemsRemoved.Subscribe(u => Event.Set.Remove(u.Condition));
            eventActionListConstructor = EventActions.BeforeItemsAdded.Subscribe(u => u.Construct());
            eventActionAdded = EventActions.BeforeItemsAdded.Subscribe(u => Event.Actions.Add(u.EventAction));
            eventActionRemoved = EventActions.ItemsRemoved.Subscribe(u => Event.Actions.Remove(u.EventAction));
        }

        private static T New<T>(Type type)
        {
            return (T)Activator.CreateInstance(type);
        }

        private void AddItem<T>(ICollection<T> collection, T element)
                    where T : ViewModelBase
        {
            collection.Add(ViewStack.Push(element));
        }
    }
}
