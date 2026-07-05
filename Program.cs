using TodoList_API.Data;
using TodoList_API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ITodoService, TodoService>();
builder.AddTodoListDb();

var app = builder.Build();

app.MigrateDb();

app.Run();
