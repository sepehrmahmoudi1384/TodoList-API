namespace TodoList_API.DTOs;

public record CreateTodoDTO(string Title, DateTime DueDate)
{
    public bool IsValidDueDate() => DueDate > DateTime.Now; 
}