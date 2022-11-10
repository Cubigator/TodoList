using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Model;
using TodoList.Savings;

namespace TodoList.ViewModels
{
    public class MainVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public MainVM()
        {
            DoneExercises = JsonHandler.Load();
        }

        public List<Exercise> DoneExercises { get; set; }

        void Notify(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


    }
}
