using TodoApi.Model;

namespace TodoApi.Data
{
    public class TaskCollection
    {
        public List<TaskItem> Tasks { get; init; }
        private int _nextTaskId;

        public TaskCollection()
        {
            Tasks = new List<TaskItem>();
            _nextTaskId = 1;
            seedData();            
        }

        public TaskItem? GetTask(int id)
        {
            return Tasks.FirstOrDefault(t => t.Id == id);
        }

        public TaskItem AddTask(string title)
        {
            var newItem = new TaskItem(_nextTaskId, title, false);
            _nextTaskId++;
            Tasks.Add(newItem);
            return newItem;
        }

        public bool DeleteTask(int id)
        {
            var task = GetTask(id);
            if (task == null)
            {
                return false;
            }
            Tasks.Remove(task);
            return true;
        }

        private void seedData()
        {
            // create some initial data
            AddTask("Task 1");
            AddTask("Task 2");
        }
    }
}
