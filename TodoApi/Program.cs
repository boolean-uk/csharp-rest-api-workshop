using TodoApi.Data;
using TodoApi.Endpoints;
using TodoApi.Model;
using TodoApi.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// this creates a single OBJECT of type TaskCollection
// stores this object inside the App / Web Framework
// whenever a request handler needs this object, it will be injected
// this object lives throught the entire lifetime of the application
builder.Services.AddSingleton<TaskCollection>();

// this creates a new instance of TaskRepository for each request
builder.Services.AddScoped<ITaskRepository, TaskRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.ConfigureTaskEndpoints();


app.Run();




