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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class resultController : ControllerBase
    {
        private readonly ResultContext _context;
        private IHubContext<EchoHub> _hub;

        public resultController(ResultContext context, IHubContext<EchoHub> hub)
        {
            _hub = hub;
            _context = context;
        }

        [HttpGet("bytes"), Authorize]
        public async Task<ActionResult<IEnumerable<ResultItem>>> GetResultNBItems()
        {
            var alles = await _context.ResultItems.ToListAsync();

            for (int i = 0; i < alles.Count; i++)
            {
                alles[i].FileBytes = null;
            }
            await _hub.Clients.All.SendAsync("results", alles);
            return alles;
        }

        [HttpGet("bytes/{id}"), Authorize]
        public async Task<ActionResult<ResultItem>> GetResultNBItem(long id)
        {
            var todoItem = await _context.ResultItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }
            todoItem.FileBytes = null;
            return todoItem;
        }

        [HttpPost("bytes"), Authorize]
        public async Task<ActionResult<ResultItem>> PostResultNBItem(ResultItem item)
        {
            var results = await _context.ResultItems.ToListAsync();
            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].Username == item.Username && results[i].FileName == item.FileName)
                {
                    return BadRequest();
                }
            }
            _context.ResultItems.Add(item);
            await _context.SaveChangesAsync();

            for (int i = 0; i < results.Count; i++)
            {
                results[i].FileBytes = null;
            }
            await _hub.Clients.All.SendAsync("results", results);
            return CreatedAtAction(nameof(GetResultItem), new { id = item.Id }, item);
        }

        // PUT: api/Todo/5
        [HttpPut("bytes/{id}"), Authorize]
        public async Task<IActionResult> PutResultNBItem(long id, ResultItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }
            var fileItem = await _context.ResultItems.FindAsync(id);
            fileItem.Id = fileItem.Id;
            fileItem.FileBytes = fileItem.FileBytes;
            fileItem.FileName = item.FileName;
            fileItem.School = item.School;
            fileItem.Username = item.Username;

            //_context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            var alles = await _context.ResultItems.ToListAsync();

            for (int i = 0; i < alles.Count; i++)
            {
                alles[i].FileBytes = null;
            }
            await _hub.Clients.All.SendAsync("results", alles);
            return NoContent();
        }

        // DELETE: api/Todo/5
        [HttpDelete("bytes/{id}"), Authorize]
        public async Task<IActionResult> DeleteResultNBItem(long id)
        {
            var todoItem = await _context.ResultItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }
            _context.ResultItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            var alles = await _context.ResultItems.ToListAsync();

            for (int i = 0; i < alles.Count; i++)
            {
                alles[i].FileBytes = null;
            }
            await _hub.Clients.All.SendAsync("results", alles);
            return NoContent();
        }



        // GET: api/Todo
        [HttpGet, Authorize]
        public async Task<ActionResult<IEnumerable<ResultItem>>> GetResultItems()
        {
            await _hub.Clients.All.SendAsync("results", _context.ResultItems.ToListAsync());
            return await _context.ResultItems.ToListAsync();
        }

        // GET: api/Todo/5
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<ResultItem>> GetResultItem(long id)
        {
            var todoItem = await _context.ResultItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        // POST: api/Todo
        [HttpPost, Authorize]
        public async Task<ActionResult<ResultItem>> PostResultItem(ResultItem item)
        {
            _context.ResultItems.Add(item);
            await _context.SaveChangesAsync();

            await _hub.Clients.All.SendAsync("results", _context.ResultItems.ToListAsync());
            return CreatedAtAction(nameof(GetResultItem), new { id = item.Id }, item);
        }

        // PUT: api/Todo/5
        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> PutResultItem(long id, ResultItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            await _hub.Clients.All.SendAsync("results", _context.ResultItems.ToListAsync());
            return NoContent();
        }

        // DELETE: api/Todo/5
        [HttpDelete("{id}"), Authorize]
        public async Task<IActionResult> DeleteResultItem(long id)
        {
            var todoItem = await _context.ResultItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _context.ResultItems.Remove(todoItem);
            await _context.SaveChangesAsync();
            await _hub.Clients.All.SendAsync("results", _context.ResultItems.ToListAsync());
            return NoContent();
        }
    }
}
