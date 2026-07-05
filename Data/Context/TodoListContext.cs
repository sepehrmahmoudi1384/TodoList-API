using Microsoft.EntityFrameworkCore;
using TodoList_API.Models;

namespace TodoList_API.Data.Context;

public class TodoListContext(DbContextOptions options)
    : DbContext(options)
{
    public DbSet<Todo> Todos { get; set; }
}
