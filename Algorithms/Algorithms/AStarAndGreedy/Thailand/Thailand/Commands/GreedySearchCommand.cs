using System;
using System.Windows;
using System.Windows.Input;

namespace Thailand.Commands
{
    class GreedySearchCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Func<bool> _canExecute;
        private Action<int, int> _execution;

        public GreedySearchCommand(Func<bool> canExecute, Action<int, int> execution)
        {
            this._canExecute = canExecute;
            this._execution = execution;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute();
        }

        public void Execute(object parameter)
        {
            string obj = (string)parameter;
            string[] parsed = obj.Split(',');

            if (parsed.Length == 2)
            {
                _execution(Convert.ToInt32(parsed[0]), Convert.ToInt32(parsed[1]));
            }
        }
    }
}
