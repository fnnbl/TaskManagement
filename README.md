# Task Management App

## Overview
This is a simple console-based Task Management App written in C#. The application allows users to manage their tasks effectively by adding, deleting, editing, viewing, saving, and loading tasks. Tasks are stored in JSON format for persistence.

## Features
- **Add Task**: Create a new task with a title, description, and due date.
- **Delete Task**: Remove an existing task based on its title.
- **Edit Task**: Modify the details of an existing task.
- **Show Single Task**: Display details of a specific task.
- **Show All Tasks**: Display all tasks sorted by their due dates.
- **Save Tasks**: Save all tasks to a JSON file.
- **Load Tasks**: Load tasks from a JSON file.
- **Exit**: Exit the application.

## Installation
1. **Clone the repository**:
    ```bash
    git clone https://github.com/your-repo/task-management-app.git
    ```
2. **Navigate to the project directory**:
    ```bash
    cd task-management-app
    ```
3. **Build the project**:
    ```bash
    dotnet build
    ```
4. **Run the project**:
    ```bash
    dotnet run
    ```

## Usage
Upon running the application, the main menu will be displayed with the following options:

1. Aufgabe anlegen (Add Task)
2. Aufgabe löschen (Delete Task)
3. Aufgabe bearbeiten (Edit Task)
4. Einzelne Aufgabe anzeigen (Show Single Task)
5. Alle Aufgaben anzeigen (Show All Tasks)
6. Aufgaben speichern (Save Tasks)
7. Aufgaben laden (Load Tasks)
8. Programm beenden (Exit)

### Main Menu Navigation
- Enter the corresponding number to select an option.
- Follow the prompts to add, delete, edit, or view tasks.
- Press any key to return to the main menu after completing an action.

## Code Structure

### `Program.cs`
This is the main entry point of the application. It handles user interactions and displays the main menu.

```csharp
namespace TaskManagementBetter.UI
{
    public class Program
    {
        static TaskManager taskManager = new TaskManager();
        static void Main(string[] args)
        {
            DisplayMainMenu();
        }

        static void DisplayMainMenu() { ... }
        static void ProcessUserChoice(string? choice) { ... }
        static void AddTask() { ... }
        static void DeleteTask() { ... }
        static void EditTask() { ... }
        static void ShowSingleTask() { ... }
        static void ShowAllTasks() { ... }
        static void SaveTasks() { ... }
        static void LoadTasks() { ... }
    }
}
