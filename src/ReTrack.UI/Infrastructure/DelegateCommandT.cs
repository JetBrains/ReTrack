using System;
using System.Windows.Input;

namespace ReTrack.UI.Infrastructure
{
    public class DelegateCommand<TParameter> : ICommand
    {
        private readonly Action<TParameter> _action;
        private readonly Func<TParameter, bool> _canExecute;

        public DelegateCommand(Action<TParameter> action)
            : this(action, p => true)
        {
        }

        public DelegateCommand(Action<TParameter> action, Func<TParameter, bool> canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public void Execute(object parameter)
        {
            _action((TParameter)parameter);
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute((TParameter)parameter);
        }

#pragma warning disable 67
        public event EventHandler CanExecuteChanged;
#pragma warning restore 67
    }
}