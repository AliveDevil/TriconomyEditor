using System.ComponentModel;
using System.Runtime.CompilerServices;
using libUIStack;

namespace RuleSetEditor.ViewModels
{
    public class ViewModelBase : IView, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private RelayCommand closeCommand;
        private bool disposed = false;

        public RelayCommand CloseCommand => closeCommand ?? (closeCommand = new RelayCommand(ViewStack.Pop));

        public IViewStack ViewStack
        {
            get; set;
        }

        public void Dispose()
        {
            Dispose(!disposed);
        }

        protected virtual void Dispose(bool disposing)
        {
            disposed = true;
        }

        protected void RaiseSetIfChanged<T>(ref T field, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(field, value)) return;
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
