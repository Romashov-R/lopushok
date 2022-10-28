using System;
using System.Windows.Input;

namespace Vtitbid.Romashov.Bss.V2.Application.Commands
{
    public abstract class CommandBase : ICommand
    {
        protected Action<object> _command;
        protected Predicate<object>? _predicate;
        public event EventHandler? CanExecuteChanged;

        public virtual bool CanExecute(object? parameter)
        {
            if (_predicate == null)
                return true;

            return _predicate(parameter);
        }

        public virtual void Execute(object? parameter)
        {
            _command?.Invoke(parameter);
        }

        protected virtual void OnCanExecuteChanged(object sender, EventArgs e)
        {
            CanExecuteChanged?.Invoke(sender, e);
        }
    }
}