﻿using System;
using System.Windows.Input;

namespace todoList
{
    public class DelegateCommand : ICommand
    {
        private readonly Action<object> executeDelegate;
        private readonly Func<object, bool> canExecuteDelegate;
        public DelegateCommand(Action<object> executeDelegate, Func<object, bool> canExecuteDelegate)
        {
            this.executeDelegate = executeDelegate;
            this.canExecuteDelegate = canExecuteDelegate;
        }

        public bool CanExecute(object parameter)
        {
            if (canExecuteDelegate != null)
                return canExecuteDelegate(parameter);
            return true;
        }

        public void Execute(object parameter)
        {
            if (executeDelegate != null)
                executeDelegate(parameter);
        }

        public event EventHandler CanExecuteChanged // MAGIC ! dont touch !
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
