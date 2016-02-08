using Reactive.Bindings;
using RuleSet.Menus;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.ToolbarViewModels
{
    public class OpenToolbarItemViewModel : ToolbarItemViewModel<OpenToolbarItem>
    {
        private ReactiveProperty<ToolbarViewModel> toolbarProperty;

        public ReactiveProperty<ToolbarViewModel> Toolbar
        {
            get
            {
                return toolbarProperty;
            }
            private set
            {
                toolbarProperty = value;
            }
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Toolbar = ReactiveProperty.FromObject(MenuItem,
                t => t.Toolbar,
                t => RuleSetViewModel.Create<ToolbarViewModel>(v =>
                {
                    v.Toolbar = t;
                }),
                t => t?.Toolbar);
        }
    }
}
