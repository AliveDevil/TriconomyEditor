using System.Windows;
using libUIStack;
using RuleSetEditor.Properties;
using RuleSetEditor.ViewModels;

namespace RuleSetEditor
{
    public partial class App : Application
    {
        public DefaultViewStack ViewState { get; private set; }

        public App()
        {
            InitializeComponent();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            ViewState.Clear();
            Settings.Default.Save();
            base.OnExit(e);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            ViewState = new DefaultViewStack();
            ViewState.Push<LandingPageViewModel>();
            base.OnStartup(e);
        }
    }
}
