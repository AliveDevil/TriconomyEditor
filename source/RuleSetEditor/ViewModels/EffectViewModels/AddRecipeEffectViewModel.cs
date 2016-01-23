using System;
using System.Linq;
using ReactiveUI;
using RuleSet.Effects;
using RuleSetEditor.ViewModels.RuleSetViewModels;

namespace RuleSetEditor.ViewModels.EffectViewModels
{
    public class AddRecipeEffectViewModel : EffectViewModel<AddRecipeEffect>
    {
        private IDisposable beforeInPartAdded;
        private IDisposable beforeInPartRemoved;
        private IDisposable beforeOutPartAdded;
        private IDisposable beforeOutPartRemoved;
        private ReactiveList<ResourcePartViewModel> inParts;
        private ReactiveList<ResourcePartViewModel> outParts;

        public ReactiveList<ResourcePartViewModel> InParts
        {
            get { return inParts; }
            private set { RaiseSetIfChanged(ref inParts, value); }
        }

        public ReactiveList<ResourcePartViewModel> OutParts
        {
            get { return outParts; }
            private set { RaiseSetIfChanged(ref outParts, value); }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Dispose(ref beforeInPartAdded);
                Dispose(ref beforeInPartRemoved);
                Dispose(ref beforeOutPartAdded);
                Dispose(ref beforeOutPartRemoved);

                foreach (var part in InParts)
                    part.Dispose();
                foreach (var part in OutParts)
                    part.Dispose();

                InParts.Clear();
                OutParts.Clear();
                inParts = null;
                outParts = null;
            }
            base.Dispose(disposing);
        }

        protected override void OnEffectChanged()
        {
            base.OnEffectChanged();

            InParts = new ReactiveList<ResourcePartViewModel>(Effect.InResources.Select(p => new ResourcePartViewModel()
            {
                RuleSetViewModel = RuleSetViewModel,
                ResourcePart = p
            }))
            {
                ChangeTrackingEnabled = true
            };
            OutParts = new ReactiveList<ResourcePartViewModel>(Effect.OutResources.Select(p => new ResourcePartViewModel()
            {
                RuleSetViewModel = RuleSetViewModel,
                ResourcePart = p
            }))
            {
                ChangeTrackingEnabled = true
            };

            beforeInPartAdded = InParts.BeforeItemsAdded.Subscribe(p => Effect.InResources.Add(p.ResourcePart));
            beforeInPartRemoved = InParts.BeforeItemsRemoved.Subscribe(p => Effect.InResources.Remove(p.ResourcePart));
            beforeOutPartAdded = OutParts.BeforeItemsAdded.Subscribe(p => Effect.OutResources.Add(p.ResourcePart));
            beforeOutPartRemoved = OutParts.BeforeItemsRemoved.Subscribe(p => Effect.OutResources.Remove(p.ResourcePart));
        }
    }
}
