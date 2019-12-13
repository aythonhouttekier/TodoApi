using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TodoApi.Hubs;
using TodoApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GebruikersController : ControllerBase
    {
        private readonly UserContext _context;
        private IHubContext<EchoHub> _hub;

        public GebruikersController(UserContext context, IHubContext<EchoHub> hub)
        {
            _hub = hub;
            _context = context;
            if (_context.UserItems.Count() == 0)
            {
                _context.UserItems.Add(new User
                {
                    Username = "admin",
                    Firstname = "admin",
                    Lastname = "admin",
                    Password = "azerty",
                    Access = false,
                    School = "",
                    Role = "Admin"
                });
                _context.UserItems.Add(new User
                {
                    Username = "Leerkracht1",
                    Firstname = "Leerkracht1",
                    Lastname = "Leerkracht1",
                    Password = "azerty",
                    Access = false,
                    School = "Elektronica-ICT",
                    Role = "Leerkracht"
                });
                _context.UserItems.Add(new User
                {
                    Username = "Leerkracht2",
                    Firstname = "Leerkracht2",
                    Lastname = "Leerkracht2",
                    Password = "azerty",
                    Access = false,
                    School = "Luchtvaart",
                    Role = "Leerkracht"
                });
                _context.UserItems.Add(new User
                {
                    Username = "Student1",
                    Firstname = "Student1",
                    Lastname = "Student1",
                    Password = "azerty",
                    Access = false,
                    School = "Elektronica-ICT",
                    Role = "Student"
                });
                _context.UserItems.Add(new User
                {
                    Username = "Student3",
                    Firstname = "Student3",
                    Lastname = "Student3",
                    Password = "azerty",
                    Access = false,
                    School = "Elektronica-ICT",
                    Role = "Student"
                });
                _context.UserItems.Add(new User
                {
                    Username = "Student4",
                    Firstname = "Student4",
                    Lastname = "Student4",
                    Password = "azerty",
                    Access = false,
                    School = "Elektronica-ICT",
                    Role = "Student"
                });
                _context.UserItems.Add(new User
                {
                    Username = "Student5",
                    Firstname = "Student5",
                    Lastname = "Student5",
                    Password = "azerty",
                    Access = false,
                    School = "Elektronica-ICT",
                    Role = "Student"
                });
                _context.UserItems.Add(new User
                {
                    Username = "Student6",
                    Firstname = "Student6",
                    Lastname = "Student6",
                    Password = "azerty",
                    Access = false,
                    School = "Elektronica-ICT",
                    Role = "Student"
                });
                _context.UserItems.Add(new User
                {
                    Username = "Student7",
                    Firstname = "Student7",
                    Lastname = "Student7",
                    Password = "azerty",
                    Access = false,
                    School = "Elektronica-ICT",
                    Role = "Student"
                });
                _context.UserItems.Add(new User
                {
                    Username = "Student2",
                    Firstname = "Student2",
                    Lastname = "Student2",
                    Password = "azerty",
                    Access = false,
                    School = "Luchtvaart",
                    Role = "Student"
                });
                _context.SaveChanges();
            }
        }

        //// GET: api/users
        //[HttpGet, Authorize]
        //public async Task<ActionResult<IEnumerable<User>>> GetUserItems()
        //{
        //    return await _context.UserItems.ToListAsync();
        //}
        //GET: api/users

        [HttpGet, Authorize]
        public async Task<ActionResult<IEnumerable<User>>> GetUserItems()
        {
            await _hub.Clients.All.SendAsync("Users", _context.UserItems.ToListAsync());
            return await _context.UserItems.ToListAsync();
        }

        // GET: api/users/5
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<User>> GetUserItem(long id)
        {
            var userItem = await _context.UserItems.FindAsync(id);

            if (userItem == null)
            {
                return NotFound();
            }

            return userItem;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<User>> UserLogin(User item)
        {
            var users = await _context.UserItems.ToListAsync();
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Username == item.Username && 
                    users[i].Password == item.Password)
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                    var tokeOptions = new JwtSecurityToken(
                        issuer: "http://localhost:44359",
                        audience: "http://localhost:44359",
                        claims: new List<Claim>(),
                        expires: DateTime.Now.AddMinutes(50),
                        signingCredentials: signinCredentials
                    );

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                    return Ok(new { Token = tokenString, user = users[i] });
                }
            }
            return Unauthorized();
        }

        // POST: api/users
        [HttpPost]
        public async Task<ActionResult<User>> PostUserItem(User item)
        {
            var users = await _context.UserItems.ToListAsync();
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Username == item.Username)
                {
                    return BadRequest();
                }
                else
                {
                    _context.UserItems.Add(item);
                }
            }
            await _context.SaveChangesAsync();
            await _hub.Clients.All.SendAsync("Users", _context.UserItems.ToListAsync());
            return CreatedAtAction(nameof(GetUserItem), new { id = item.Id }, item);
        }

        // PUT: api/users/5
        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> PutUserItem(long id, User item)
        {
            var users = await _context.UserItems.ToListAsync();
            var userItem = await _context.UserItems.FindAsync(id);

            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Username == item.Username && userItem.Username != item.Username)
                {
                    return BadRequest();
                }
            }
            userItem.Id = id;
            userItem.Firstname = item.Firstname;
            userItem.Lastname = item.Lastname;
            userItem.Username = item.Username;
            userItem.Password = item.Password;
            userItem.Role = item.Role;
            userItem.School = item.School;
            userItem.Access = item.Access;

            await _context.SaveChangesAsync();
            await _hub.Clients.All.SendAsync("Users", _context.UserItems.ToListAsync());
            return Ok(userItem);
        }

        // DELETE: api/users/5
        [HttpDelete("{id}"), Authorize]
        public async Task<IActionResult> DeleteUserItem(long id)
        {
            var userItem = await _context.UserItems.FindAsync(id);

            if (userItem == null)
            {
                return NotFound();
            }

            _context.UserItems.Remove(userItem);
            await _context.SaveChangesAsync();
            await _hub.Clients.All.SendAsync("Users", _context.UserItems.ToListAsync());
            return NoContent();
        }
    }
}
