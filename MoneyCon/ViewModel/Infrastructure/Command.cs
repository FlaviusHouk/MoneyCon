using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MoneyCon.ViewModel.Infrastructure
{
    public class Command : ICommand
    {
        private bool _canExecute = true;

        public Action<object> Action { get; }

        public bool CanExecuteFlag
        {
            get
            {
                return _canExecute;
            }

            set
            {
                _canExecute = value;
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public Command(Action<object> action)
        {
            Action = action;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return CanExecuteFlag;
        }

        public void Execute(object parameter)
        {
            Action?.Invoke(parameter);
        }
    }
}
