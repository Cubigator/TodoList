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
            _AllExercises = JsonHandler.Load();
            Todos = (from ex in _AllExercises where ex.IsDone == false select ex).ToList();
            Finished = (from ex in _AllExercises where ex.IsDone == true select ex).ToList();
        }
        List<Exercise> _AllExercises;
        public List<Exercise> Todos { get; set; }
        public List<Exercise> Finished { get; set; }

        void Notify(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ButtonCommand AddTodos
        {
            get
            {
                return new ButtonCommand(() =>
                    {
                        AddExercise addExercise = new AddExercise();
                        addExercise.ShowDialog();
                    });
            }
        }

    }
}
