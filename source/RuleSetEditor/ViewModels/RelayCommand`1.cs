using System;
using System.Windows.Input;

namespace RuleSetEditor.ViewModels
{
    public class RelayCommand<T> : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private Func<T, bool> canExecuteFunc;
        private Action<T> executeAction;

        public RelayCommand(Action<T> executeAction) : this(executeAction, (ignore) => true)
        {
        }

        public RelayCommand(Action<T> executeAction, Func<T, bool> canExecuteFunc)
        {
            this.executeAction = executeAction;
            this.canExecuteFunc = canExecuteFunc;
        }

        public bool CanExecute(object parameter)
        {
            if (!(parameter is T)) return false;

            return canExecuteFunc((T)parameter);
        }

        public void Execute(object parameter)
        {
            if (!(parameter is T)) return;

            executeAction((T)parameter);
        }
    }
}
