using Microsoft.AspNetCore.Mvc;
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
            taskGroup.MapGet("/{id}", GetTask);
            taskGroup.MapPost("/", CreateTask);
            taskGroup.MapPut("/{id}", UpdateTask);
            taskGroup.MapDelete("/{id}", DeleteTask);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static IResult GetAllTasks(ITaskRepository tasks) {
            return TypedResults.Ok(tasks.GetAllTasks());    
        }
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult GetTask(ITaskRepository tasks, int id)
        {
            TaskItem? item = tasks.GetTask(id);
            if (item == null)
            {
                return TypedResults.NotFound($"Task with id {id} not found.");
            }
            return TypedResults.Ok(item);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static IResult CreateTask(ITaskRepository tasks, TaskItemPostPayload newTaskData)
        {
            if (newTaskData.Title == null) return TypedResults.BadRequest("Title is required.");
            TaskItem item = tasks.AddTask(newTaskData.Title);
            return TypedResults.Created($"/tasks{item.Id}", item);
        }
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static IResult UpdateTask(ITaskRepository tasks, int id, TaskItemUpdatePayload updateData)
        {
            try
            {
                TaskItem? item = tasks.UpdateTask(id, updateData);
                if (item == null)
                {
                    return TypedResults.NotFound($"Task with id {id} not found.");
                }
                return TypedResults.Ok(item);
            }
            catch (Exception e)
            {
                return TypedResults.BadRequest(e.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult DeleteTask(ITaskRepository tasks, int id)
        {
            if(!tasks.DeleteTask(id))
            {
                return Results.NotFound("Failed to delete task.");
            }
            return Results.NoContent();
        }
    }
}
