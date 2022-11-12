using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
            UpdateCategories();
            _addedTask = new Exercise();
            Selected = new Exercise();
        }

        void UpdateCategories()
        {
            Todos = (from ex in _AllExercises where ex.IsDone == false select ex).ToList();
            Finished = (from ex in _AllExercises where ex.IsDone == true select ex).ToList();
        }

        static Exercise _addedTask;
        public Exercise Selected { get; set; }

        static AddExercise _addExercise = new AddExercise();
        public Exercise AddedTask
        {
            get { return _addedTask; }
            set
            {
                _addedTask = value;
                Notify("AddedTask");
            }
        }


        List<Exercise> _AllExercises;
        public List<Exercise> Todos { get; set; }
        public List<Exercise> Finished { get; set; }


        void Notify(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ButtonCommand AddTodosMenu
        {
            get
            {
                return new ButtonCommand(() =>
                    {
                        _addExercise.ShowDialog();
                        if(_addExercise.DialogResult == true)
                        {
                            _AllExercises.Add(AddedTask);
                            Todos.Add(AddedTask);
                            Todos = new List<Exercise>(Todos);
                            Notify("Todos");
                            Notify("Finished");
                        }
                        _addExercise = new AddExercise();
                        JsonHandler.Save(_AllExercises);
                    });
            }
        }

        public ButtonCommand CloseAddWindow
        {
            get
            {
                return new ButtonCommand(() =>
                {
                    _addExercise.DialogResult = true;
                });
            }
        }

        public ButtonCommand DeleteExercise
        {
            get
            {
                return new ButtonCommand(() =>
                {
                    _AllExercises.Remove(Selected);
                    UpdateCategories();
                    Notify("Todos");
                    Notify("Finished");
                    JsonHandler.Save(_AllExercises);
                });
            }
        }
    }
}
