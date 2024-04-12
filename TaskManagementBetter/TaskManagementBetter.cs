using Newtonsoft.Json;

namespace TaskManagementBetter
{
    public class Task
    {
        // Eigenschaften (müssen serialisierbar sein)
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }

        // Konstruktor
        public Task(string title, string description, DateTime dueDate)
        {
            Title = title;
            Description = description;
            DueDate = dueDate;
        }

        // Konstruktor für die Serialisierung
        [JsonConstructor]
        public Task(string title, string description, string dueDate)
        {
            Title = title;
            Description = description;
            DueDate = DateTime.ParseExact(dueDate, "dd-MM-yyyy", null);
        }

        // Methode zum Konvertieren des Datums in das gewünschte Format für die Speicherung
        public string GetFormattedDueDate()
        {
            return DueDate.ToString("dd-MM-yyyy");
        }
    }

    public class TaskManager
    {
        private List<Task> tasks = new List<Task>();

        public TaskManager()
        {
        }

        public void AddTask(Task task)
        {
            if (task != null)
            {
                tasks.Add(task);
            }
            else
            {
                throw new ArgumentNullException(nameof(task), "Die Aufgabe darf nicht null sein.");
            }
        }

        public void DeleteTask(Task task)
        {
            if (task != null)
            {
                tasks.Remove(task);
            }
            else
            {
                throw new ArgumentNullException(nameof(task), "Die Aufgabe darf nicht null sein.");
            }
        }

        public void EditTask(Task task, string newTitle, string newDescription, DateTime newDueDate)
        {
            if (task != null)
            {
                task.Title = newTitle;
                task.Description = newDescription;
                task.DueDate = newDueDate;
            }
            else
            {
                throw new ArgumentNullException(nameof(task), "Die Aufgabe darf nicht null sein.");
            }
        }

        public List<Task> GetAllTasks()
        {
            return new List<Task>(tasks);
        }

        public void SaveTasksToJson(string filePath)
        {
            string json = JsonConvert.SerializeObject(tasks, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public void LoadTasksFromJson(string filePath)
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                if (!string.IsNullOrEmpty(json))
                {
                    List<Task>? loadedTasks = JsonConvert.DeserializeObject<List<Task>>(json);

                    if (loadedTasks != null)
                    {
                        tasks = loadedTasks;
                    }
                    else
                    {
                        Console.WriteLine("Die Aufgaben konnten nicht geladen werden.");
                    }
                }
                else
                {
                    Console.WriteLine("Die Datei ist leer.");
                }
            }
            else
            {
                Console.WriteLine("Die Datei existiert nicht.");
            }
        }
    }
}