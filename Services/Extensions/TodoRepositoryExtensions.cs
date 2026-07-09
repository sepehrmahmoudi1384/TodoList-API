using TodoList_API.DTOs;
using TodoList_API.Models;

namespace TodoList_API.Services.Extensions;

public static class TodoRepositoryExtensions
{
    public static bool IsValidateTitle(
        this string? message
    )
    => !string.IsNullOrEmpty(message);


    public static void PatchTodo(
        this Todo todo,
        UpdateTodoDTO newTodo
    )
    {
        if (newTodo.Title != null)
            todo.Title = newTodo.Title;

        if (newTodo.IsCompleted != null)
            todo.IsCompleted = (bool)newTodo.IsCompleted;

        if (newTodo.DueDate != null)
            todo.DueDate = (DateTime)newTodo.DueDate;
    }    

    public static void TryParseTodoDTO(
        this CreateTodoDTO todoDTO,
        out Todo todo
    )
    {
        todo = new Todo
        {
            Title = todoDTO.Title,
            DueDate = todoDTO.DueDate,
            IsCompleted = false
        };
    }
}