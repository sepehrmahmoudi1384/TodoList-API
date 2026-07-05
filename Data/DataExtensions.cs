using Microsoft.EntityFrameworkCore;
using TodoList_API.Data.Context;

namespace TodoList_API.Data;

public static class DataExtensions
{
    public static void MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        
        var dbContext = scope.ServiceProvider
            .GetRequiredService<TodoListContext>();

        dbContext.Database.Migrate();
    }

    public static void AddTodoListDb(this WebApplicationBuilder builder)
    {
        builder.Services.AddSqlite<TodoListContext>(
            connectionString: builder.Configuration.GetConnectionString("TodoList")
        );
    }
}
