﻿using System;
using System.Linq;
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
                CanExecuteChangedInternal += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
                CanExecuteChangedInternal -= value;
            }
        }

        private event EventHandler CanExecuteChangedInternal;

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

        public void Dispose()
        {
            RemoveAllEvents();
        }

        public void Execute(object parameter)
        {
            if (!(parameter is T)) return;

            executeAction((T)parameter);
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
