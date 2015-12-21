using System;
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
        private IViewStack viewStack;

        public RelayCommand CloseCommand => closeCommand ?? (closeCommand = new RelayCommand(ViewStack.Pop));

        public IViewStack ViewStack
        {
            get
            {
                return viewStack;
            }
            set
            {
                if (!RaiseSetIfChanged(ref viewStack, value)) return;
                OnViewStackChanged();
            }
        }

        public void Dispose()
        {
            Dispose(!disposed);
        }

        public void Dispose<T>(ref T t) where T : class, IDisposable
        {
            t?.Dispose();
            t = null;
        }

        public void Update()
        {
            OnPropertyChanged(this, new PropertyChangedEventArgs(string.Empty));
        }

        protected virtual void Dispose(bool disposing)
        {
            PropertyChanged = null;
            closeCommand = null;
            disposed = true;
        }

        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(sender, e);
        }

        protected virtual void OnViewStackChanged()
        {
        }

        protected bool RaiseSetIfChanged<T>(ref T field, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            return true;
        }
    }
}
