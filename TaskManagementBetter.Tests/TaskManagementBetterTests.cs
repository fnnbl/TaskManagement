namespace TaskManagementBetter.Tests
{
    public class TaskManagerTests
    {
        private TaskManager taskManager;
        private string testFilePath = "test_tasks.json";

        [SetUp]
        public void Setup()
        {
            taskManager = new TaskManager();
        }

        [Test]
        public void AddTask_WhenValidTaskProvided_ShouldAddTaskToList()
        {
            // Arrange
            var task = new Task("Test Task", "Test Description", DateTime.Now);

            // Act
            taskManager.AddTask(task);

            // Assert
            Assert.Contains(task, taskManager.GetAllTasks());
        }

        [Test]
        public void DeleteTask_WhenExistingTaskProvided_ShouldRemoveTaskFromList()
        {
            // Arrange
            var task = new Task("Test Task", "Test Description", DateTime.Now);
            taskManager.AddTask(task);

            // Act
            taskManager.DeleteTask(task);

            // Assert
            Assert.IsFalse(taskManager.GetAllTasks().Contains(task));
        }

        [Test]
        public void EditTask_WhenExistingTaskProvided_ShouldUpdateTaskDetails()
        {
            // Arrange
            var originalTask = new Task("Test Task", "Test Description", DateTime.Now);
            taskManager.AddTask(originalTask);

            // Act
            var newTitle = "Updated Title";
            var newDescription = "Updated Description";
            var newDueDate = DateTime.Now.AddDays(1);
            taskManager.EditTask(originalTask, newTitle, newDescription, newDueDate);

            // Assert
            var updatedTask = taskManager.GetAllTasks().Find(t => t.Title == newTitle);
            Assert.NotNull(updatedTask);
            Assert.AreEqual(newTitle, updatedTask.Title);
            Assert.AreEqual(newDescription, updatedTask.Description);
            Assert.AreEqual(newDueDate, updatedTask.DueDate);
        }

        [Test]
        public void GetTask_WhenExistingTaskTitleProvided_ShouldReturnTask()
        {
            // Arrange
            var task = new Task("Test Task", "Test Description", DateTime.Now);
            taskManager.AddTask(task);

            // Act
            var retrievedTask = taskManager.GetAllTasks().Find(t => t.Title == task.Title);

            // Assert
            Assert.AreEqual(task, retrievedTask);
        }

        [Test]
        public void GetAllTasks_ShouldReturnAllTasksInList()
        {
            // Arrange
            var task1 = new Task("Task 1", "Description 1", DateTime.Now);
            var task2 = new Task("Task 2", "Description 2", DateTime.Now.AddDays(1));
            taskManager.AddTask(task1);
            taskManager.AddTask(task2);

            // Act
            var tasks = taskManager.GetAllTasks();

            // Assert
            Assert.Contains(task1, tasks);
            Assert.Contains(task2, tasks);
        }

        [Test]
        public void SaveAndLoadTasks_ShouldSaveAndLoadTasks()
        {
            // Arrange
            TaskManager originalTaskManager = new TaskManager();
            Task task1 = new Task("Test Title 1", "Test Description 1", new DateTime(2024, 4, 15));
            Task task2 = new Task("Test Title 2", "Test Description 2", new DateTime(2024, 4, 16));
            originalTaskManager.AddTask(task1);
            originalTaskManager.AddTask(task2);

            // Act
            originalTaskManager.SaveTasksToJson(testFilePath);
            TaskManager loadedTaskManager = new TaskManager();
            loadedTaskManager.LoadTasksFromJson(testFilePath);

            // Assert
            List<Task> originalTasks = originalTaskManager.GetAllTasks();
            List<Task> loadedTasks = loadedTaskManager.GetAllTasks();

            Assert.AreEqual(originalTasks.Count, loadedTasks.Count);
            for (int i = 0; i < originalTasks.Count; i++)
            {
                Assert.AreEqual(originalTasks[i].Title, loadedTasks[i].Title);
                Assert.AreEqual(originalTasks[i].Description, loadedTasks[i].Description);
                Assert.AreEqual(originalTasks[i].GetFormattedDueDate(), loadedTasks[i].GetFormattedDueDate());
            }
        }

        [Test]
        public void AddTask_WhenNullTaskProvided_ShouldThrowArgumentNullException()
        {
            // Arrange
            Task nullTask = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => taskManager.AddTask(nullTask));
        }

        [Test]
        public void EditTask_WhenNullTaskProvided_ShouldThrowArgumentNullException()
        {
            // Arrange
            Task nullTask = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => taskManager.EditTask(nullTask, "New Title", "New Description", DateTime.Now));
        }

        [Test]
        public void LoadTasksFromNonexistentFile_ShouldOutputFileNotFoundError()
        {
            // Arrange
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            taskManager.LoadTasksFromJson("nonexistent_file.json");

            // Assert
            Assert.AreEqual("Die Datei existiert nicht." + Environment.NewLine, consoleOutput.ToString());
        }

        [Test]
        public void GetAllTasks_ShouldReturnTasksSortedByDueDate()
        {
            // Arrange
            var task1 = new Task("Task 1", "Description 1", new DateTime(2025, 4, 15));
            var task2 = new Task("Task 2", "Description 2", new DateTime(2024, 4, 14));
            var task3 = new Task("Task 3", "Description 3", new DateTime(2024, 4, 16));
            taskManager.AddTask(task1);
            taskManager.AddTask(task2);
            taskManager.AddTask(task3);

            // Act
            var sortedTasks = taskManager.GetAllTasks();

            // Assert
            var sortedByDueDate = sortedTasks.OrderBy(t => t.DueDate).ToList();
            for (int i = 0; i < sortedTasks.Count; i++)
            {
                Assert.AreEqual(sortedByDueDate[i], sortedTasks[i]);
            }
        }
    }
}