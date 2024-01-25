using Microsoft.EntityFrameworkCore;
using TodoApi.Model;

namespace TodoApi.Data
{
    public class TaskContext: DbContext
    {
        public TaskContext(DbContextOptions<TaskContext> options)
            : base(options)
        {

        }

        public DbSet<TaskItem> TaskItems { get; set; }
    }
}
