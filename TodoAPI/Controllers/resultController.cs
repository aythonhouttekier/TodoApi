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

            //    public long Id { get; set; }
            //public string Username { get; set; }
            //public string School { get; set; }
            //public string Opleiding { get; set; }
            //public string Vak { get; set; }
            //public string FileName { get; set; }
            //public byte[] FileBytes { get; set; }
            //if (_context.ResultItems.Count() == 0)
            //{
            //    // Create a new TodoItem if collection is empty,
            //    // which means you can't delete all Items.
            //    _context.ResultItems.Add(new ResultItem
            //    {
            //        Username = "Student1",
            //        School = "Vives",
            //        Opleiding = "Elektronica-ICT1",
            //        Vak = "Programmeren1",
            //        FileName = "examenProgrammeren1",
            //        FileBytes = null
            //    });
            //    _context.ResultItems.Add(new ResultItem
            //    {
            //        Username = "Student4",
            //        School = "Vives",
            //        Opleiding = "Elektronica-ICT1",
            //        Vak = "Programmeren1",
            //        FileName = "examenProgrammeren1",
            //        FileBytes = null
            //    });
            //    _context.ResultItems.Add(new ResultItem
            //    {
            //        Username = "Student3",
            //        School = "Vives",
            //        Opleiding = "Elektronica-ICT1",
            //        Vak = "Programmeren1",
            //        FileName = "examenProgrammeren1",
            //        FileBytes = null
            //    });
            //    _context.ResultItems.Add(new ResultItem
            //    {
            //        Username = "Student5",
            //        School = "Vives",
            //        Opleiding = "Luchtvaart1",
            //        Vak = "Onderhoud",
            //        FileName = "examenOnderhoud1",
            //        FileBytes = null
            //    });
            //    _context.ResultItems.Add(new ResultItem
            //    {
            //        Username = "Student5",
            //        School = "VUB",
            //        Opleiding = "Rechten1",
            //        Vak = "Recht",
            //        FileName = "examenRecht1",
            //        FileBytes = null
            //    });

            //    _context.SaveChanges();
            //}
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

            var alles = await _context.ResultItems.ToListAsync();

            for (int i = 0; i < alles.Count; i++)
            {
                alles[i].FileBytes = null;
            }
            await _hub.Clients.All.SendAsync("results", alles);
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
            fileItem.Opleiding = item.Opleiding;
            fileItem.Vak = item.Vak;
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


        [HttpPut("bytes/naam/{filenaam}/school/{school}/opleiding/{opleiding}/nieuwenaam/{nieuwenaam}"), Authorize]
        public async Task<IActionResult> PutNieuweNaammmResultNBItem(string filenaam, string school, string opleiding, string nieuwenaam)
        {
            var alles = await _context.ResultItems.ToListAsync();

            for (int i = 0; i < alles.Count; i++)
            {
                if (alles[i].School == school && alles[i].Opleiding == opleiding && alles[i].FileName == filenaam)
                {
                    alles[i].FileName = nieuwenaam;
                }
            }
            await _context.SaveChangesAsync();
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

        // DELETE: bytes/deleteall
        [HttpDelete("deleteall"), Authorize]
        public async Task<IActionResult> DeleteAllResults()
        {
            var alles = await _context.ResultItems.ToListAsync();
            
            for (int i = 0; i < alles.Count; i++)
            {
                var todoItem = await _context.ResultItems.FindAsync(alles[i].Id);
                _context.ResultItems.Remove(todoItem);
            }
            
            await _context.SaveChangesAsync();
            var tout = await _context.ResultItems.ToListAsync();

            await _hub.Clients.All.SendAsync("results", tout);
            return NoContent();
        }
        [HttpDelete("deleteall/school/{school}"), Authorize]
        public async Task<IActionResult> DeleteSchoolResults(string school)
        {
            var alles = await _context.ResultItems.ToListAsync();

            for (int i = 0; i < alles.Count; i++)
            {
                if (alles[i].School == school)
                {
                    var todoItem = await _context.ResultItems.FindAsync(alles[i].Id);
                    _context.ResultItems.Remove(todoItem);
                }
            }

            await _context.SaveChangesAsync();
            var tout = await _context.ResultItems.ToListAsync();

            await _hub.Clients.All.SendAsync("results", tout);
            return NoContent();
        }
        [HttpDelete("deleteall/opleiding/{opleiding}"), Authorize]
        public async Task<IActionResult> DeleteAllOpleidingResults(string opleiding)
        {
            var alles = await _context.ResultItems.ToListAsync();

            for (int i = 0; i < alles.Count; i++)
            {
                if (alles[i].Opleiding == opleiding)
                {
                    var todoItem = await _context.ResultItems.FindAsync(alles[i].Id);
                    _context.ResultItems.Remove(todoItem);
                }
            }

            await _context.SaveChangesAsync();
            var tout = await _context.ResultItems.ToListAsync();

            await _hub.Clients.All.SendAsync("results", tout);
            return NoContent();
        }

        [HttpDelete("deleteall/opleiding/{opleiding}/vak/{vak}"), Authorize]
        public async Task<IActionResult> DeleteOpleidingResults(string opleiding, string vak)
        {
            var alles = await _context.ResultItems.ToListAsync();

            for (int i = 0; i < alles.Count; i++)
            {
                if (alles[i].Opleiding == opleiding && alles[i].Vak == vak)
                {
                    var todoItem = await _context.ResultItems.FindAsync(alles[i].Id);
                    _context.ResultItems.Remove(todoItem);
                }
            }

            await _context.SaveChangesAsync();
            var tout = await _context.ResultItems.ToListAsync();

            await _hub.Clients.All.SendAsync("results", tout);
            return NoContent();
        }

        [HttpDelete("delete/username/{username}"), Authorize]
        public async Task<IActionResult> DeleteUsernameResults(string username)
        {
            var alles = await _context.ResultItems.ToListAsync();

            for (int i = 0; i < alles.Count; i++)
            {
                if (alles[i].Username == username)
                {
                    var todoItem = await _context.ResultItems.FindAsync(alles[i].Id);
                    if (todoItem == null)
                    {
                        return NotFound();
                    }
                    _context.ResultItems.Remove(todoItem);
                }
            }

            await _context.SaveChangesAsync();
            var tout = await _context.ResultItems.ToListAsync();

            await _hub.Clients.All.SendAsync("results", tout);
            return NoContent();
        }

        [HttpDelete("delete/opleiding/{opleiding}/filename/{filename}"), Authorize]
        public async Task<IActionResult> DeleteFilenameResults(string opleiding, string filename)
        {
            var alles = await _context.ResultItems.ToListAsync();

            for (int i = 0; i < alles.Count; i++)
            {
                if (alles[i].FileName == filename && alles[i].Opleiding == opleiding)
                {
                    var todoItem = await _context.ResultItems.FindAsync(alles[i].Id);
                    if (todoItem == null)
                    {
                        return NotFound();
                    }
                    _context.ResultItems.Remove(todoItem);
                }
            }

            await _context.SaveChangesAsync();
            var tout = await _context.ResultItems.ToListAsync();

            await _hub.Clients.All.SendAsync("results", tout);
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


        // GET: api/Todo/5
        [HttpGet("geeet/{filename}/{username}"), Authorize]
        public async Task<ActionResult<ResultItem>> GetFilenameResultItem(string filename, string username)
        {
            var alles = await _context.ResultItems.ToListAsync();

            for (int i = 0; i < alles.Count; i++)
            {
                if (alles[i].FileName == filename && alles[i].Username == username)
                {
                    var todoItem = alles[i];
                    return todoItem;
                }
            }
            return Ok("Geen result");
        }


        // GET: api/Todo/5
        [HttpGet("getresults/{username}"), Authorize]
        public async Task<ActionResult<List<ResultItem>>> GetUsernaamResultItem(string username)
        {
            var alles = await _context.ResultItems.ToListAsync();
            var a = new List<ResultItem> { };
            for (int i = 0; i < alles.Count; i++)
            {
                if (alles[i].Username == username)
                {
                    var todoItem = alles[i];
                    a.Add(todoItem);
                }
            }
            return a;
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
