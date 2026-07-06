using TodoList_API.Models;

namespace TodoList_API.Services;

public interface ITodoService
{
    Task<IEnumerable<Todo>> GetAll();
    Task<Todo?> GetById(int id);
    void Update(Todo todo);
    void Delete(Todo todo);
    Task Add(Todo todo);
    Task SaveChangesAsync();
}
