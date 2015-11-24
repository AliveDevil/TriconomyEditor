using System.IO;
using RuleSetEditor.Properties;

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
                    using (var reader = file.OpenText())
                        ruleSet = RuleSet.RuleSet.Load(reader);
                    Settings.Default.LastFile = file.FullName;
                    ViewStack.Set<RuleSetViewModel>()._(_ =>
                    {
                        _.SourceFilePath = file.FullName;
                        _.RuleSet = ruleSet;
                    });
                }, () => !string.IsNullOrEmpty(Settings.Default.LastFile) && File.Exists(Settings.Default.LastFile)));
            }
        }
    }
}
