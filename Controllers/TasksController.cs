using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using TaskManagement.Models;

namespace TaskManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _db;

        public TasksController(AppDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Create(TaskItem task)
        {
            _db.Tasks.Add(task);
            await _db.SaveChangesAsync();
            return Ok(task);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var task = await _db.Tasks
                .Include(t => t.Comments)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (task == null) return NotFound();

            var taskDto = new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Comments = task.Comments.Select(c => new CommentDto
                {
                    Id = c.Id,
                    CommentText = c.CommentText,
                    UserId = c.UserId
                }).ToList()
            };

            return Ok(taskDto);
        }


        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            var tasks = await _db.Tasks.Where(t => t.UserId == userId).ToListAsync();
            return Ok(tasks);
        }


        public class TaskDto
        {
            public int Id { get; set; }
            public string Title { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public List<CommentDto> Comments { get; set; } = new();
        }

        public class CommentDto
        {
            public int Id { get; set; }
            public string CommentText { get; set; } = string.Empty;
            public int UserId { get; set; }
        }

    }

}
