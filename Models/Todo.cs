using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TodoList_API.Models;

public class Todo
{
    public int Id { get; set; }
    
    [Required]
    public required string Title { get; set; }
    
    public bool IsCompleted { get; set; } = false;

    [Required]
    public required DateTime DueDate { get; set; }
}
