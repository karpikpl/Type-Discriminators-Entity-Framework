using Microsoft.EntityFrameworkCore;
using Windigo.Models;

namespace Windigo.Data;

public class TodoContext : DbContext
{
    private bool _modelInitialized = false;

    public TodoContext(DbContextOptions<TodoContext> options)
        : base(options)
    {
    }

    public DbSet<TodoItem> TodoItems { get; set; }
    public DbSet<WorkTodoItem> WorkTodoItems { get; set; }
    public DbSet<PersonalTodoItem> PersonalTodoItems { get; set; }

    public DbSet<ApplicationConfig> ApplicationConfigs { get; set; }
    public DbSet<Plant> Plants { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (_modelInitialized)
        {
            // NOTE: if for any reason more than 1 context is created, this will prevent the model from being re-initialized
            return;
        }

        modelBuilder.ApplyConfiguration(new ApplicationConfigConfiguration(modelBuilder));
        modelBuilder.ApplyConfiguration(new PlantConfiguration(modelBuilder));

        modelBuilder.Entity<TodoItem>()
            .ToTable("Todos")
            .HasKey(t => t.Id);

        // same thing with reflection:
        var todoItemType = typeof(TodoItem);
        var derivedTypes = todoItemType.Assembly.GetTypes()
            .Where(p => todoItemType.IsAssignableFrom(p));

        var discriminatorBuilder = modelBuilder.Entity<TodoItem>()
            .HasDiscriminator<int>("TodoTypeDiscriminator");

        foreach (var type in derivedTypes)
        {
            var instance = Activator.CreateInstance(type) as TodoItem;

            discriminatorBuilder
                .HasValue(type, instance!.TodoTypeDiscriminator);
        }

        base.OnModelCreating(modelBuilder);

        _modelInitialized = true;
    }
}