using System;
using System.Linq;
using ReactiveUI;
using RuleSet.Effects;

namespace RuleSetEditor.ViewModels.EffectViewModels
{
    public class AddRecipeEffectViewModel : EffectViewModel
    {
        private IDisposable beforeInPartAdded;
        private IDisposable beforeInPartRemoved;
        private IDisposable beforeOutPartAdded;
        private IDisposable beforeOutPartRemoved;
        private ReactiveList<RecipePartViewModel> inParts;
        private ReactiveList<RecipePartViewModel> outParts;

        public AddRecipeEffect AddRecipeEffect => (AddRecipeEffect)Effect;

        public ReactiveList<RecipePartViewModel> InParts
        {
            get { return inParts; }
            private set { RaiseSetIfChanged(ref inParts, value); }
        }

        public ReactiveList<RecipePartViewModel> OutParts
        {
            get { return outParts; }
            private set { RaiseSetIfChanged(ref outParts, value); }
        }

        public override string ToString()
        {
            return "Add Recipe";
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

            InParts = new ReactiveList<RecipePartViewModel>(AddRecipeEffect.InResources.Select(p => new RecipePartViewModel()
            {
                RuleSetViewModel = RuleSetViewModel,
                RecipePart = p
            }))
            {
                ChangeTrackingEnabled = true
            };
            OutParts = new ReactiveList<RecipePartViewModel>(AddRecipeEffect.OutResources.Select(p => new RecipePartViewModel()
            {
                RuleSetViewModel = RuleSetViewModel,
                RecipePart = p
            }))
            {
                ChangeTrackingEnabled = true
            };

            beforeInPartAdded = InParts.BeforeItemsAdded.Subscribe(p => AddRecipeEffect.InResources.Add(p.RecipePart));
            beforeInPartRemoved = InParts.BeforeItemsRemoved.Subscribe(p => AddRecipeEffect.InResources.Remove(p.RecipePart));
            beforeOutPartAdded = OutParts.BeforeItemsAdded.Subscribe(p => AddRecipeEffect.OutResources.Add(p.RecipePart));
            beforeOutPartRemoved = OutParts.BeforeItemsRemoved.Subscribe(p => AddRecipeEffect.OutResources.Remove(p.RecipePart));
        }
    }
}
