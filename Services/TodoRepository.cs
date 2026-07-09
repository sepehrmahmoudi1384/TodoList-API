using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TodoList_API.Data.Context;
using TodoList_API.Models;

namespace TodoList_API.Services;

public class TodoRepository(TodoListContext dbContext)
    : ITodoRepository
{
    private readonly TodoListContext _dbContext = dbContext;

    public async Task Add(Todo todo)
        => await _dbContext.Todos.AddAsync(todo);

    public void Delete(Todo todo)
        => _dbContext.Todos.Remove(todo);

    public async Task<IEnumerable<Todo>> GetAll()
        => await _dbContext.Todos
            .AsNoTracking()
            .ToListAsync();

    public async Task<IEnumerable<Todo>> GetComplete()
        => await _dbContext.Todos
                .Where(todo => todo.IsCompleted == true)
                .ToListAsync();
                
    public async Task<IEnumerable<Todo>> GetInComplete()
        => await _dbContext.Todos
                .Where(todo => todo.IsCompleted == false)
                .ToListAsync();

    public async Task<Todo?> GetById(int id)
        => await _dbContext.Todos.FindAsync(id);

    public async Task SaveChangesAsync() =>
        await _dbContext.SaveChangesAsync();

    public async Task<IEnumerable<Todo>> SearchByTitle(string title)
        => await _dbContext.Todos
                .Where(todo => todo.Title.Contains(title))
                .ToListAsync();

    public void Update(Todo todo)
        => _dbContext.Todos.Update(todo);
}
