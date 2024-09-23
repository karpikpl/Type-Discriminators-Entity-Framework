using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Windigo.Data;
using Windigo.Models;

namespace Windigo.Pages
{
    public class ConfigPageModel : PageModel
    {
        private readonly TodoContext _context;

        public ConfigPageModel(TodoContext context)
        {
            _context = context;
        }

        public IList<ApplicationConfig> AppConfigItems { get; set; }

        public async Task OnGetAsync()
        {
            AppConfigItems = await _context.ApplicationConfigs
            .Include(ac => ac.Plant)
            .ToListAsync();
        }
    }
}