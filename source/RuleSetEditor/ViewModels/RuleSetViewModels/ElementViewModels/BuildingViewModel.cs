using System;
using System.Linq;
using Reactive.Bindings;
using ReactiveUI;
using RuleSet.Elements;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels
{
    public class BuildingViewModel : ElementViewModel
    {
        private ReactiveList<UpgradeViewModel> upgradeList;
        private ReactiveProperty<int> variantsProperty;

        public Building Building => (Building)Element;

        public ReactiveList<UpgradeViewModel> UpgradeList
        {
            get { return upgradeList; }
            private set { RaiseSetIfChanged(ref upgradeList, value); }
        }

        public ReactiveProperty<int> Variants
        {
            get { return variantsProperty; }
            private set { RaiseSetIfChanged(ref variantsProperty, value); }
        }

        protected override void OnElementChanged()
        {
            base.OnElementChanged();
            Variants = ReactiveProperty.FromObject(Building, b => b.Variants);
            UpgradeList = new ReactiveList<UpgradeViewModel>(Building.Upgrades.Select(u =>
            {
                return new UpgradeViewModel() { RuleSetViewModel = RuleSetViewModel, Upgrade = u };
            }));
            UpgradeList.ChangeTrackingEnabled = true;
            UpgradeList.BeforeItemsAdded.Subscribe(u => Building.Upgrades.Add(u.Upgrade));
            UpgradeList.BeforeItemsRemoved.Subscribe(u => Building.Upgrades.Remove(u.Upgrade));
        }
    }
}
