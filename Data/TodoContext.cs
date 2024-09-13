using Microsoft.EntityFrameworkCore;
using Windigo.Models;

namespace Windigo.Data;

public class TodoContext : DbContext
{
    public TodoContext(DbContextOptions<TodoContext> options)
        : base(options)
    {
    }

    public DbSet<TodoItem> TodoItems { get; set; }
    public DbSet<WorkTodoItem> WorkTodoItems { get; set; }
    public DbSet<PersonalTodoItem> PersonalTodoItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TodoItem>()
            .ToTable("Todos")
            .HasKey(t => t.Id);

        // modelBuilder.Entity<TodoItem>()
        //     .HasDiscriminator<int>("TodoTypeDiscriminator")
        //     .HasValue<TodoItem>(0)
        //     .HasValue<WorkTodoItem>(1)
        //     .HasValue<PersonalTodoItem>(2);

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
    }
}