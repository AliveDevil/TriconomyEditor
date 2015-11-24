using System;
using System.Windows.Input;

namespace RuleSetEditor.ViewModels
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

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

        public void Execute(object parameter)
        {
            executeAction();
        }
    }
}
