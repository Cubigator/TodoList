using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.ViewModels;

namespace TodoList.Model
{
    public class ExerciseEC : Exercise
    {
        public ButtonCommand Edit { get; set; }
        public ButtonCommand Delete { get; set; }
    }
}
