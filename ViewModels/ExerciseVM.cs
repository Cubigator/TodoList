using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Model;

namespace TodoList.ViewModels
{
    public class ExerciseVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void Notify(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        string _header;
        string _mainText;

        public ExerciseVM()
        {
            Header = ExerciseINFO.Header;
            MainText= ExerciseINFO.MainText;
        }

        public string Header
        {
            get { return _header; }
            set
            {
                _header = value;
                Notify("Header");
            }
        }

        public string MainText
        {
            get { return _mainText; }
            set
            {
                _mainText = value;
                Notify("MainText");
            }
        }

    }
}
