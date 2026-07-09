namespace TodoList_API.DTOs.Extensions;

public static class CreateTodoDTOExtension
{
    public static bool IsValidDueDate(this CreateTodoDTO todo) 
        => todo.DueDate > DateTime.Now;
}