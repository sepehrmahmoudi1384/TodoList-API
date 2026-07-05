using TodoList_API;
using TodoList_API.Services;
using TodoList_API.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ITodoService, TodoService>();
builder.AddTodoListDb();

var app = builder.Build();

app.MigrateDb();

app.Run();
