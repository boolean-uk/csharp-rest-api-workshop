using Microsoft.EntityFrameworkCore;
using TodoApi.Model;
using System.Diagnostics;

namespace TodoApi.Data
{
    public class TaskContext : DbContext
    {
        // private string _connectionString;
        public TaskContext(DbContextOptions<TaskContext> options) : base(options)
        {
            // loading the DefaultConnectionString value from the appsettings.json
            // var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            // _connectionString = configuration.GetValue<string>("ConnectionStrings:ConnectionString")!;
            // this.Database.EnsureCreated();
        }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     // TODO remove this and show this
        //     //optionsBuilder.UseInMemoryDatabase(databaseName: "Database");
        //     // use postgresql db
        //     optionsBuilder.UseNpgsql(_connectionString);
        // }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskItem>().HasData(
                new TaskItem { Id = 1, Title = "Task 1", IsCompleted = false },
                new TaskItem { Id = 2, Title = "Task 2", IsCompleted = false },
                new TaskItem { Id = 3, Title = "Task 3", IsCompleted = false }
            );
        }

        public DbSet<TaskItem> TaskItems { get; set; }
    }
}
