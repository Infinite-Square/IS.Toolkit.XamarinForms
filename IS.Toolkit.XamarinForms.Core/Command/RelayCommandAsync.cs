using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IS.Toolkit.XamarinForms.Core
{
    public class RelayCommandAsync<T> : ICommand
    {
        #region Fields
        private readonly Func<T, Task> _executedMethod;
        private readonly Func<T, bool> _canExecuteMethod;
        private bool _taskRunning;
        #endregion

        #region Events
        public event EventHandler CanExecuteChanged;
        #endregion

        public RelayCommandAsync(Func<T, Task> execute)
            : this(execute, null)
        {
        }

        public RelayCommandAsync(Func<T, Task> execute, Func<T, bool> canExecute)
        {
            _executedMethod = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecuteMethod = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (_taskRunning)
            {
                return false;
            }

            return _canExecuteMethod == null || _canExecuteMethod((T)parameter);
        }

        public async void Execute(object parameter)
        {
            try
            {
                _taskRunning = true;
                RaiseCanExecuteChanged();
                await _executedMethod((T)parameter);
            }
            finally
            {
                _taskRunning = false;
                RaiseCanExecuteChanged();
            }
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public class RelayCommandAsync : ICommand
    {
        #region Fields
        private readonly Func<Task> _executedMethod;
        private readonly Func<bool> _canExecuteMethod;
        private bool _taskRunning;
        #endregion

        #region Events
        public event EventHandler CanExecuteChanged;
        #endregion

        public RelayCommandAsync(Func<Task> execute)
            : this(execute, null)
        {
        }

        public RelayCommandAsync(Func<Task> execute, Func<bool> canExecute)
        {
            _executedMethod = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecuteMethod = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (_taskRunning)
            {
                return false;
            }

            return _canExecuteMethod == null || _canExecuteMethod();
        }

        public async void Execute(object parameter)
        {
            try
            {
                _taskRunning = true;
                RaiseCanExecuteChanged();
                await _executedMethod();
            }
            finally
            {
                _taskRunning = false;
                RaiseCanExecuteChanged();
            }
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
