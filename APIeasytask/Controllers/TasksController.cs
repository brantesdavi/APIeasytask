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
                .Include(t => t.Priority) 
                .Include(t => t.Subtasks) 
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
                    subtask.TaskId = task.TaskId; 
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
                return NotFound(); 
            }

            return Ok(task);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Models.Task updatedTask)
        {
            if (id != updatedTask.TaskId)
            {
                return BadRequest("Task ID mismatch");
            }

            var existingTask = await _context.Tasks.FindAsync(id);

            if (existingTask == null)
            {
                return NotFound();
            }

            existingTask.Title = updatedTask.Title;
            existingTask.Description = updatedTask.Description;
            existingTask.Deadline = updatedTask.Deadline;

            var existingPriority = await _context.Priorities.FindAsync(updatedTask.Priority?.PriorityId);
            if (existingPriority == null)
            {
                return BadRequest("Invalid PriorityId");
            }
            existingTask.Priority = existingPriority;

            // Assuming Subtasks can be updated
            if (updatedTask.Subtasks != null && updatedTask.Subtasks.Any())
            {
                // Remove existing subtasks associated with the task
                _context.Subtasks.RemoveRange(_context.Subtasks.Where(s => s.TaskId == id));

                // Add updated subtasks
                foreach (var subtask in updatedTask.Subtasks)
                {
                    subtask.TaskId = id;
                    _context.Subtasks.Add(subtask);
                }
            }

            _context.Entry(existingTask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.TaskId == id);
        }




    }
}
