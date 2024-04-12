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
            Assert.That(taskManager.GetAllTasks(), Contains.Item(task));
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
            Assert.That(updatedTask.Title, Is.EqualTo(newTitle));
            Assert.That(updatedTask.Description, Is.EqualTo(newDescription));
            Assert.That(updatedTask.DueDate, Is.EqualTo(newDueDate));
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
            Assert.That(retrievedTask, Is.EqualTo(task));
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
            Assert.That(tasks, Contains.Item(task1));
            Assert.That(tasks, Contains.Item(task2));
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

            Assert.That(loadedTasks.Count, Is.EqualTo(originalTasks.Count));
            for (int i = 0; i < originalTasks.Count; i++)
            {
                Assert.That(loadedTasks[i].Title, Is.EqualTo(originalTasks[i].Title));
                Assert.That(loadedTasks[i].Description, Is.EqualTo(originalTasks[i].Description));
                Assert.That(loadedTasks[i].GetFormattedDueDate(), Is.EqualTo(originalTasks[i].GetFormattedDueDate()));
            }
        }

        [Test]
        public void AddTask_WhenNullTaskProvided_ShouldThrowArgumentNullException()
        {
            // Arrange
            Task? nullTask = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => taskManager.AddTask(nullTask!));
        }

        [Test]
        public void EditTask_WhenNullTaskProvided_ShouldThrowArgumentNullException()
        {
            // Arrange
            Task? nullTask = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => taskManager.EditTask(nullTask!, "New Title", "New Description", DateTime.Now));
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
            Assert.That(consoleOutput.ToString(), Is.EqualTo("Die Datei existiert nicht." + Environment.NewLine));
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
            Assert.That(sortedTasks, Is.EqualTo(sortedByDueDate));
        }
    }
}