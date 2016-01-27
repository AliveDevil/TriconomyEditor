using ReactiveUI;
using RuleSet.Elements;
using RuleSetEditor.ViewModels.RuleSetViewModels;
using RuleSetEditor.ViewModels.RuleSetViewModels.ResourcePartViewModels;

namespace RuleSetEditor.ViewModels.EffectViewModels.EffectEditViewModels
{
    public class AddRecipeEffectEditViewModel : RuleSetViewModelBase
    {
        private RelayCommand addInResourcePartCommand;
        private RelayCommand addOutResourcePartCommand;
        private AddRecipeEffectViewModel addRecipeEffect;
        private RelayCommand<ResourcePartViewModel> editResourcePartCommand;
        private ReactiveList<ResourcePartViewModel> inParts;
        private ReactiveList<ResourcePartViewModel> outParts;
        private RelayCommand removeInResourcePartCommand;
        private RelayCommand removeOutResourcePartCommand;
        private ResourcePartViewModel selectedInPart;
        private ResourcePartViewModel selectedOutPart;

        public RelayCommand AddInResourcePartCommand
        {
            get
            {
                return addInResourcePartCommand ?? (addInResourcePartCommand = new RelayCommand(() =>
                {
                    var viewModel = new ResourcePartViewModel()
                    {
                        RuleSetViewModel = RuleSetViewModel,
                        ResourcePart = new ResourcePart()
                    };
                    AddRecipeEffect.InParts.Add(viewModel);
                    SelectedInPart = viewModel;
                    EditResourcePartCommand.Execute(viewModel);
                }));
            }
        }

        public RelayCommand AddOutResourcePartCommand
        {
            get
            {
                return addOutResourcePartCommand ?? (addOutResourcePartCommand = new RelayCommand(() =>
                {
                    var viewModel = new ResourcePartViewModel()
                    {
                        RuleSetViewModel = RuleSetViewModel,
                        ResourcePart = new ResourcePart()
                    };
                    AddRecipeEffect.OutParts.Add(viewModel);
                    selectedOutPart = viewModel;
                    EditResourcePartCommand.Execute(viewModel);
                }));
            }
        }

        public AddRecipeEffectViewModel AddRecipeEffect
        {
            get
            {
                return addRecipeEffect;
            }
            set
            {
                if (!RaiseSetIfChanged(ref addRecipeEffect, value))
                    return;
                InParts = AddRecipeEffect.InParts;
                OutParts = AddRecipeEffect.OutParts;
            }
        }

        public RelayCommand<ResourcePartViewModel> EditResourcePartCommand
        {
            get
            {
                return editResourcePartCommand ?? (editResourcePartCommand = new RelayCommand<ResourcePartViewModel>(part =>
                {
                    ViewStack.Push<ResourcePartEditViewModel>()._(_ => _.ResourcePart = part);
                }));
            }
        }

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
        
        public RelayCommand RemoveInResourcePartCommand
        {
            get
            {
                return removeInResourcePartCommand ?? (removeInResourcePartCommand = new RelayCommand(() =>
                {
                    AddRecipeEffect.InParts.Remove(SelectedInPart);
                }));
            }
        }

        public RelayCommand RemoveOutResourcePartCommand
        {
            get
            {
                return removeOutResourcePartCommand ?? (removeOutResourcePartCommand = new RelayCommand(() =>
                {
                    AddRecipeEffect.OutParts.Remove(SelectedOutPart);
                }));
            }
        }

        public ResourcePartViewModel SelectedInPart
        {
            get { return selectedInPart; }
            set { RaiseSetIfChanged(ref selectedInPart, value); }
        }

        public ResourcePartViewModel SelectedOutPart
        {
            get { return selectedOutPart; }
            set { RaiseSetIfChanged(ref selectedOutPart, value); }
        }
    }
}
