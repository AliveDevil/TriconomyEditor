using System;
using ReactiveUI;
using RuleSet;
using RuleSet.EventActions;

namespace RuleSetEditor.ViewModels.EventViewModels.EventActionViewModels
{
    public class TriggerRandomSoundEventActionViewModel : EventActionViewModel<TriggerRandomSoundEventAction>
    {
        private RelayCommand addReferenceCommand;
        private RelayCommand removeReferenceCommand;
        private NamedReferenceViewModel selectedReference;
        private IDisposable soundAdded;
        private IDisposable soundListConstructor;
        private IDisposable soundListInitializer;
        private IDisposable soundListPostDisposer;
        private IDisposable soundListPostInitializer;
        private IDisposable soundRemoved;
        private ReactiveList<NamedReferenceViewModel> sounds;

        public RelayCommand AddReferenceCommand
        {
            get
            {
                return addReferenceCommand ?? (addReferenceCommand = new RelayCommand(() =>
                {
                    Sounds.Add(RuleSetViewModel.Create<NamedReferenceViewModel>(vm =>
                    {
                        vm.NamedReference = new NamedReference();
                    }));
                }));
            }
        }

        public RelayCommand RemoveReferenceCommand
        {
            get
            {
                return removeReferenceCommand ?? (removeReferenceCommand = new RelayCommand(() =>
                {
                    Sounds.Remove(SelectedReference);
                }));
            }
        }

        public NamedReferenceViewModel SelectedReference
        {
            get
            {
                return selectedReference;
            }
            set
            {
                RaiseSetIfChanged(ref selectedReference, value);
            }
        }

        public ReactiveList<NamedReferenceViewModel> Sounds
        {
            get
            {
                return sounds;
            }
            private set
            {
                RaiseSetIfChanged(ref sounds, value);
            }
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            Sounds = new ReactiveList<NamedReferenceViewModel>() { ChangeTrackingEnabled = true };
            soundListInitializer = Sounds.BeforeItemsAdded.Subscribe(u => u?.Initialize());
            soundListPostInitializer = Sounds.ItemsAdded.Subscribe(u => u?.PostInitialize());
            soundListPostDisposer = Sounds.BeforeItemsRemoved.Subscribe(u => u?.Dispose());
        }

        protected override void OnPostInitialize()
        {
            base.OnPostInitialize();

            foreach (var item in EventAction.Sources)
            {
                Sounds.Add(RuleSetViewModel.Create<NamedReferenceViewModel>(v =>
                {
                    v.NamedReference = item;
                }));
            }

            soundListConstructor = Sounds.BeforeItemsAdded.Subscribe(u => u?.Construct());
            soundAdded = Sounds.BeforeItemsAdded.Subscribe(u => EventAction.Sources.Add(u?.NamedReference));
            soundRemoved = Sounds.ItemsRemoved.Subscribe(u => EventAction.Sources.Remove(u?.NamedReference));
        }
    }
}
