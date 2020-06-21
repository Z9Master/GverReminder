using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace ToDoGver.OtherWindows.EventsWindows
{
    class FastTask
    {
        // Unique id for indentify one time tasks
        private int id = 0;

        // Path to the data task file
        string filePath = @"E:\Project\C#\GverReminder\ToDoGver\GverReminder\ToDoGver\Data\OneTimeEvent.txt";
        
        // List to store task to save to the file
        public List<OneTimeEvent> ListOneTimeEvents = new List<OneTimeEvent>();

        public 
        
        // The list to load file text to parse to the ListOneTimeEvents
        List<string> lines;
        
        // Simple load file from filePath
        public void LoadFile()
        {
            try
            {
                lines = File.ReadAllLines(filePath).ToList();
                foreach (var line in lines)
                {
                    string[] entries = line.Split(',');
                    OneTimeEvent oneTask = new OneTimeEvent();

                    oneTask.nameEvent = entries[0];
                    oneTask.IdEvent = Int32.Parse(entries[1]);
                    ListOneTimeEvents.Add(oneTask);
                }
            }
            catch
            {
                MessageBox.Show("Can´t load file");
            }
        }

        // git test

        // Simple delete task by IdEvent
        public void DeleteEvent(int index)
        {
            List<string> output = new List<string>();
            ListOneTimeEvents.Remove(new OneTimeEvent() { IdEvent = index });
            foreach (var task in ListOneTimeEvents)
            {
                output.Add($"{task.nameEvent},{task.IdEvent}");
            }
            File.WriteAllLines(filePath, output);
        }

        // Simple add task with name and unique ID
        public void AddItems(string name)
        {
            if(ListOneTimeEvents.Count >= 1)
            {
                id = ListOneTimeEvents[ListOneTimeEvents.Count() - 1].IdEvent + 1;
            }
            else
            {
                id = 0;
            }
            List<string> output = new List<string>();
            ListOneTimeEvents.Add(new OneTimeEvent() { nameEvent = name, IdEvent = id });
            foreach (var task in ListOneTimeEvents)
            {
                output.Add($"{task.nameEvent},{task.IdEvent}");
            }
            File.WriteAllLines(filePath, output);
        }
        
    }

    // Class that define variables for ListOneTimeEvents
    class OneTimeEvent : IEquatable<OneTimeEvent>
    {
        public string nameEvent { get; set; }
        public int IdEvent { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            OneTimeEvent objAsPart = obj as OneTimeEvent;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }

        public override int GetHashCode()
        {
            return IdEvent;
        }

        public bool Equals(OneTimeEvent other)
        {
            if (other == null) return false;
            return (this.IdEvent.Equals(other.IdEvent));
        }
    }
}
