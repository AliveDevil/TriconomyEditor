using System;
using System.Linq;
using Reactive.Bindings;
using ReactiveUI;
using RuleSet.Effects;
using RuleSet.Elements;
using RuleSetEditor.ViewModels.EffectViewModels;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels
{
    public class BuildingViewModel : ElementViewModel
    {
        private IDisposable beforeEffectAdded;
        private IDisposable beforeEffectRemoved;
        private IDisposable beforeUpgradeAdded;
        private IDisposable beforeUpgradeRemoved;
        private ReactiveList<EffectViewModel> effectList;
        private ReactiveList<UpgradeViewModel> upgradeList;
        private ReactiveProperty<int> variantsProperty;

        public Building Building => (Building)Element;

        public ReactiveList<EffectViewModel> EffectList
        {
            get { return effectList; }
            private set { RaiseSetIfChanged(ref effectList, value); }
        }

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
                Dispose(ref variantsProperty);
                Dispose(ref beforeUpgradeAdded);
                Dispose(ref beforeUpgradeRemoved);
                Dispose(ref beforeEffectAdded);
                Dispose(ref beforeEffectRemoved);

                foreach (var item in UpgradeList)
                    item.Dispose();
                foreach (var item in EffectList)
                    item.Dispose();

                EffectList.Clear();
                UpgradeList.Clear();
                effectList = null;
                upgradeList = null;
            }
            base.Dispose(disposing);
        }

        protected override void OnElementChanged()
        {
            base.OnElementChanged();
            Variants = ReactiveProperty.FromObject(Building, b => b.Variants);
            UpgradeList = new ReactiveList<UpgradeViewModel>(Building.Upgrades.Select(u =>
            {
                return new UpgradeViewModel()
                {
                    RuleSetViewModel = RuleSetViewModel,
                    Upgrade = u
                };
            }))
            {
                ChangeTrackingEnabled = true
            };
            beforeUpgradeAdded = UpgradeList.BeforeItemsAdded.Subscribe(u => Building.Upgrades.Add(u.Upgrade));
            beforeUpgradeRemoved = UpgradeList.BeforeItemsRemoved.Subscribe(u => Building.Upgrades.Remove(u.Upgrade));

            EffectList = new ReactiveList<EffectViewModel>(Building.Effects.Select(e =>
            {
                EffectViewModel model = null;

                if (e is AddRecipeEffect) model = new AddRecipeEffectViewModel();

                if (model != null)
                {
                    model.RuleSetViewModel = RuleSetViewModel;
                    model.Effect = e;
                }

                return model;
            }).Where(e => e != null))
            {
                ChangeTrackingEnabled = true
            };
            beforeEffectAdded = EffectList.BeforeItemsAdded.Subscribe(e => Building.Effects.Add(e.Effect));
            beforeEffectRemoved = EffectList.BeforeItemsRemoved.Subscribe(e => Building.Effects.Remove(e.Effect));
        }
    }
}
