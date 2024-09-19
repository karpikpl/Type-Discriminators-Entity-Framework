using Microsoft.EntityFrameworkCore;
using Windigo.Data;
using Windigo.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TodoContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("TodoConnectionString"));
    options.EnableDetailedErrors();
    options.EnableSensitiveDataLogging();
    options.LogTo(Console.WriteLine, LogLevel.Trace);
});
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API V1");
    });
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<TodoContext>();
    context.Database.EnsureDeleted();
}
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<TodoContext>();
    context.Database.EnsureCreated();
}
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<TodoContext>();
    if (!context.TodoItems.Any())
    {
        context.TodoItems.AddRange(
            new TodoItem
            {
                Name = "Learn about Windigo",
                IsComplete = false
            },
            new TodoItem
            {
                Name = "Build a Windigo app",
                IsComplete = false
            },
            new WorkTodoItem
            {
                Name = "Work on Windigo",
                IsComplete = false,
                ProjectName = "Windigo"
            },
            new PersonalTodoItem
            {
                Name = "Learn to play guitar",
                IsComplete = false,
                Hobby = "Music"
            }
        );

        var plant = new Plant { Name = "Cactus" };
        context.Plants.Add(plant);
        context.ApplicationConfigs.Add(new ApplicationConfig
        {
            Key = "DefaultSQLCommandTimeoutSeconds",
            Value = "30",
            Environment = 1,
            Plant = plant,
            Comment = "testing"
        });

        context.SaveChanges();
    }
}
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<TodoContext>();

    // get DefaultSQLCommandTimeoutSeconds
    var config = context.ApplicationConfigs
        .First(c => c.Key == "DefaultSQLCommandTimeoutSeconds");
    
    var commandTimeout = int.Parse(config.Value);
    Console.WriteLine($"Command timeout: {commandTimeout}");
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();
