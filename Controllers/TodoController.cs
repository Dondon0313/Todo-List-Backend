using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;


namespace WebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private static List<Todo> Todos = new();

        [HttpGet]
        public IActionResult GetTodos() => Ok(Todos);

        [HttpGet("{id}")]
        public IActionResult GetTodo(int id)
        {
            var todo = Todos.FirstOrDefault(t => t.Id == id);
            return todo is null ? NotFound() : Ok(todo);
        }

        [HttpPost]
        public IActionResult CreateTodo([FromBody] Todo newTodo)
        {
            newTodo.Id = Todos.Count > 0 ? Todos.Max(t => t.Id) + 1 : 1;
            newTodo.Completed = false;
            newTodo.UpdatedAt = null;
            Todos.Add(newTodo);
            return CreatedAtAction(nameof(GetTodo), new { id = newTodo.Id }, newTodo);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTodo(int id, [FromBody] Todo updatedTodo)
        {
            var todo = Todos.FirstOrDefault(t => t.Id == id);
            if (todo is null) return NotFound();

            todo.Text = updatedTodo.Text;
            todo.Completed = updatedTodo.Completed;
            todo.UpdatedAt = DateTime.Now;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTodo(int id)
        {
            var todo = Todos.FirstOrDefault(t => t.Id == id);
            if (todo is null) return NotFound();
            Todos.Remove(todo);
            return NoContent();
        }

        [HttpDelete("clearCompleted")]
        public IActionResult ClearCompleted()
        {
            Todos.RemoveAll(t => t.Completed);
            return NoContent();
        }
    }
}
