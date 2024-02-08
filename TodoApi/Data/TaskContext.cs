using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TodoApi.Model;

namespace TodoApi.Data
{
    public class TaskContext : IdentityUserContext<ApplicationUser>
    {
        public DbSet<TaskItem> TaskItems { get; set; }

        public TaskContext(DbContextOptions<TaskContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TaskItem>().HasData(
                new TaskItem { Id = 1, Title = "Task 1", IsCompleted = false },
                new TaskItem { Id = 2, Title = "Task 2", IsCompleted = false },
                new TaskItem { Id = 3, Title = "Task 3", IsCompleted = false }
            );
        }

    }
}
