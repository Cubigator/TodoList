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
            UpdateCategories(_AllExercises);
            _addedTask = new Exercise();
            Selected = new Exercise();
            TodosVisibility = Visibility.Hidden;
        }


        static Exercise _addedTask;
        static AddExercise _addExercise = new AddExercise();
        public Exercise Selected { get; set; }
        string _find;

        Visibility _todosVisibility;

        List<Exercise> _AllExercises;
        public List<Exercise> Todos { get; set; }
        public List<Exercise> Finished { get; set; }
        public List<Exercise> SearchedTodos { get; set; }


        void Notify(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        void UpdateCategories(List<Exercise> source)
        {
            Todos = (from ex in source where ex.IsDone == false select ex).ToList();
            Finished = (from ex in source where ex.IsDone == true select ex).ToList();
            Notify("Todos");
            Notify("Finished");
        }

        public string Find
        {
            get { return _find; }
            set
            {
                _find = value;
                Notify("Find");
                SearchedTodos = (from ex in _AllExercises where ex.Header.ToLower().IndexOf(_find.ToLower()) != -1 select ex).ToList();
                UpdateCategories(SearchedTodos);
            }
        }

        public Visibility TodosVisibility
        {
            get { return _todosVisibility; }
            set
            {
                _todosVisibility = value;
                Notify("TodosVisibility");
            }
        }

        public Exercise AddedTask
        {
            get { return _addedTask; }
            set
            {
                _addedTask = value;
                Notify("AddedTask");
            }
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
                            SearchedTodos = (from ex in _AllExercises where ex.Header.ToLower().IndexOf(_find.ToLower()) != -1 select ex).ToList();
                            UpdateCategories(SearchedTodos);
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

        public ButtonCommand ShowTodos
        {
            get
            {
                return new ButtonCommand(() =>
                {
                    TodosVisibility = Visibility.Visible;
                });
            }
        }

        public ButtonCommand HideTodos
        {
            get
            {
                return new ButtonCommand(() =>
                {
                    TodosVisibility = Visibility.Hidden;
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
                    UpdateCategories(SearchedTodos);
                    Notify("Todos");
                    Notify("Finished");
                    JsonHandler.Save(_AllExercises);
                });
            }
        }
    }
}
