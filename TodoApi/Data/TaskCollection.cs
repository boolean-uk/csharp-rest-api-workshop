using TodoApi.Model;

namespace TodoApi.Data
{
    public class TaskCollection
    {
        public List<TaskItem> Tasks { get; set; }
        private int _id = 0;

        public TaskCollection()
        {
            Tasks = new List<TaskItem>();
            AddTask("Task 1");
            AddTask("Task 2");
        }

        public TaskItem AddTask(string title)
        {
            _id++;
            var newItem = new TaskItem(_id, title, false);
            Tasks.Add(newItem);
            return newItem;
        }

        public TaskItem? GetTask(int id)
        {
            return Tasks.FirstOrDefault(t => t.Id == id);
        }
    }
}
