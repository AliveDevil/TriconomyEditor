using System;
using ReactiveUI;
using RuleSet;
using RuleSet.Needs;
using RuleSetEditor.ViewModels.RuleSetViewModels.NeedViewModels;
using RuleSetEditor.ViewModels.RuleSetViewModels.NeedViewModels.EditViewModels;

namespace RuleSetEditor.ViewModels.RuleSetViewModels
{
    public class NeedsListViewModel : RuleSetViewModelBase
    {
        private RelayCommand<Type> addNeedCommand;
        private RelayCommand<NeedViewModel> editNeedCommand;
        private IReactiveDerivedList<NeedViewModel> needs;
        private RelayCommand removeNeedCommand;
        private NeedViewModel selectedNeed;

        public RelayCommand<Type> AddNeedCommand
        {
            get
            {
                return addNeedCommand ?? (addNeedCommand = new RelayCommand<Type>(t =>
                {
                    var need = (Need)Activator.CreateInstance(t);
                    if (need is ResourceNeed)
                        AddAndSelectNewResource<ResourceNeedViewModel, ResourceNeed>(need);
                    else if (need is BuildingNeed)
                        AddAndSelectNewResource<BuildingNeedViewModel, BuildingNeed>(need);
                }));
            }
        }

        public RelayCommand<NeedViewModel> EditNeedCommand
        {
            get
            {
                return editNeedCommand ?? (editNeedCommand = new RelayCommand<NeedViewModel>(need =>
                {
                    SelectedNeed = need;
                    if (need is ResourceNeedViewModel)
                        ViewStack.Push<ResourceNeedEditViewModel>()._(_ => { _.ResourceNeed = (ResourceNeedViewModel)need; });
                    else if (need is BuildingNeedViewModel)
                        ViewStack.Push<BuildingNeedEditViewModel>()._(_ => { _.BuildingNeed = (BuildingNeedViewModel)need; });
                }));
            }
        }

        public IReactiveDerivedList<NeedViewModel> Needs
        {
            get { return needs; }
            private set { RaiseSetIfChanged(ref needs, value); }
        }

        public RelayCommand RemoveNeedCommand
        {
            get
            {
                return removeNeedCommand ?? (removeNeedCommand = new RelayCommand(() =>
                {
                    RuleSetViewModel.Needs.Remove(SelectedNeed);
                }));
            }
        }

        public NeedViewModel SelectedNeed
        {
            get { return selectedNeed; }
            set { RaiseSetIfChanged(ref selectedNeed, value); }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Dispose(ref needs);
                AddNeedCommand?.Dispose();
                EditNeedCommand?.Dispose();
                RemoveNeedCommand?.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void OnRuleSetChanged()
        {
            base.OnRuleSetChanged();
            Needs = RuleSetViewModel.Needs.CreateDerivedCollection(e => e);
            Needs.ChangeTrackingEnabled = true;
        }

        private void AddAndSelectNewResource<TViewModel, TNeed>(Need need)
            where TViewModel : NeedViewModel<TNeed>, new()
            where TNeed : Need
        {
            TViewModel viewModel = new TViewModel()
            {
                RuleSetViewModel = RuleSetViewModel,
                Need = (TNeed)need
            };
            RuleSetViewModel.Needs.Add(viewModel);
            SelectedNeed = viewModel;
        }
    }
}
