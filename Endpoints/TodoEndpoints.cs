using TodoList_API.DTOs;
using TodoList_API.DTOs.Extensions;
using TodoList_API.Models;
using TodoList_API.Services;
using TodoList_API.Services.Extensions;

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
            async (ITodoRepository todoRepository) =>
            {
                var todos = await todoRepository.GetAll();
                return Results.Ok(todos);
            }
        );

        // GET /todos/1
        todoGroup.MapGet(
            "/{id}",
            async (int id, ITodoRepository todoRepository) =>
            {
                var todo = await todoRepository.GetById(id);

                if (todo is null)
                    return Results.NotFound();

                return Results.Ok(todo);
            }
        )
        .WithName(GetByIdEndpoint);

        // POST /todos
        todoGroup.MapPost(
            "/",
            async (CreateTodoDTO todoDTO, ITodoRepository todoRepository) =>
            {
                if (!todoDTO.IsValidDueDate())
                    return Results.BadRequest(
                        $"This Due Time '{todoDTO.DueDate}' is for the past!"
                    );

                if (todoDTO.Title is null)
                    return Results.BadRequest(
                        "You don't specified any title for the task!"
                    );

                todoDTO.TryParseTodoDTO(out Todo todo);

                await todoRepository.Add(todo);
                await todoRepository.SaveChangesAsync();

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
            async (int id, ITodoRepository todoRepository) =>
            {
                var todo = await todoRepository.GetById(id);

                if (todo is null)
                    return Results.NotFound(
                        $"A todo item with id={id} not found!"
                    );

                todoRepository.Delete(todo);
                await todoRepository.SaveChangesAsync();

                return Results.NoContent();
            }
        );

        // PATCH /todos/1
        todoGroup.MapPatch(
            "/{id}",
            async (int id, UpdateTodoDTO todoDTO, ITodoRepository todoRepository) =>
            {
                var todo = await todoRepository.GetById(id);

                if (todo is null)
                    return Results.NotFound(
                        $"A todo item with id={id} not found!"
                    );

                todo.PatchTodo(todoDTO);

                await todoRepository.SaveChangesAsync();

                return Results.NoContent();
            }
        );

        // GET /todos/completed
        todoGroup.MapGet(
            "/completed",
            async (ITodoRepository todoRepository)
                => await todoRepository.GetComplete()
        );

        // GET /todos/pending
        todoGroup.MapGet(
            "/pending",
            async (ITodoRepository todoRepository)
                => await todoRepository.GetInComplete()
        );

        // GET /find?title=...
        todoGroup.MapGet(
            "/find",
            async (string? title, ITodoRepository todoRepository) =>
            {
                if (!title.IsValidateTitle())
                    return Results.BadRequest(
                        $"You should specify a value for title parameter!"
                    );
                
                var foundTodos = await todoRepository.SearchByTitle(title!);

                return Results.Ok(foundTodos);
            }
        );
    }
}
