using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Hubs;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        
        private readonly FileContext _context;
        private IHubContext<EchoHub> _hub;

        public FilesController(FileContext context, IHubContext<EchoHub> hub)
        {
            _hub = hub;
            _context = context;
        }

        [HttpGet("bytes"), Authorize]
        public async Task<ActionResult<IEnumerable<FileItem>>> GetTodoNBItems()
        {
            var alles = await _context.TodoItems.ToListAsync();

            for (int i = 0; i < alles.Count; i++)
            {
                alles[i].FileBytes = null;
            }
            await _hub.Clients.All.SendAsync("Files", alles);
            return alles;
        }

        [HttpGet("bytes/filename/{filename}"), Authorize]
        public async Task<ActionResult<FileItem>> GetfileTodoItem(string filename)
        {
            var alles = await _context.TodoItems.ToListAsync();

            for (int i = 0; i < alles.Count; i++)
            {
                alles[i].FileBytes = null;
                if (alles[i].FileName == filename)
                {
                    var todoItem = alles[i];
                    if (todoItem == null)
                    {
                        return NotFound();
                    }
                    return todoItem;
                }
            }
            return NotFound();
        }

        [HttpGet("bytes/{id}"), Authorize]
        public async Task<ActionResult<FileItem>> GetNBTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }
            todoItem.FileBytes = null;
            return todoItem;
        }

        [HttpPost("bytes"), Authorize]
        public async Task<ActionResult<FileItem>> PostNBTodoItem(FileItem item)
        {
            var results = await _context.TodoItems.ToListAsync();
            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].FileName == item.FileName && results[i].School == item.School)
                {
                    return BadRequest();
                }
            }
            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();

            var alles = await _context.TodoItems.ToListAsync();

            for (int i = 0; i < alles.Count; i++)
            {
                alles[i].FileBytes = null;
            }
            await _hub.Clients.All.SendAsync("Files", alles);
            return CreatedAtAction(nameof(GetTodoItem), new { id = item.Id }, item);
        }

        // PUT: api/Todo/5
        [HttpPut("bytes/{id}"), Authorize]
        public async Task<IActionResult> PutNBTodoItem(long id, FileItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }
            var fileItem = await _context.TodoItems.FindAsync(id);
            fileItem.Id = fileItem.Id;
            fileItem.FileBytes = fileItem.FileBytes;
            fileItem.FileName = item.FileName;
            fileItem.FileUrl = item.FileUrl;
            fileItem.School = item.School;
            fileItem.Visibility = item.Visibility;
            fileItem.Exam = item.Exam;
            fileItem.Spelling = item.Spelling;
            fileItem.Woord = item.Woord;
            fileItem.Woordenboek = item.Woordenboek;
            fileItem.Vertalen = item.Vertalen;
            fileItem.Drive = item.Drive;
            fileItem.Online = item.Online;

            //_context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            var alles = await _context.TodoItems.ToListAsync();

            for (int i = 0; i < alles.Count; i++)
            {
                alles[i].FileBytes = null;
            }
            await _hub.Clients.All.SendAsync("Files", alles);
            return NoContent();
        }

        // DELETE: api/Todo/5
        [HttpDelete("bytes/{id}"), Authorize]
        public async Task<IActionResult> DeleteNBTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }
            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            var alles = await _context.TodoItems.ToListAsync();

            for (int i = 0; i < alles.Count; i++)
            {
                alles[i].FileBytes = null;
            }
            await _hub.Clients.All.SendAsync("Files", alles);
            return NoContent();
        }

        // DELETE: bytes/deleteall
        [HttpDelete("deleteall"), Authorize]
        public async Task<IActionResult> DeleteAllResults()
        {
            var alles = await _context.TodoItems.ToListAsync();

            for (int i = 0; i < alles.Count; i++)
            {
                var todoItem = await _context.TodoItems.FindAsync(alles[i].Id);
                _context.TodoItems.Remove(todoItem);
            }

            await _context.SaveChangesAsync();
            var tout = await _context.TodoItems.ToListAsync();

            await _hub.Clients.All.SendAsync("Files", tout);
            return NoContent();
        }
        [HttpDelete("deleteall/{school}"), Authorize]
        public async Task<IActionResult> DeleteSchoolResults(string school)
        {
            var alles = await _context.TodoItems.ToListAsync();

            for (int i = 0; i < alles.Count; i++)
            {
                if (alles[i].School == school)
                {
                    var todoItem = await _context.TodoItems.FindAsync(alles[i].Id);
                    _context.TodoItems.Remove(todoItem);
                }
            }

            await _context.SaveChangesAsync();
            var tout = await _context.TodoItems.ToListAsync();

            await _hub.Clients.All.SendAsync("Files", tout);
            return NoContent();
        }








        // GET: api/Todo
        [HttpGet, Authorize]
        public async Task<ActionResult<IEnumerable<FileItem>>> GetTodoItems()
        {
            await _hub.Clients.All.SendAsync("Files", _context.TodoItems.ToListAsync());
            return await _context.TodoItems.ToListAsync();
        }

        // GET: api/Todo/5
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<FileItem>> GetTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        // POST: api/Todo
        [HttpPost, Authorize]
        public async Task<ActionResult<FileItem>> PostTodoItem(FileItem item)
        {
            var results = await _context.TodoItems.ToListAsync();
            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].FileName == item.FileName && results[i].School == item.School)
                {
                    return BadRequest();
                }
            }
            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();

            await _hub.Clients.All.SendAsync("Files", _context.TodoItems.ToListAsync());
            return CreatedAtAction(nameof(GetTodoItem), new { id = item.Id }, item);
        }

        // PUT: api/Todo/5
        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> PutTodoItem(long id, FileItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            await _hub.Clients.All.SendAsync("Files", _context.TodoItems.ToListAsync());
            return NoContent();
        }

        // DELETE: api/Todo/5
        [HttpDelete("{id}"), Authorize]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
            await _hub.Clients.All.SendAsync("Files", _context.TodoItems.ToListAsync());
            return NoContent();
        }
    }
}