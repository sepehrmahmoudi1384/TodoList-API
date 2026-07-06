using TodoList_API.Data.Context;
using TodoList_API.DTOs;
using TodoList_API.Models;
using TodoList_API.Services;

namespace TodoList_API.Endpoints;

public static class TodoEndpoints
{
    private const string GetByIdEndpoint = "GetTodoById";

    public static void MapTodoEndpoints(
        this WebApplication app
    )
    {
        var todoGroup = app.MapGroup("/todos");

        // GET /todos
        todoGroup.MapGet(
            "/",
            async (ITodoService todoService) =>
            {
                var todos = await todoService.GetAll();
                return Results.Ok(todos);
            }
        );

        // GET /todos/1
        todoGroup.MapGet(
            "/{id}",
            async (int id, ITodoService todoService) =>
            {
                var todo = await todoService.GetById(id);

                if (todo == null)
                    return Results.NotFound();

                return Results.Ok(todo);
            }
        )
        .WithName(GetByIdEndpoint);

        // POST /todos
        todoGroup.MapPost(
            "/",
            async (CreateTodoDTO todoDTO, ITodoService todoService) =>
            {
                if (!todoDTO.IsValidDueDate())
                    return Results.BadRequest(
                        $"This Due Time '{todoDTO.DueDate}' is for the past!"
                    );

                if (todoDTO.Title == null)
                    return Results.BadRequest(
                        "You don't specified any title for the task!"
                    );

                var todo = new Todo
                {
                    Title = todoDTO.Title,
                    DueDate = todoDTO.DueDate,
                    IsCompleted = false
                };

                await todoService.Add(todo);
                await todoService.SaveChangesAsync();

                return Results.CreatedAtRoute(
                    GetByIdEndpoint,
                    new { id = todo.Id },
                    todo
                );
            }
        );

        // DELETE /todos/1
        todoGroup.MapDelete(
            "/{id}",
            async (int id, ITodoService todoService) =>
            {
                var todo = await todoService.GetById(id);

                if (todo is null)
                    return Results.NotFound(
                        $"A todo item with id={id} not found!"
                    );

                todoService.Delete(todo);
                await todoService.SaveChangesAsync();

                return Results.NoContent();
            }
        );

        // PATCH /todos/1
        todoGroup.MapPatch(
            "/{id}",
            async (int id, UpdateTodoDTO todoDTO, ITodoService todoService) =>
            {
                var todo = await todoService.GetById(id);

                if (todo == null)
                    return Results.NotFound(
                        $"A todo item with id={id} not found!"
                    );

                if (todoDTO.Title != null)
                    todo.Title = todoDTO.Title;

                if (todoDTO.IsCompleted != null)
                    todo.IsCompleted = (bool)todoDTO.IsCompleted;

                if (todoDTO.DueDate != null)
                    todo.DueDate = (DateTime)todoDTO.DueDate;

                await todoService.SaveChangesAsync();

                return Results.NoContent();
            }
        );

        // GET /todos/completed

        // GET /todos/pending

        // GET /todos?search=...
        
    }
}
