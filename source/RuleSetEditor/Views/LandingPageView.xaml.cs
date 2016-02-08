using System.IO;
using System.Windows;
using System.Windows.Controls;
using RuleSetEditor.ViewModels;

namespace RuleSetEditor.Views
{
    public partial class LandingPageView : UserControl
    {
        public LandingPageView()
        {
            InitializeComponent();
        }

        private void Label_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length == 1)
                {
                    e.Effects = DragDropEffects.Link;
                    e.Handled = true;
                    return;
                }
            }
            e.Effects = DragDropEffects.None;
            e.Handled = true;
        }

        private void Label_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length == 1)
                {
                    e.Effects = DragDropEffects.Link;
                    e.Handled = true;
                    return;
                }
            }
            e.Effects = DragDropEffects.None;
            e.Handled = true;
        }

        private void Label_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length == 1)
                {
                    RuleSet.RuleSet ruleSet;
                    FileInfo file = new FileInfo(files[0]);
                    using (var stream = file.OpenRead())
                        ruleSet = RuleSet.RuleSet.Load(stream);
                    Settings.Default.LastFile = file.FullName;
                    ((ViewModelBase)DataContext).ViewStack.Set<RuleSetViewModel>()._(_ =>
                    {
                        _.SourceFilePath = file.FullName;
                        _.RuleSet = ruleSet;
                        _.Initialize();
                        _.PostInitialize();
                    });
                }
            }
        }
    }
}
