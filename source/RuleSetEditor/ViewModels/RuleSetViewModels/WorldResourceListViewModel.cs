using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using RuleSet.Elements;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels;
using RuleSetEditor.ViewModels.RuleSetViewModels.ElementViewModels.WorldResourceViewModels;

namespace RuleSetEditor.ViewModels.RuleSetViewModels
{
    public class WorldResourceListViewModel : RuleSetViewModelBase
    {
        private RelayCommand addWorldResourceCommand;
        private RelayCommand removeWorldResourceCommand;
        private IReactiveDerivedList<WorldResourceInfoViewModel> worldResources;
        private WorldResourceInfoViewModel selectedWorldResource;

        public RelayCommand AddWorldResourceCommand
        {
            get
            {
                return addWorldResourceCommand ?? (addWorldResourceCommand = new RelayCommand(() =>
                {
                    RuleSetViewModel.ElementList.Add(new WorldResourceViewModel() { RuleSetViewModel = RuleSetViewModel, Element = new WorldResource() { Name = "New World Resource" } });
                }));
            }
        }

        public RelayCommand RemoveWorldResourceCommand
        {
            get
            {
                return removeWorldResourceCommand ?? (removeWorldResourceCommand = new RelayCommand(() =>
                {
                    RuleSetViewModel.ElementList.Remove(SelectedWorldResource?.WorldResource);
                }));
            }
        }

        public IReactiveDerivedList<WorldResourceInfoViewModel> WorldResources
        {
            get { return worldResources; }
            private set { RaiseSetIfChanged(ref worldResources, value); }
        }

        private RelayCommand<WorldResourceInfoViewModel> editWorldResourceCommand;

        public RelayCommand<WorldResourceInfoViewModel> EditWorldResourceCommand
        {
            get
            {
                return editWorldResourceCommand ?? (editWorldResourceCommand = new RelayCommand<WorldResourceInfoViewModel>(worldResource =>
                {
                    ViewStack.Push<WorldResourceEditViewModel>()._(_ => _.WorldResource = worldResource.WorldResource);
                }));
            }
        }


        public WorldResourceInfoViewModel SelectedWorldResource
        {
            get { return selectedWorldResource; }
            set { RaiseSetIfChanged(ref selectedWorldResource, value); }
        }

        protected override void OnRuleSetChanged()
        {
            base.OnRuleSetChanged();
            WorldResources = RuleSetViewModel.ElementList.CreateDerivedCollection(e => new WorldResourceInfoViewModel() { WorldResource = (WorldResourceViewModel)e }, e => e is WorldResourceViewModel);
        }
    }
}
