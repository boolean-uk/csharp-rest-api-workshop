using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Endpoints;
using TodoApi.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TaskContext>(
    opt =>
    {
        opt.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionString"));
    });

// this creates a new instance of TaskRepository for each request
builder.Services.AddScoped<ITaskRepository, TaskRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI();
// }


app.UseHttpsRedirection();

app.ConfigureTaskEndpoints();

app.ApplyProjectMigrations();

app.Run();




