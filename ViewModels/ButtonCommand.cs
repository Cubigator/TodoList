using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TodoList.ViewModels
{
    public class ButtonCommand : ICommand
    {
        Action _action;
        Func<bool> _func;

        public ButtonCommand(Action action, Func<bool> func=null)
        {
            _action = action;
            _func = func;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _func == null || _func(); 
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
