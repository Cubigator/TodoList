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
        public int Index { get; set; }
        public ItemButtonCommand Edit { get; set; }
        public ItemButtonCommand Delete { get; set; }
        public ItemButtonCommand Open { get; set; }
    }
}
