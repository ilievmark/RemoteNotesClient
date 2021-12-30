using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RemoteNotes.UI.ViewModel.Tool
{
    public class AsyncCommand : ICommand
    {
        private readonly Func<Task> _asyncFunc;
        private readonly Func<object, Task> _asyncParamFunc;
        private readonly Func<bool> _canExecuteFunc;
        private readonly Func<object, bool> _canExecuteParamFunc;

        private bool _oldCanExecute;

        #region ctors

        public AsyncCommand(Func<Task> asyncFunc)
        {
            if (asyncFunc == null)
                throw new ArgumentNullException(nameof(asyncFunc));

            _asyncFunc = asyncFunc;
        }

        public AsyncCommand(Func<object, Task> asyncFunc)
        {
            if (asyncFunc == null)
                throw new ArgumentNullException(nameof(asyncFunc));

            _asyncParamFunc = asyncFunc;
        }

        public AsyncCommand(Func<Task> asyncFunc, Func<bool> canExecute) : this(asyncFunc)
        {
            if (canExecute == null)
                throw new ArgumentNullException(nameof(canExecute));

            _canExecuteFunc = canExecute;
        }

        public AsyncCommand(Func<object, Task> asyncFunc, Func<object, bool> canExecute) : this(asyncFunc)
        {
            if (canExecute == null)
                throw new ArgumentNullException(nameof(canExecute));

            _canExecuteParamFunc = canExecute;
        }

        #endregion

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (_canExecuteFunc != null)
                return _canExecuteFunc();

            if (_canExecuteParamFunc != null)
                return _canExecuteParamFunc(parameter);

            return true;
        }

        public async void Execute(object parameter)
        {
            try
            {
                var canExecute = CanExecute(parameter);
                ChangeCanExecute(canExecute);
                if (!canExecute) return;

                if (_asyncFunc != null)
                    await _asyncFunc();

                if (_asyncParamFunc != null)
                    await _asyncParamFunc(parameter);
            }
            catch (Exception e)
            {
                Debugger.Break();
                Debug.WriteLine("Unhandled exception");
                throw;
            }
        }

        public void RaiseExecuteChanged(object parameter)
        {
            ChangeCanExecute(CanExecute(parameter));
        }

        private void ChangeCanExecute(bool newCanExecute)
        {
            if (_oldCanExecute != newCanExecute)
                CanExecuteChanged?.Invoke(this, new EventArgs());
            _oldCanExecute = newCanExecute;
        }
    }
}