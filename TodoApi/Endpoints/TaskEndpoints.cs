using Microsoft.AspNetCore.Authorization;
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
        [Authorize()]
        public static IResult GetAllTasks(IRepository repo)
        {
            return TypedResults.Ok(repo.GetAllTasks());
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult GetTask(IRepository repo, int id)
        {
            TaskItem? item = repo.GetTask(id);
            if (item == null)
            {
                return TypedResults.NotFound($"Task with id {id} not found.");
            }
            return TypedResults.Ok(item);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static IResult CreateTask(IRepository repo, TaskItemPostPayload newTaskData)
        {
            if (newTaskData.Title == null) return TypedResults.BadRequest("Title is required.");
            TaskItem item = repo.AddTask(newTaskData.Title);
            return TypedResults.Created($"/tasks{item.Id}", item);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static IResult UpdateTask(IRepository repo, int id, TaskItemUpdatePayload updateData)
        {
            try
            {
                TaskItem? item = repo.UpdateTask(id, updateData);
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
        public static IResult DeleteTask(IRepository repo, int id)
        {
            if (!repo.DeleteTask(id))
            {
                return Results.NotFound("Failed to delete task.");
            }
            return Results.NoContent();
        }
    }
}
