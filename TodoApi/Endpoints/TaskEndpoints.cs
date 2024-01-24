using TodoApi.Model;
using TodoApi.Repository;

namespace TodoApi.Endpoints
{
    public static class TaskEndpoints
    {
        public static void ConfigureTaskEndpoints(this WebApplication app)
        {
            var taskGroup = app.MapGroup("tasks");
            taskGroup.MapGet("/", GetAllTasks);
            taskGroup.MapPost("/", CreateTask);
            taskGroup.MapPut("/{id}", UpdateTask);
        }
        public static IResult GetAllTasks(ITaskRepository tasks) {
            return TypedResults.Ok(tasks.GetAllTasks());    
        }
        public static IResult CreateTask(ITaskRepository tasks, TaskItemPostPayload newTaskData)
        {
            TaskItem item = tasks.AddTask(newTaskData.Title);
            return TypedResults.Created($"/tasks{item.Id}", item);
        }
        public static IResult UpdateTask(ITaskRepository tasks, int id, TaskItemUpdatePayload updateData)
        {
            try
            {
                TaskItem? item = tasks.UpdateTask(id, updateData);
                if (item == null)
                {
                    return Results.NotFound($"Task with id {id} not found.");
                }
                return Results.Ok(item);
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        }
    }
}
