using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TodoList_API.Models;

public class Todo
{
    public int Id { get; set; }
    
    [Required]
    public required string Title { get; set; }
    
    [DefaultValue(false)]
    public bool IsCompleted { get; set; }

    [Required]
    public DateOnly DueDate { get; set; }
}
