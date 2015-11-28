using System;
using System.Linq;
using Reactive.Bindings;
using ReactiveUI;
using RuleSet.Elements;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels
{
    public class BuildingViewModel : ElementViewModel
    {
        private IDisposable beforeItemsAdded;
        private IDisposable beforeItemsRemoved;
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                beforeItemsAdded.Dispose();
                beforeItemsRemoved.Dispose();
                foreach (var item in UpgradeList)
                {
                    item.Dispose();
                }
                UpgradeList.Clear();
                Variants.Dispose();

                upgradeList = null;
                variantsProperty = null;
            }
            base.Dispose(disposing);
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
            beforeItemsAdded = UpgradeList.BeforeItemsAdded.Subscribe(u => Building.Upgrades.Add(u.Upgrade));
            beforeItemsRemoved = UpgradeList.BeforeItemsRemoved.Subscribe(u => Building.Upgrades.Remove(u.Upgrade));
        }
    }
}
