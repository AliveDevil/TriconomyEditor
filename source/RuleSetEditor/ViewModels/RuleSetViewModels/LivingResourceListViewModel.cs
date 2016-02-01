using ReactiveUI;
using RuleSet.Elements;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels.LivingResourceViewModels;

namespace RuleSetEditor.ViewModels.RuleSetViewModels
{
    public class LivingResourceListViewModel : RuleSetViewModelBase
    {
        private LivingResourceViewModel selectedLivingResourceViewModel;
        private IReactiveDerivedList<LivingResourceViewModel> livingResources;
        private RelayCommand<LivingResourceViewModel> editWorldResourceCommand;

        public RelayCommand AddLivingResourceCommand => new RelayCommand(() =>
        {
            LivingResourceViewModel model = new LivingResourceViewModel() { RuleSetViewModel = RuleSetViewModel, Element = new LivingResource() { Name = "New Living Resource" } };
            RuleSetViewModel.ElementList.Add(model);
            SelectedLivingResource = model;
        });

        public RelayCommand<LivingResourceViewModel> EditLivingResourceCommand
        {
            get
            {
                return editWorldResourceCommand ?? (editWorldResourceCommand = new RelayCommand<LivingResourceViewModel>(worldResource =>
                {
                    SelectedLivingResource = worldResource;
                    ViewStack.Push<LivingResourceEditViewModel>()._(_ => _.LivingResource = worldResource);
                }));
            }
        }

        public RelayCommand RemoveLivingResourceCommand => new RelayCommand(() =>
        {
            RuleSetViewModel.ElementList.Remove(SelectedLivingResource);
        });

        public LivingResourceViewModel SelectedLivingResource
        {
            get { return selectedLivingResourceViewModel; }
            set { RaiseSetIfChanged(ref selectedLivingResourceViewModel, value); }
        }

        public IReactiveDerivedList<LivingResourceViewModel> LivingResources
        {
            get { return livingResources; }
            private set { RaiseSetIfChanged(ref livingResources, value); }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Dispose(ref livingResources);
                AddLivingResourceCommand.Dispose();
                EditLivingResourceCommand.Dispose();
                RemoveLivingResourceCommand.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void OnRuleSetChanged()
        {
            base.OnRuleSetChanged();
            LivingResources = RuleSetViewModel.ElementList.CreateDerivedCollection(e => (LivingResourceViewModel)e, e => e is LivingResourceViewModel, (l, r) => l.Name.Value.CompareTo(r.Name.Value));
            LivingResources.ChangeTrackingEnabled = true;
        }
    }
}
