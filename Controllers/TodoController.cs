using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Windigo.Data;
using Windigo.Models;

namespace Windigo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> GetTodos()
        {
            return _context.TodoItems.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<TodoItem> GetTodoItem(int id)
        {
            var todoItem = _context.TodoItems.Find(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        [HttpPost]
        public ActionResult<TodoItem> PostTodoItem(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
        }

        [HttpPut("{id}")]
        public IActionResult PutTodoItem(string id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(todoItem).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTodoItem(int id)
        {
            var todoItem = _context.TodoItems.Find(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpGet("TestQuery")]
        public JsonResult TestQuery([FromQuery]int id, string searchTerm)
        {
            var result = new { id, searchTerm, message = $"This has been {nameof(TestQuery)} with {id} and {searchTerm}" };
            return new JsonResult(result);
        }

        [Route("TestQuery2/{foo}/{optional?}")]
        [HttpPost("TestQuery2")]
        public JsonResult TestQuery2([FromQuery]int id, string foo, string? optional,[FromBody] Something something)
        {
            var result = new { message = $"This has been {nameof(TestQuery)} with {id} and {something}" };
            return new JsonResult(result);
        }

        public record Something(string Name, int Age);
    }
}