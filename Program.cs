using TodoList_API.Data;
using TodoList_API.Endpoints;
using TodoList_API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();
builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.AddTodoListDb();

var app = builder.Build();

app.MigrateDb();
app.MapTodoEndpoints();

app.Run();
