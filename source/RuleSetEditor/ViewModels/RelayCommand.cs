using System;
using System.Linq;
using System.Windows.Input;

namespace RuleSetEditor.ViewModels
{
    public class RelayCommand : ICommand, IDisposable
    {
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                CanExecuteChangedInternal += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
                CanExecuteChangedInternal -= value;
            }
        }

        private event EventHandler CanExecuteChangedInternal;

        private Func<bool> canExecuteFunc;
        private Action executeAction;

        public RelayCommand(Action executeAction) : this(executeAction, () => true)
        {
        }

        public RelayCommand(Action executeAction, Func<bool> canExecuteFunc)
        {
            this.executeAction = executeAction;
            this.canExecuteFunc = canExecuteFunc;
        }

        public bool CanExecute(object parameter)
        {
            return canExecuteFunc();
        }

        public void Dispose()
        {
            RemoveAllEvents();
        }

        public void Execute(object parameter)
        {
            executeAction();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChangedInternal?.Invoke(this, EventArgs.Empty);
        }

        public void RemoveAllEvents()
        {
            foreach (var item in CanExecuteChangedInternal?.GetInvocationList() ?? Enumerable.Empty<Delegate>())
            {
                CanExecuteChanged -= (EventHandler)item;
            }
            CanExecuteChangedInternal = null;
        }
    }
}
