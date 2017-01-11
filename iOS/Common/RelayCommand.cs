﻿using System;
using System.Windows.Input;

namespace ProjectMato.iOS.Common
{
    public class RelayCommand : ICommand
    {
        private readonly Predicate<object> _canExecute = null;
        private readonly Action<object> _execute = null;

        public RelayCommand(Predicate<object> canExecute, Action<object> execute)
        {
            _canExecute = canExecute;
            _execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute != null)
            {
                return (_canExecute(parameter));
            }

            return false;
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseExecuteChanged()
        {
            try
            {
                if (CanExecuteChanged != null)
                {
                    CanExecuteChanged(this, EventArgs.Empty);
                }

            }
            catch (Exception ex)
            {


            }
        }

        public void Execute(object parameter)
        {
            if (_execute != null)
            {
                _execute(parameter);
            }
        }
    }
}
