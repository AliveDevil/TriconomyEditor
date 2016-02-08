using System;
using Reactive.Bindings;
using ReactiveUI;
using RuleSet;
using RuleSet.Elements;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels
{
    public class BuildingViewModel : ElementViewModel<Building>
    {
        private RelayCommand addUpgradeCommand;
        private RelayCommand<UpgradeViewModel> editUpgradeCommand;
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
