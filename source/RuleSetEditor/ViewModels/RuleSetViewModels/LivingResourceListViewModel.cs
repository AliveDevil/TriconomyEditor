using ReactiveUI;
using RuleSet.Elements;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels;

namespace RuleSetEditor.ViewModels.RuleSetViewModels
{
    public class LivingResourceListViewModel : RuleSetViewModelBase
    {
        private RelayCommand addLivingResourceCommand;
        private RelayCommand<LivingResourceViewModel> editLivingResourceCommand;
        private IReactiveDerivedList<LivingResourceViewModel> livingResources;
        private RelayCommand removeLivingResourceCommand;
        private LivingResourceViewModel selectedLivingResourceViewModel;

        public RelayCommand AddLivingResourceCommand => addLivingResourceCommand ?? (addLivingResourceCommand = new RelayCommand(() =>
        {
            LivingResourceViewModel model = new LivingResourceViewModel()
            {
                RuleSetViewModel = RuleSetViewModel,
                Element = new LivingResource()
                {
                    Name = "New Living Resource"
                }
            };
            RuleSetViewModel.ElementList.Add(model);
            SelectedLivingResource = model;
        }));

        public RelayCommand<LivingResourceViewModel> EditLivingResourceCommand => editLivingResourceCommand ?? (editLivingResourceCommand = new RelayCommand<LivingResourceViewModel>(worldResource =>
        {
            ViewStack.Push(SelectedLivingResource = worldResource);
        }));

        public IReactiveDerivedList<LivingResourceViewModel> LivingResources
        {
            get
            {
                return livingResources;
            }
            private set
            {
                RaiseSetIfChanged(ref livingResources, value);
            }
        }

        public RelayCommand RemoveLivingResourceCommand => removeLivingResourceCommand ?? (removeLivingResourceCommand = new RelayCommand(() =>
        {
            RuleSetViewModel.ElementList.Remove(SelectedLivingResource);
        }));

        public LivingResourceViewModel SelectedLivingResource
        {
            get
            {
                return selectedLivingResourceViewModel;
            }
            set
            {
                RaiseSetIfChanged(ref selectedLivingResourceViewModel, value);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Dispose(ref livingResources);
                Dispose(ref addLivingResourceCommand);
                Dispose(ref editLivingResourceCommand);
                Dispose(ref removeLivingResourceCommand);
            }
            base.Dispose(disposing);
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            LivingResources = RuleSetViewModel.ElementList.CreateDerivedCollection(e => (LivingResourceViewModel)e, e => e is LivingResourceViewModel, (l, r) => l.Name.Value.CompareTo(r.Name.Value));
            LivingResources.ChangeTrackingEnabled = true;
        }
    }
}
