using System.IO;
using System.Text;
using Reactive.Bindings;

namespace RuleSetEditor.ViewModels.RuleSetViewModels
{
    public class LandingPageViewModel : RuleSetViewModelBase
    {
        private ReactiveProperty<string> nameProperty;
        private RelayCommand saveCommand;

        public ReactiveProperty<string> Name
        {
            get { return nameProperty; }
            private set { RaiseSetIfChanged(ref nameProperty, value); }
        }

        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ?? (saveCommand = new RelayCommand(() =>
                {
                    FileInfo sourceFile = null;
                    if (string.IsNullOrEmpty(RuleSetViewModel.SourceFilePath))
                        sourceFile = new FileInfo($"{RuleSetViewModel.RuleSet.Name}.ruleset");
                    else
                        sourceFile = new FileInfo(RuleSetViewModel.SourceFilePath);
                    if (sourceFile.Extension != ".ruleset")
                        sourceFile.MoveTo(Path.Combine(sourceFile.Directory.FullName, $"{Path.GetFileNameWithoutExtension(sourceFile.Name)}.ruleset"));
                    if (Path.GetFileNameWithoutExtension(sourceFile.Name) != RuleSetViewModel.RuleSet.Name)
                        sourceFile.MoveTo(Path.Combine(sourceFile.Directory.FullName, $"{RuleSetViewModel.RuleSet.Name}.ruleset"));

                    using (var stream = sourceFile.Open(FileMode.Create, FileAccess.ReadWrite, FileShare.Read))
                        RuleSet.RuleSet.Save(RuleSetViewModel.RuleSet, stream);
                }));
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Dispose(ref nameProperty);
                Dispose(ref saveCommand);
            }
            base.Dispose(disposing);
        }

        protected override void OnRuleSetChanged()
        {
            base.OnRuleSetChanged();
            Name = RuleSetViewModel.Name;
        }
    }
}
