using System.Linq.Expressions;
using TodoList_API.Models;

namespace TodoList_API.Services;

public interface ITodoRepository
{
    Task<IEnumerable<Todo>> GetAll();
    Task<IEnumerable<Todo>> GetComplete();
    Task<IEnumerable<Todo>> GetInComplete();
    Task<IEnumerable<Todo>> SearchByTitle(string title);
    Task<Todo?> GetById(int id);
    void Update(Todo todo);
    void Delete(Todo todo);
    Task Add(Todo todo);
    Task SaveChangesAsync();
}
