namespace TodoList_API.DTOs;

public record UpdateTodoDTO(
    string Title,
    bool? IsCompleted = null,
    DateTime? DueDate = null
);