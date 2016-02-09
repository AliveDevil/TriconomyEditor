using System;
using System.Linq;
using ReactiveUI;
using RuleSet.Effects;
using RuleSet.Elements;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels;

namespace RuleSetEditor.ViewModels.EffectViewModels
{
    public class AddRecipeEffectViewModel : EffectViewModel<AddRecipeEffect>
    {
        private RelayCommand addInResourcePartCommand;
        private RelayCommand addOutResourcePartCommand;
        private RelayCommand<ResourcePartViewModel> editResourcePartCommand;
        private ReactiveList<ResourcePartViewModel> inParts;
        private IDisposable inPartsAdded;
        private IDisposable inPartsListConstructor;
        private IDisposable inPartsListInitializer;
        private IDisposable inPartsListPostDisposer;
        private IDisposable inPartsListPostInitializer;
        private IDisposable inPartsRemoved;
        private ReactiveList<ResourcePartViewModel> outParts;
        private IDisposable outPartsAdded;
        private IDisposable outPartsListConstructor;
        private IDisposable outPartsListInitializer;
        private IDisposable outPartsListPostDisposer;
        private IDisposable outPartsListPostInitializer;
        private IDisposable outPartsRemoved;
        private RelayCommand removeInResourcePartCommand;
        private RelayCommand removeOutResourcePartCommand;
        private ResourcePartViewModel selectedInPart;
        private ResourcePartViewModel selectedOutPart;

        public RelayCommand AddInResourcePartCommand
        {
            get
            {
                return addInResourcePartCommand ?? (addInResourcePartCommand = new RelayCommand(() =>
                {
                    InParts.Add(ViewStack.Push(RuleSetViewModel.Create<ResourcePartViewModel>(v =>
                    {
                        v.ResourcePart = new ResourcePart();
                    })));
                }));
            }
        }

        public RelayCommand AddOutResourcePartCommand
        {
            get
            {
                return addOutResourcePartCommand ?? (addOutResourcePartCommand = new RelayCommand(() =>
                {
                    OutParts.Add(ViewStack.Push(RuleSetViewModel.Create<ResourcePartViewModel>(v =>
                    {
                        v.ResourcePart = new ResourcePart();
                    })));
                }));
            }
        }

        public RelayCommand<ResourcePartViewModel> EditResourcePartCommand
        {
            get
            {
                return editResourcePartCommand ?? (editResourcePartCommand = new RelayCommand<ResourcePartViewModel>(part =>
                {
                    ViewStack.Push(part);
                }));
            }
        }

        public ReactiveList<ResourcePartViewModel> InParts
        {
            get
            {
                return inParts;
            }
            private set
            {
                RaiseSetIfChanged(ref inParts, value);
            }
        }

        public ReactiveList<ResourcePartViewModel> OutParts
        {
            get
            {
                return outParts;
            }
            private set
            {
                RaiseSetIfChanged(ref outParts, value);
            }
        }

        public RelayCommand RemoveInResourcePartCommand
        {
            get
            {
                return removeInResourcePartCommand ?? (removeInResourcePartCommand = new RelayCommand(() =>
                {
                    InParts.Remove(SelectedInPart);
                }));
            }
        }

        public RelayCommand RemoveOutResourcePartCommand
        {
            get
            {
                return removeOutResourcePartCommand ?? (removeOutResourcePartCommand = new RelayCommand(() =>
                {
                    OutParts.Remove(SelectedOutPart);
                }));
            }
        }

        public ResourcePartViewModel SelectedInPart
        {
            get
            {
                return selectedInPart;
            }
            set
            {
                RaiseSetIfChanged(ref selectedInPart, value);
            }
        }

        public ResourcePartViewModel SelectedOutPart
        {
            get
            {
                return selectedOutPart;
            }
            set
            {
                RaiseSetIfChanged(ref selectedOutPart, value);
            }
        }
        
        protected override void OnInitialize()
        {
            base.OnInitialize();

            InParts = new ReactiveList<ResourcePartViewModel>() { ChangeTrackingEnabled = true };
            inPartsListInitializer = InParts.BeforeItemsAdded.Subscribe(c => c.Initialize());
            inPartsListPostInitializer = InParts.ItemsAdded.Subscribe(c => c.PostInitialize());
            inPartsListPostDisposer = InParts.BeforeItemsRemoved.Subscribe(c => c.Dispose());

            OutParts = new ReactiveList<ResourcePartViewModel>() { ChangeTrackingEnabled = true };
            outPartsListInitializer = OutParts.BeforeItemsAdded.Subscribe(c => c.Initialize());
            outPartsListPostInitializer = OutParts.ItemsAdded.Subscribe(c => c.PostInitialize());
            outPartsListPostDisposer = OutParts.BeforeItemsRemoved.Subscribe(c => c.Dispose());
        }

        protected override void OnPostInitialize()
        {
            base.OnPostInitialize();

            foreach (var item in Effect.InResources)
            {
                InParts.Add(RuleSetViewModel.Create<ResourcePartViewModel>(v =>
                {
                    v.ResourcePart = item;
                }));
            }
            inPartsListConstructor = InParts.BeforeItemsAdded.Subscribe(c => c.Construct());
            inPartsAdded = InParts.BeforeItemsAdded.Subscribe(c => Effect.InResources.Add(c.ResourcePart));
            inPartsRemoved = InParts.ItemsRemoved.Subscribe(c => Effect.InResources.Remove(c.ResourcePart));

            foreach (var item in Effect.OutResources)
            {
                OutParts.Add(RuleSetViewModel.Create<ResourcePartViewModel>(v =>
                {
                    v.ResourcePart = item;
                }));
            }
            outPartsListConstructor = OutParts.BeforeItemsAdded.Subscribe(c => c.Construct());
            outPartsAdded = OutParts.BeforeItemsAdded.Subscribe(c => Effect.OutResources.Add(c.ResourcePart));
            outPartsRemoved = OutParts.ItemsRemoved.Subscribe(c => Effect.OutResources.Remove(c.ResourcePart));
        }
    }
}
