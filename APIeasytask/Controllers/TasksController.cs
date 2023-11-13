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

        // GET: Tasks
        [HttpGet]
        public IActionResult GetAllTasks()
        {
            var tasks = _context.Tasks.ToList();

            return Ok(tasks);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Models.Task task)
        {
            if(task == null)
            {
                return BadRequest("Task object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            task.TaskId = 0;
            task.CreatedAt = DateTime.Now;

            _context.Tasks.Add(task);
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
