using TodoApi.Model;

namespace TodoApi.Repository
{
    public interface IRepository
    {
        public List<TaskItem> GetAllTasks();

        public TaskItem AddTask(string taskTitle);

        public TaskItem? GetTask(int id);

        public TaskItem? UpdateTask(int id, TaskItemUpdatePayload updateData);

        public bool DeleteTask(int id);

        public ApplicationUser? GetUser(string email);
    }
}
