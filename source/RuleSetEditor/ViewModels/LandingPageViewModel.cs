using System.IO;

namespace RuleSetEditor.ViewModels
{
    public class LandingPageViewModel : ViewModelBase
    {
        private RelayCommand createNewRuleSetCommand;
        private RelayCommand openLastRuleSetCommand;

        public RelayCommand CreateNewRuleSetCommand
        {
            get
            {
                return createNewRuleSetCommand ?? (createNewRuleSetCommand = new RelayCommand(() =>
                {
                    ViewStack.Set<RuleSetViewModel>()._(_ =>
                    {
                        _.RuleSet = new RuleSet.RuleSet() { Name = "New RuleSet" };
                        _.Initialize();
                        _.PostInitialize();
                    });
                }));
            }
        }

        public RelayCommand OpenLastRuleSetCommand
        {
            get
            {
                return openLastRuleSetCommand ?? (openLastRuleSetCommand = new RelayCommand(() =>
                {
                    RuleSet.RuleSet ruleSet;
                    FileInfo file = new FileInfo(Settings.Default.LastFile);
                    using (var stream = file.OpenRead())
                        ruleSet = RuleSet.RuleSet.Load(stream);
                    Settings.Default.LastFile = file.FullName;
                    ViewStack.Set<RuleSetViewModel>()._(_ =>
                    {
                        _.SourceFilePath = file.FullName;
                        _.RuleSet = ruleSet;
                        _.Initialize();
                        _.PostInitialize();
                    });
                }, () => !string.IsNullOrEmpty(Settings.Default.LastFile) && File.Exists(Settings.Default.LastFile)));
            }
        }
    }
}
