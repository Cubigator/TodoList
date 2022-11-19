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
            TodosVisibility = Visibility.Hidden;
            Find = "";
        }

        static Exercise _addedTask;
        static AddExercise _addExercise = new AddExercise();

        string _find;

        Visibility _todosVisibility;

        List<Exercise> _AllExercises;
        public List<ExerciseEC> Todos { get; set; }
        public List<ExerciseEC> Finished { get; set; }
        public List<Exercise> SearchedTodos { get; set; }


        void Notify(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        void UpdateCategories(List<Exercise> source)
        {
            Todos = new List<ExerciseEC>();
            for (int i = 0; i < source.Count; i++)
            {
                if (source[i].IsDone == false)
                {
                    ExerciseEC exerciseEC = new ExerciseEC()
                    {
                        Index = i,
                        Header = source[i].Header,
                        MainText = source[i].MainText,
                        IsDone = source[i].IsDone,
                    };
                    exerciseEC.Delete = new ItemButtonCommand(() =>
                    {
                        _AllExercises.RemoveAt(exerciseEC.Index);
                        SearchedTodos = (from ex in _AllExercises where ex.Header.ToLower().IndexOf(_find.ToLower()) != -1 select ex).ToList();
                        UpdateCategories(SearchedTodos);
                        Notify("Todos");
                        Notify("Finished");
                        JsonHandler.Save(_AllExercises);
                    });
                    exerciseEC.Edit = new ItemButtonCommand(() =>
                    {
                        ExerciseINFO.Header = SearchedTodos[exerciseEC.Index].Header;
                        ExerciseINFO.MainText = SearchedTodos[exerciseEC.Index].MainText;
                        ExerciseWindow exerciseWindow = new ExerciseWindow();
                        exerciseWindow.ShowDialog();
                        SearchedTodos[exerciseEC.Index].Header = ExerciseINFO.Header;
                        SearchedTodos[exerciseEC.Index].MainText = ExerciseINFO.MainText;
                        UpdateCategories(SearchedTodos);
                        JsonHandler.Save(_AllExercises);
                    });
                    Todos.Add(exerciseEC);
                }
            }

            Finished = new List<ExerciseEC>();
            for (int i = 0; i < source.Count; i++)
            {
                if (source[i].IsDone == true)
                {
                    ExerciseEC exerciseEC = new ExerciseEC()
                    {
                        Index = i,
                        Header = source[i].Header,
                        MainText = source[i].MainText,
                        IsDone = source[i].IsDone,
                    };
                    exerciseEC.Delete = new ItemButtonCommand(() =>
                    {
                        _AllExercises.RemoveAt(exerciseEC.Index);
                        SearchedTodos = (from ex in _AllExercises where ex.Header.ToLower().IndexOf(_find.ToLower()) != -1 select ex).ToList();
                        UpdateCategories(SearchedTodos);
                        Notify("Todos");
                        Notify("Finished");
                        JsonHandler.Save(_AllExercises);
                    });
                    exerciseEC.Edit = new ItemButtonCommand(() =>
                    {
                        ExerciseINFO.Header = SearchedTodos[exerciseEC.Index].Header;
                        ExerciseINFO.MainText = SearchedTodos[exerciseEC.Index].MainText;
                        ExerciseWindow exerciseWindow = new ExerciseWindow();
                        exerciseWindow.ShowDialog();
                        SearchedTodos[exerciseEC.Index].Header = ExerciseINFO.Header;
                        SearchedTodos[exerciseEC.Index].MainText = ExerciseINFO.MainText;
                        UpdateCategories(SearchedTodos);
                        JsonHandler.Save(_AllExercises);
                    });
                    Finished.Add(exerciseEC);
                }
            }
            Notify("Todos");
            Notify("Finished");
            Notify("SearchedTodos");
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
    }
}
