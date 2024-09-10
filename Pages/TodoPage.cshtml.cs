using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Windigo.Data;
using Windigo.Models;

namespace Windigo.Pages
{
    public class TodoPageModel : PageModel
    {
        private readonly TodoContext _context;

        public TodoPageModel(TodoContext context)
        {
            _context = context;
        }

        public IList<TodoItem> TodoItems { get; set; }
        public IList<WorkTodoItem> WorkTodoItems { get; set; }
        public IList<PersonalTodoItem> PersonalTodoItems { get; set; }

        public async Task OnGetAsync()
        {
            TodoItems = await _context.TodoItems.ToListAsync();
            WorkTodoItems = await _context.WorkTodoItems.ToListAsync();
            PersonalTodoItems = await _context.PersonalTodoItems.ToListAsync();
        }
    }
}