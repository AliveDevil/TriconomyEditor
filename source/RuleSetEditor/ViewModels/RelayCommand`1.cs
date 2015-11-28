using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace RuleSetEditor.ViewModels
{
    public class RelayCommand<T> : ICommand, IDisposable
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

        private Func<T, bool> canExecuteFunc;
        private List<EventHandler> delegates = new List<EventHandler>();
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



        public void RemoveAllEvents()
        {
            foreach (EventHandler eh in delegates)
            {
                CanExecuteChanged -= eh;
            }
            delegates.Clear();
        }

        public void Dispose()
        {
            RemoveAllEvents();
        }
    }
}
