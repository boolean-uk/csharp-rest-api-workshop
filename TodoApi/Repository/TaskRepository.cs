using TodoApi.Data;
using TodoApi.Model;

namespace TodoApi.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private TaskCollection _tasks;

        public TaskRepository(TaskCollection tasks)
        {
            _tasks = tasks;
        }

        public List<TaskItem> GetAllTasks()
        {
            return _tasks.Tasks;
        }

        public TaskItem AddTask(string taskTitle)
        {
            return _tasks.AddTask(taskTitle);
        }

        public TaskItem? GetTask(int id)
        {
            return _tasks.GetTask(id);
        }

        public TaskItem? UpdateTask(int id, TaskItemUpdatePayload updateData)
        {
            // check if task exists
            var task = _tasks.GetTask(id);
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

            if(!hasUpdate) throw new Exception("No update data provided");

            return task;
        }

    }
}
