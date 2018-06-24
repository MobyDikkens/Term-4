using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace Thailand.Commands
{
    public class SetCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action<string> _execution;

        public SetCommand(Action<string> execution)
        {
            _execution = execution;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            RadioButton rb = (RadioButton)parameter; 
            _execution((string)rb.Tag);
        }
    }
}
