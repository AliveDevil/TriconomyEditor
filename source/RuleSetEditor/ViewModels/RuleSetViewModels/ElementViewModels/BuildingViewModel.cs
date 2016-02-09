using System;
using Reactive.Bindings;
using ReactiveUI;
using RuleSet;
using RuleSet.Elements;
using RuleSetEditor.ViewModels.EventViewModels;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels
{
    public class BuildingViewModel : ElementViewModel<Building>
    {
        private RelayCommand addUpgradeCommand;
        private RelayCommand<UpgradeViewModel> editUpgradeCommand;
        private IDisposable eventAdded;
        private IDisposable eventListConstructor;
        private IDisposable eventListInitializer;
        private IDisposable eventListPostDisposer;
        private IDisposable eventListPostInitializer;
        private IDisposable eventRemoved;
        private ReactiveList<EventViewModel> events;
        private RelayCommand removeUpgradeCommand;
        private UpgradeViewModel selectedUpgrade;
        private IDisposable upgradeAdded;
        private ReactiveList<UpgradeViewModel> upgradeList;
        private IDisposable upgradeListConstructor;
        private IDisposable upgradeListInitializer;
        private IDisposable upgradeListPostDisposer;
        private IDisposable upgradeListPostInitializer;
        private IDisposable upgradeRemoved;
        private ReactiveProperty<int> variantsProperty;

        public RelayCommand AddUpgradeCommand
        {
            get
            {
                return addUpgradeCommand ?? (addUpgradeCommand = new RelayCommand(() =>
                {
                    UpgradeList.Add(ViewStack.Push(RuleSetViewModel.Create<UpgradeViewModel>(v =>
                    {
                        v.Upgrade = new Upgrade() { Level = 0 };
                    })));
                }));
            }
        }

        public RelayCommand<UpgradeViewModel> EditUpgradeCommand
        {
            get
            {
                return editUpgradeCommand ?? (editUpgradeCommand = new RelayCommand<UpgradeViewModel>(u =>
                {
                    ViewStack.Push(u);
                }));
            }
        }

        public ReactiveList<EventViewModel> Events
        {
            get
            {
                return events;
            }
            private set
            {
                RaiseSetIfChanged(ref events, value);
            }
        }

        public RelayCommand RemoveUpgradeCommand
        {
            get
            {
                return removeUpgradeCommand ?? (removeUpgradeCommand = new RelayCommand(() =>
                {
                    UpgradeList.Remove(SelectedUpgrade);
                }));
            }
        }

        public UpgradeViewModel SelectedUpgrade
        {
            get
            {
                return selectedUpgrade;
            }
            set
            {
                RaiseSetIfChanged(ref selectedUpgrade, value);
            }
        }

        public ReactiveList<UpgradeViewModel> UpgradeList
        {
            get
            {
                return upgradeList;
            }
            private set
            {
                RaiseSetIfChanged(ref upgradeList, value);
            }
        }

        public ReactiveProperty<int> Variants
        {
            get
            {
                return variantsProperty;
            }
            private set
            {
                RaiseSetIfChanged(ref variantsProperty, value);
            }
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Variants = ReactiveProperty.FromObject(Element, b => b.Variants);
            Events = new ReactiveList<EventViewModel>()
            {
                ChangeTrackingEnabled = true
            };
            eventListInitializer = Events.BeforeItemsAdded.Subscribe(u => u.Initialize());
            eventListPostInitializer = Events.ItemsAdded.Subscribe(u => u.PostInitialize());
            eventListPostDisposer = Events.BeforeItemsRemoved.Subscribe(u => u.Dispose());

            UpgradeList = new ReactiveList<UpgradeViewModel>()
            {
                ChangeTrackingEnabled = true
            };

            upgradeListInitializer = UpgradeList.BeforeItemsAdded.Subscribe(u => u.Initialize());
            upgradeListPostInitializer = UpgradeList.ItemsAdded.Subscribe(u => u.PostInitialize());
            upgradeListPostDisposer = UpgradeList.BeforeItemsRemoved.Subscribe(u => u.Dispose());
        }

        protected override void OnPostInitialize()
        {
            base.OnPostInitialize();

            foreach (var e in Element.Events)
            {
                Events.Add(RuleSetViewModel.Create<EventViewModel>(v =>
                {
                    v.Event = e;
                }));
            }

            eventListConstructor = Events.BeforeItemsAdded.Subscribe(u => u.Construct());
            eventAdded = Events.BeforeItemsAdded.Subscribe(u => Element.Events.Add(u.Event));
            eventRemoved = Events.ItemsRemoved.Subscribe(u => Element.Events.Remove(u.Event));

            foreach (var upgrade in Element.Upgrades)
            {
                UpgradeList.Add(RuleSetViewModel.Create<UpgradeViewModel>(v =>
                {
                    v.Upgrade = upgrade;
                }));
            }

            upgradeListConstructor = UpgradeList.BeforeItemsAdded.Subscribe(u => u.Construct());
            upgradeAdded = UpgradeList.BeforeItemsAdded.Subscribe(u => Element.Upgrades.Add(u.Upgrade));
            upgradeRemoved = UpgradeList.ItemsRemoved.Subscribe(u => Element.Upgrades.Remove(u.Upgrade));
        }
    }
}
