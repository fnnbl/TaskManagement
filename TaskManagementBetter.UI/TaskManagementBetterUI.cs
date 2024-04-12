using System.Globalization;

namespace TaskManagementBetter.UI
{
    public class Program
    {
        static TaskManager taskManager = new TaskManager();

        static void Main(string[] args)
        {
            DisplayMainMenu();
        }

        static void DisplayMainMenu()
        {
            Console.WriteLine("Aufgabenverwaltungssystem");
            Console.WriteLine("================================");
            Console.WriteLine("1. Aufgabe anlegen");
            Console.WriteLine("2. Aufgabe löschen");
            Console.WriteLine("3. Aufgabe bearbeiten");
            Console.WriteLine("4. Einzelne Aufgabe anzeigen");
            Console.WriteLine("5. Alle Aufgaben anzeigen");
            Console.WriteLine("6. Aufgaben speichern");
            Console.WriteLine("7. Aufgaben laden");
            Console.WriteLine("8. Programm beenden");
            Console.WriteLine("================================");
            Console.Write("Wählen Sie eine Option: ");
            ProcessUserChoice(Console.ReadLine());
        }

        static void ProcessUserChoice(string? choice)
        {
            if (choice != null)
            {
                switch (choice)
                {
                    case "1":
                        AddTask();
                        break;
                    case "2":
                        DeleteTask();
                        break;
                    case "3":
                        EditTask();
                        break;
                    case "4":
                        ShowSingleTask();
                        break;
                    case "5":
                        ShowAllTasks();
                        break;
                    case "6":
                        SaveTasks();
                        break;
                    case "7":
                        LoadTasks();
                        break;
                    case "8":
                        Console.WriteLine("Programm wird beendet...");
                        return;
                    default:
                        Console.WriteLine("Ungültige Eingabe. Bitte wählen Sie eine Option aus dem Menü.");
                        break;
                }
            }

            // Clear console for better readability and Display the main menu again
            Console.WriteLine("Drücken Sie eine beliebige Taste, um fortzufahren...");
            Console.ReadKey();
            Console.Clear();
            DisplayMainMenu();
        }

        static void AddTask()
        {
            Console.WriteLine("Neue Aufgabe anlegen");
            Console.Write("Titel: ");
            string? title = Console.ReadLine();
            Console.Write("Beschreibung: ");
            string? description = Console.ReadLine();
            Console.Write("Fälligkeitsdatum (TT.MM.JJJJ): ");
            if (DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dueDate))
            {
                taskManager.AddTask(new Task(title!, description!, dueDate));
                Console.WriteLine("Aufgabe wurde erfolgreich angelegt.");
            }
            else
            {
                Console.WriteLine("Ungültiges Datumsformat.");
            }
        }

        static void DeleteTask()
        {
            Console.Write("Titel der zu löschenden Aufgabe: ");
            string? title = Console.ReadLine();
            var task = taskManager.GetAllTasks().Find(t => t.Title == title);
            if (task != null)
            {
                taskManager.DeleteTask(task);
                Console.WriteLine("Aufgabe wurde erfolgreich gelöscht.");
            }
            else
            {
                Console.WriteLine("Aufgabe mit diesem Titel wurde nicht gefunden.");
            }
        }

        static void EditTask()
        {
            Console.Write("Titel der zu bearbeitenden Aufgabe: ");
            string? title = Console.ReadLine();
            var task = taskManager.GetAllTasks().Find(t => t.Title == title);
            if (task != null)
            {
                Console.Write("Neuer Titel: ");
                string? newTitle = Console.ReadLine();
                Console.Write("Neue Beschreibung: ");
                string? newDescription = Console.ReadLine();
                Console.Write("Neues Fälligkeitsdatum (TT-MM-JJJJ): ");
                if (DateTime.TryParseExact(Console.ReadLine(), "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime newDueDate))
                {
                    taskManager.EditTask(task, newTitle!, newDescription!, newDueDate);
                    Console.WriteLine("Aufgabe wurde erfolgreich bearbeitet.");
                }
                else
                {
                    Console.WriteLine("Ungültiges Datumsformat.");
                }
            }
            else
            {
                Console.WriteLine("Aufgabe mit diesem Titel wurde nicht gefunden.");
            }
        }

        static void ShowSingleTask()
        {
            Console.Write("Titel der anzuzeigenden Aufgabe: ");
            string? title = Console.ReadLine();
            var task = taskManager.GetAllTasks().Find(t => t.Title == title);
            if (task != null)
            {
                Console.WriteLine($"Titel: {task.Title}");
                Console.WriteLine($"Beschreibung: {task.Description}");
                Console.WriteLine($"Fälligkeitsdatum: {task.DueDate.ToString("dd-MM-yyyy")}");
            }
            else
            {
                Console.WriteLine("Aufgabe mit diesem Titel wurde nicht gefunden.");
            }
        }

        static void ShowAllTasks()
        {
            var tasks = taskManager.GetAllTasks();
            tasks.Sort((a, b) => a.DueDate.CompareTo(b.DueDate));

            Console.WriteLine("Alle Aufgaben:");
            foreach (var task in tasks)
            {
                Console.WriteLine($"- Titel: {task.Title}, Fälligkeitsdatum: {task.DueDate.ToString("dd-MM-yyyy")}");
            }
        }

        static void SaveTasks()
        {
            taskManager.SaveTasksToJson("tasks.json");
            Console.WriteLine("Aufgaben wurden erfolgreich gespeichert.");
        }

        static void LoadTasks()
        {
            taskManager.LoadTasksFromJson("tasks.json");
            Console.WriteLine("Aufgaben wurden erfolgreich geladen.");
        }
    }
}