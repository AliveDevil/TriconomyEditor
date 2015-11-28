using System;
using System.Collections.Generic;
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
                delegates.Add(value);
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
                delegates.Remove(value);
            }
        }

        private Func<bool> canExecuteFunc;
        private List<EventHandler> delegates = new List<EventHandler>();
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

        public void RemoveAllEvents()
        {
            foreach (EventHandler eh in delegates)
            {
                CommandManager.RequerySuggested -= eh;
            }
            delegates.Clear();
        }
    }
}
