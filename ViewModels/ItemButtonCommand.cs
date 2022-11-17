using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TodoList.ViewModels
{
    public class ItemButtonCommand : ICommand
    {
        Action _action;
        public object Id { get; set; }

        public ItemButtonCommand(Action action, object id)
        {
            _action = action;
            Id = id;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _action();
        }
    }
}