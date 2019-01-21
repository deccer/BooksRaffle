using System;
using System.Windows.Input;

namespace BookmarksRaffle.Commands
{
    public class DelegateCommand : ICommand
    {
        private readonly Action _action;
        private readonly Func<bool> _canExecute;

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public DelegateCommand(Action action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public DelegateCommand(Action action, Func<bool> canExecuteEvaluator) : this(action)
        {
            _canExecute = canExecuteEvaluator ?? throw new ArgumentNullException(nameof(canExecuteEvaluator));
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke() ?? true;
        }

        public void Execute(object parameter)
        {
            _action();
        }
    }

    public class DelegateCommand<T> : ICommand
    {
        private readonly Action<T> _action;
        private readonly Predicate<T> _canExecute;

        public DelegateCommand(Action<T> action, Predicate<T> canExecute = null)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public void Execute(object parameter)
        {
            _action((T)parameter);
        }
    }
}