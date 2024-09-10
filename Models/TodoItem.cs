using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Windigo.Models;

public class TodoItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    public string Name { get; set; }
    public bool IsComplete { get; set; }

    public int TodoTypeDiscriminator { get; set; } = 0;
}

public class WorkTodoItem : TodoItem
{
    public string ProjectName { get; set; }

    public WorkTodoItem()
    {
        TodoTypeDiscriminator = 1;
    }
}

public class PersonalTodoItem : TodoItem
{
    public string Hobby { get; set; }

    public PersonalTodoItem()
    {
        TodoTypeDiscriminator = 2;
    }
}