using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Model;

namespace TodoList.Savings
{
    static public class JsonHandler
    {
        static public void Save(List<Exercise> exercises)
        {
            string json = JsonConvert.SerializeObject(exercises);
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

            FileStream file = new FileStream(path + "\\Savings\\Save.json", FileMode.Truncate);
            StreamWriter writer = new StreamWriter(file);

            writer.Write(json);
            writer.Flush();

            writer.Close();
            file.Close();
        }
        static public List<Exercise> Load()
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

            FileStream file = new FileStream(path + "\\Savings\\Save.json", FileMode.Open);
            StreamReader reader = new StreamReader(file);

            string json = reader.ReadToEnd();

            List<Exercise> exercises = JsonConvert.DeserializeObject(json, typeof(List<Exercise>)) as List<Exercise>;
            file.Close();
            reader.Close();
            return exercises;
        }
    }
}
