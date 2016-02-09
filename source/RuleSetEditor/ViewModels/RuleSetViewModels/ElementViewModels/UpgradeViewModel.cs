using System;
using Reactive.Bindings;
using ReactiveUI;
using RuleSet;
using RuleSet.Elements;
using RuleSetEditor.ViewModels.EffectViewModels;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels
{
    public class UpgradeViewModel : RuleSetViewModelBase
    {
        private RelayCommand addCostCommand;
        private RelayCommand<Type> addEffectCommand;
        private IDisposable costAdded;
        private IDisposable costListConstructor;
        private IDisposable costListInitializer;
        private IDisposable costListPostDisposer;
        private IDisposable costListPostInitializer;
        private IDisposable costRemoved;
        private ReactiveList<ResourcePartViewModel> costs;
        private RelayCommand<ResourcePartViewModel> editCostCommand;
        private RelayCommand<EffectViewModel> editEffectCommand;
        private IDisposable effectAdded;
        private IDisposable effectListConstructor;
        private IDisposable effectListInitializer;
        private IDisposable effectListPostDisposer;
        private IDisposable effectListPostInitializer;
        private IDisposable effectRemoved;
        private ReactiveList<EffectViewModel> effects;
        private ReactiveProperty<int> levelProperty;
        private RelayCommand removeCostCommand;
        private RelayCommand removeEffectCommand;
        private ResourcePartViewModel selectedCost;
        private EffectViewModel selectedEffect;
        private Upgrade upgrade;

        public RelayCommand AddCostCommand => addCostCommand ?? (addCostCommand = new RelayCommand(() =>
        {
            Costs.Add(ViewStack.Push(RuleSetViewModel.Create<ResourcePartViewModel>(v =>
            {
                v.ResourcePart = new ResourcePart();
            })));
        }));

        public RelayCommand<Type> AddEffectCommand => addEffectCommand ?? (addEffectCommand = new RelayCommand<Type>(t =>
        {
            AddEffect((Effect)Activator.CreateInstance(t));
        }));

        public ReactiveList<ResourcePartViewModel> Costs
        {
            get
            {
                return costs;
            }
            private set
            {
                RaiseSetIfChanged(ref costs, value);
            }
        }

        public RelayCommand<ResourcePartViewModel> EditCostCommand => editCostCommand ?? (editCostCommand = new RelayCommand<ResourcePartViewModel>(resourcePart =>
        {
            ViewStack.Push(SelectedCost = resourcePart);
        }));

        public RelayCommand<EffectViewModel> EditEffectCommand => editEffectCommand ?? (editEffectCommand = new RelayCommand<EffectViewModel>(e =>
        {
            ViewStack.Push(e);
        }));

        public ReactiveList<EffectViewModel> Effects
        {
            get
            {
                return effects;
            }
            private set
            {
                RaiseSetIfChanged(ref effects, value);
            }
        }

        public ReactiveProperty<int> Level
        {
            get
            {
                return levelProperty;
            }
            private set
            {
                Dispose(ref levelProperty);
                if (!RaiseSetIfChanged(ref levelProperty, value))
                    return;
                Level.PropertyChanged += OnPropertyChanged;
            }
        }

        public RelayCommand RemoveCostCommand => removeCostCommand ?? (removeCostCommand = new RelayCommand(() =>
        {
            Costs.Remove(SelectedCost);
        }));

        public RelayCommand RemoveEffectCommand => removeEffectCommand ?? (removeEffectCommand = new RelayCommand(() =>
        {
            Effects.Remove(SelectedEffect);
        }));

        public ResourcePartViewModel SelectedCost
        {
            get
            {
                return selectedCost;
            }
            set
            {
                RaiseSetIfChanged(ref selectedCost, value);
            }
        }

        public EffectViewModel SelectedEffect
        {
            get
            {
                return selectedEffect;
            }
            set
            {
                RaiseSetIfChanged(ref selectedEffect, value);
            }
        }

        public Upgrade Upgrade
        {
            get
            {
                return upgrade;
            }
            set
            {
                RaiseSetIfChanged(ref upgrade, value);
            }
        }
        
        protected override void OnInitialize()
        {
            base.OnInitialize();

            Level = ReactiveProperty.FromObject(Upgrade, u => u.Level);

            Effects = new ReactiveList<EffectViewModel>() { ChangeTrackingEnabled = true };
            effectListInitializer = Effects.BeforeItemsAdded.Subscribe(e => e.Initialize());
            effectListPostInitializer = Effects.ItemsAdded.Subscribe(e => e.PostInitialize());
            effectListPostDisposer = Effects.BeforeItemsRemoved.Subscribe(e => e.Dispose());

            Costs = new ReactiveList<ResourcePartViewModel>() { ChangeTrackingEnabled = true };
            costListInitializer = Costs.BeforeItemsAdded.Subscribe(c => c.Initialize());
            costListPostInitializer = Costs.ItemsAdded.Subscribe(c => c.PostInitialize());
            costListPostDisposer = Costs.BeforeItemsRemoved.Subscribe(c => c.Dispose());
        }

        protected override void OnPostInitialize()
        {
            base.OnPostInitialize();

            foreach (var item in Upgrade.Effects)
            {
                Effects.Add(EffectViewModel.FindViewModel(item, RuleSetViewModel));
            }
            effectListConstructor = Effects.BeforeItemsAdded.Subscribe(e => e.Construct());
            effectAdded = Effects.BeforeItemsAdded.Subscribe(e => Upgrade.Effects.Add(e?.Effect));
            effectRemoved = Effects.ItemsRemoved.Subscribe(e => Upgrade.Effects.Remove(e.Effect));

            foreach (var item in Upgrade.Costs)
            {
                Costs.Add(RuleSetViewModel.Create<ResourcePartViewModel>(f =>
                {
                    f.ResourcePart = item;
                }));
            }
            costListConstructor = Costs.BeforeItemsAdded.Subscribe(c => c.Construct());
            costAdded = Costs.BeforeItemsAdded.Subscribe(c => Upgrade.Costs.Add(c.ResourcePart));
            costRemoved = Costs.ItemsRemoved.Subscribe(c => Upgrade.Costs.Remove(c.ResourcePart));
        }

        private void AddEffect(Effect effect)
        {
            Effects.Add(ViewStack.Push(EffectViewModel.FindViewModel(effect, RuleSetViewModel)));
        }
    }
}
