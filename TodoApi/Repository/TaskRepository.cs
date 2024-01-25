using TodoApi.Data;
using TodoApi.Model;

namespace TodoApi.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private TaskContext _db;

        public TaskRepository(TaskContext db)
        {
            _db = db;
        }

        public List<TaskItem> GetAllTasks()
        {
            return _db.TaskItems.ToList();
        }

        public TaskItem AddTask(string taskTitle)
        {
            var taskItem = new TaskItem() { Title = taskTitle, IsCompleted = false };
            _db.Add(taskItem);
            _db.SaveChanges();
            return taskItem;
        }

        public TaskItem? GetTask(int id)
        {
            return _db.TaskItems.FirstOrDefault(t => t.Id == id);
        }

        public TaskItem? UpdateTask(int id, TaskItemUpdatePayload updateData)
        {
            // check if task exists
            var task = GetTask(id);
            if (task == null)
            {
                return null;
            }

            bool hasUpdate = false;

            if(updateData.Title != null)
            {
                task.Title = (string)updateData.Title;
                hasUpdate = true;
            }

            if(updateData.IsCompleted != null)
            {
                task.IsCompleted = (bool)updateData.IsCompleted;
                hasUpdate = true;
            }

            if(!hasUpdate) throw new Exception("No task update data provided");

            _db.SaveChanges();

            return task;
        }

        public bool DeleteTask(int id)
        {
            var taskItem = GetTask(id);
            if (taskItem == null) return false;
            _db.TaskItems.Remove(taskItem);
            _db.SaveChanges();
            return true;
        }

    }
}
