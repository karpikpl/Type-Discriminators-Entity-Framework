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

        modelBuilder.Entity<TodoItem>()
            .HasDiscriminator<int>("TodoTypeDiscriminator")
            .HasValue<TodoItem>(0)
            .HasValue<WorkTodoItem>(1)
            .HasValue<PersonalTodoItem>(2);

        base.OnModelCreating(modelBuilder);
    }
}