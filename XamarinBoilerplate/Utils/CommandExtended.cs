using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace XamarinBoilerplate.Utils
{
    public class CommandExtended : ICommand
    {
        private bool isBusy;
        private readonly Func<bool> canExecute;
        private EventHandler CommandManager;
        private Func<Task> ExecuteTaskCommand;
        private Func<object, Task> ExecuteAction;
        private Func<object, Task> ExecuteSelectedItemCommand;

        public CommandExtended(Func<Task> executeActionTaskCommand)
        {
            ExecuteTaskCommand = executeActionTaskCommand;
        }

        public CommandExtended(Func<Task> executeActionTaskCommand, Func<bool> canExecute)
        {
            this.canExecute = canExecute;
            ExecuteTaskCommand = executeActionTaskCommand;
        }

        public CommandExtended(Func<object, Task> executeActionSelectedItemCommand)
        {
            ExecuteTaskCommand = null;
            ExecuteSelectedItemCommand = executeActionSelectedItemCommand;
        }

        event EventHandler ICommand.CanExecuteChanged
        {
            add
            {
                CommandManager += value;
            }

            remove
            {
                if (CommandManager != null)
                {
                    CommandManager -= value;
                }
            }
        }

        public void ChangeCanExecute()
        {
            CommandManager?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            return (canExecute?.Invoke() ?? true) && !isBusy;
        }

        private Func<object, T> ConvertAction<T>(Func<T> action)
        {
            if (action == null) return null;

            return _ => action();
        }

        private Func<object, Task> ConvertActionWithParameters(Func<object, Task> action, object parameter)
        {
            if (action == null) return null;

            return _ => action(parameter);
        }

        public async void Execute(object parameter)
        {
            if (isBusy)
            {
                return;
            }

            isBusy = true;

            if (ExecuteTaskCommand != null)
            {
                ExecuteAction = ConvertAction(ExecuteTaskCommand);
            }
            else
            {
                ExecuteAction = ConvertActionWithParameters(ExecuteSelectedItemCommand, parameter);
            }

            await ExecuteAction.Invoke(parameter);

            isBusy = false;
        }
    }
}
