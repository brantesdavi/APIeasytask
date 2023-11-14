using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using APIeasytask.Models;

namespace APIeasytask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : Controller
    {
        private readonly APIDbContext _context;

        public TasksController(APIDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllTasks()
        {
            var tasks = _context.Tasks
                .Include(t => t.Priority) // Incluindo a entidade Priority relacionada à Task
                .Include(t => t.Subtasks) // Incluindo as Subtasks relacionadas à Task
                .ToList();

            return Ok(tasks);
        }



        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Models.Task task)
        {
            if (task == null)
            {
                return BadRequest("Task object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Definindo a data de criação como a data atual
            task.CreatedAt = DateTime.Now;

            var existingPriority = await _context.Priorities.FindAsync(task.Priority?.PriorityId);
            if (existingPriority == null)
            {
                return BadRequest("Invalid PriorityId");
            }
            task.Priority = existingPriority;

            _context.Tasks.Add(task);

            if (task.Subtasks != null && task.Subtasks.Any())
            {
                foreach (var subtask in task.Subtasks)
                {
                    subtask.TaskId = task.TaskId; // Associando a subtask à tarefa correta
                    _context.Subtasks.Add(subtask);
                }
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTask", new { id = task.TaskId }, task);
        }

        [HttpGet("{id}")]
        public IActionResult GetTask(int id)
        {
            var task = _context.Tasks.Find(id);

            if (task == null)
            {
                return NotFound(); // Retorna 404 Not Found se a tarefa não for encontrada
            }

            return Ok(task); // Retorna 200 OK com os detalhes da tarefa se encontrada
        }

    }
}
