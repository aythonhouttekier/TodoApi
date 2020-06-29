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
            _context.UserItems.Include(b => b.Examsetting).Include(b => b.Vak);
            if (_context.UserItems.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all Items.
                _context.UserItems.Add(new User
                {
                    Username = "adminSensotec",
                    Firstname = "adminSensotec",
                    Lastname = "adminSensotec",
                    Password = "azerty",
                    Access = false,
                    Status = false,
                    School = "",
                    Opleiding = "",
                    Vak = new List<Vak> { },
                    //  LeerkrachtOpleiding = new List<LeerkrachtOpleiding> { },
                    Role = "SuperAdmin",
                    Examsetting = new List<Setting> { }
                });
                _context.UserItems.Add(new User
                {
                    Username = "adminVives",
                    Firstname = "adminVives",
                    Lastname = "adminVives",
                    Password = "azerty",
                    Access = false,
                    Status = false,
                    School = "Vives",
                    Opleiding = "",
                    Vak = new List<Vak> { },
                    //  LeerkrachtOpleiding = new List<LeerkrachtOpleiding> { },
                    Role = "SchoolAdmin",
                    Examsetting = new List<Setting> { }
                });
                _context.UserItems.Add(new User
                {
                    Username = "adminHoGent",
                    Firstname = "adminHoGent",
                    Lastname = "adminHoGent",
                    Password = "azerty",
                    Access = false,
                    Status = false,
                    School = "HoGent",
                    Opleiding = "",
                    Vak = new List<Vak> { },
                    //  LeerkrachtOpleiding = new List<LeerkrachtOpleiding> { },
                    Role = "SchoolAdmin",
                    Examsetting = new List<Setting> { }
                });
                _context.UserItems.Add(new User
                {
                    Username = "Leerkracht1",
                    Firstname = "Leerkracht1",
                    Lastname = "Leerkracht1",
                    Password = "azerty",
                    Access = false,
                    Status = false,
                    School = "Vives",
                    Opleiding = "Elektronica-ICT1",
                    Vak = new List<Vak> { new Vak { User_Id = 4, Vakname = "Webtechnologie", Opleidingname = "Elektronica-ICT1" }, new Vak { User_Id = 4, Vakname = "Programmeren1", Opleidingname = "Elektronica-ICT1" }, new Vak { User_Id = 4, Vakname = "Sofware-Eng2", Opleidingname = "Elektronica-ICT2" } },
                    Role = "Leerkracht",
                    Examsetting = new List<Setting> { }
                });
                _context.UserItems.Add(new User
                {
                    Username = "Leerkrachttest",
                    Firstname = "Leerkrachttest",
                    Lastname = "Leerkrachttest",
                    Password = "azerty",
                    Access = false,
                    Status = false,
                    School = "Vives",
                    Opleiding = "Elektronica-ICT2",
                    Vak = new List<Vak> { new Vak { User_Id = 4, Vakname = "Programmeren2", Opleidingname = "Elektronica-ICT2" } },
                    Role = "Leerkracht",
                    Examsetting = new List<Setting> { }
                });
                _context.UserItems.Add(new User
                {
                    Username = "Leerkracht2",
                    Firstname = "Leerkracht2",
                    Lastname = "Leerkracht2",
                    Password = "azerty",
                    Access = false,
                    Status = false,
                    School = "Vives",
                    Opleiding = "Elektronica-ICT2",
                    Vak = new List<Vak> { new Vak { User_Id = 5, Vakname = "Elektronica1", Opleidingname = "Elektronica-ICT1" }, new Vak { User_Id = 5, Vakname = "Elektronica2", Opleidingname = "Elektronica-ICT2" } },
                    Role = "Leerkracht",
                    Examsetting = new List<Setting> { }
                });
                _context.UserItems.Add(new User
                {
                    Username = "Student1",
                    Firstname = "Student1",
                    Lastname = "Student1",
                    Password = "azerty",
                    Access = false,
                    Status = false,
                    School = "Vives",
                    Opleiding = "Elektronica-ICT1",
                    Vak = new List<Vak> { },
                    //  LeerkrachtOpleiding = new List<LeerkrachtOpleiding> { },
                    Role = "Student",
                    Examsetting = new List<Setting> { }
                });
                _context.UserItems.Add(new User
                {
                    Username = "Student3",
                    Firstname = "Student3",
                    Lastname = "Student3",
                    Password = "azerty",
                    Access = false,
                    Status = false,
                    School = "Vives",
                    Opleiding = "Elektronica-ICT1",
                    Vak = new List<Vak> { },
                    //  LeerkrachtOpleiding = new List<LeerkrachtOpleiding> { },
                    Role = "Student",
                    //Examsetting = new Setting { Spelling = true }
                    Examsetting = new List<Setting> { }
                });
                _context.UserItems.Add(new User
                {
                    Username = "Student4",
                    Firstname = "Student4",
                    Lastname = "Student4",
                    Password = "azerty",
                    Access = false,
                    Status = false,
                    School = "Vives",
                    Opleiding = "Elektronica-ICT1",
                    Vak = new List<Vak> { },
                    //  LeerkrachtOpleiding = new List<LeerkrachtOpleiding> { },
                    Role = "Student",
                    Examsetting = new List<Setting> { }
                });
                _context.UserItems.Add(new User
                {
                    Username = "Student5",
                    Firstname = "Student5",
                    Lastname = "Student5",
                    Password = "azerty",
                    Access = false,
                    Status = false,
                    School = "Vives",
                    Opleiding = "Luchtvaart1",
                    Vak = new List<Vak> { },
                    //   LeerkrachtOpleiding = new List<LeerkrachtOpleiding> { },
                    Role = "Student",
                    Examsetting = new List<Setting> { }
                });
                _context.UserItems.Add(new User
                {
                    Username = "Student6",
                    Firstname = "Student6",
                    Lastname = "Student6",
                    Password = "azerty",
                    Access = false,
                    Status = false,
                    School = "Vives",
                    Opleiding = "Luchtvaart1",
                    Vak = new List<Vak> { },
                    //   LeerkrachtOpleiding = new List<LeerkrachtOpleiding> { },
                    Role = "Student",
                    Examsetting = new List<Setting> { }
                });
                _context.UserItems.Add(new User
                {
                    Username = "Student7",
                    Firstname = "Student7",
                    Lastname = "Student7",
                    Password = "azerty",
                    Access = false,
                    Status = false,
                    School = "Vives",
                    Opleiding = "Luchtvaart1",
                    Vak = new List<Vak> { },
                    //  LeerkrachtOpleiding = new List<LeerkrachtOpleiding> { },
                    Role = "Student",
                    Examsetting = new List<Setting> { }
                });
                _context.UserItems.Add(new User
                {
                    Username = "Student2",
                    Firstname = "Student2",
                    Lastname = "Student2",
                    Password = "azerty",
                    Access = false,
                    Status = false,
                    School = "Vives",
                    Opleiding = "Luchtvaart1",
                    Vak = new List<Vak> { },
                    //   LeerkrachtOpleiding = new List<LeerkrachtOpleiding> { },
                    Role = "Student",
                    Examsetting = new List<Setting> { }
                });
                _context.UserItems.Add(new User
                {
                    Username = "Leerkracht3",
                    Firstname = "Leerkracht3",
                    Lastname = "Leerkracht3",
                    Password = "azerty",
                    Access = false,
                    Status = false,
                    School = "HoGent",
                    Opleiding = "Chemie1",
                    Vak = new List<Vak> { new Vak { User_Id = 13, Vakname = "Chemie1", Opleidingname = "Chemie1" }},
                    //  LeerkrachtOpleiding = new List<LeerkrachtOpleiding> { },
                    Role = "Leerkracht",
                    Examsetting = new List<Setting> { }
                });
                _context.UserItems.Add(new User
                {
                    Username = "Student8",
                    Firstname = "Student8",
                    Lastname = "Student8",
                    Password = "azerty",
                    Access = false,
                    Status = false,
                    School = "HoGent",
                    Opleiding = "Chemie1",
                    Vak = new List<Vak> { },
                    //  LeerkrachtOpleiding = new List<LeerkrachtOpleiding> { },
                    Role = "Student",
                    Examsetting = new List<Setting> { }
                });
                _context.UserItems.Add(new User
                {
                    Username = "Student9",
                    Firstname = "Student9",
                    Lastname = "Student9",
                    Password = "azerty",
                    Access = false,
                    Status = false,
                    School = "HoGent",
                    Opleiding = "Chemie1",
                    Vak = new List<Vak> { },
                    //  LeerkrachtOpleiding = new List<LeerkrachtOpleiding> { },
                    Role = "Student",
                    Examsetting = new List<Setting> { }
                });
                _context.UserItems.Add(new User
                {
                    Username = "Student10",
                    Firstname = "Student10",
                    Lastname = "Student10",
                    Password = "azerty",
                    Access = false,
                    Status = false,
                    School = "HoGent",
                    Opleiding = "Chemie1",
                    Vak = new List<Vak> { },
                    //  LeerkrachtOpleiding = new List<LeerkrachtOpleiding> { },
                    Role = "Student",
                    Examsetting = new List<Setting> { }
                });
                _context.UserItems.Add(new User
                {
                    Username = "Student11",
                    Firstname = "Student11",
                    Lastname = "Student11",
                    Password = "azerty",
                    Access = false,
                    Status = false,
                    School = "VUB",
                    Opleiding = "Rechten1",
                    Vak = new List<Vak> { },
                    //  LeerkrachtOpleiding = new List<LeerkrachtOpleiding> { },
                    Role = "Student",
                    Examsetting = new List<Setting> { }
                });

                _context.SaveChanges();
            }
        }

        //GET: api/users
        [HttpGet, Authorize]
        public async Task<ActionResult<IEnumerable<User>>> GetUserItems()
        {
            System.Diagnostics.Debug.WriteLine("GGGGGEEEEEEEEETTTTT");
            await _hub.Clients.All.SendAsync("Users", _context.UserItems.Include(b => b.Examsetting).Include(b => b.Vak).ToListAsync());
            return await _context.UserItems.Include(b => b.Examsetting).Include(b => b.Vak).ToListAsync();
        }

        // GET: api/users/5
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<User>> GetUserItem(long id)
        {
            var users = await _context.UserItems.Include(b => b.Examsetting).Include(b => b.Vak).ToListAsync();
            //var userItem = await _context.UserItems.FindAsync(id);

            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Id == id)
                {
                    var userItem = users[i];
                    return userItem;
                }
            }
            return BadRequest();
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<User>> UserLogin(User item)
        {
            var users = await _context.UserItems.Include(b => b.Examsetting).Include(b => b.Vak).ToListAsync();
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
                        expires: DateTime.Now.AddMinutes(240),
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
            var users = await _context.UserItems.Include(b => b.Examsetting).Include(b => b.Vak).ToListAsync();
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Username == item.Username)
                {
                    return BadRequest();
                }
                else
                {
                    if (item.Examsetting != null)
                    {
                        for (int a = 0; a < item.Examsetting.Count; a++)
                        {
                            item.Examsetting[a].User_Id = item.Id;
                        }
                    }
                    if (item.Vak != null)
                    {
                        for (int a = 0; a < item.Vak.Count; a++)
                        {
                            item.Vak[a].User_Id = item.Id;
                        }
                    }
                    _context.UserItems.Add(item);
                }
            }
            await _context.SaveChangesAsync();
            await _hub.Clients.All.SendAsync("Users", _context.UserItems.Include(b => b.Examsetting).Include(b => b.Vak).ToListAsync());
            return CreatedAtAction(nameof(GetUserItem), new { id = item.Id }, item);
        }

        // PUT: api/users/5
        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> PutUserItem(long id, User item)
        {
            var users = await _context.UserItems.Include(b => b.Examsetting).Include(b => b.Vak).ToListAsync();
            var userItem = await _context.UserItems.FindAsync(id);
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Username == item.Username && userItem.Username != item.Username)
                {
                    return BadRequest();
                }
                if (users[i].Id == id)
                {
                    var putuser = users[i];
                    putuser.Id = id;
                    putuser.Firstname = item.Firstname;
                    putuser.Examsetting = item.Examsetting;
                    putuser.Status = item.Status;
                    putuser.Lastname = item.Lastname;
                    putuser.Username = item.Username;
                    putuser.Password = item.Password;
                    putuser.Role = item.Role;
                    putuser.School = item.School;
                    putuser.Opleiding = item.Opleiding;
                    putuser.Vak = item.Vak;
                    putuser.Access = item.Access;
                    await _context.SaveChangesAsync();
                    await _hub.Clients.All.SendAsync("Users", _context.UserItems.Include(b => b.Examsetting).Include(b => b.Vak).ToListAsync());
                    return Ok(putuser);
                }
            }
            return BadRequest();
        }
        [HttpPut("leerkracht/{id}/{opleiding}"), Authorize]
        public async Task<IActionResult> PutLeerkrachtUserItem(long id, string opleiding, User item)
        {
            var users = await _context.UserItems.Include(b => b.Examsetting).Include(b => b.Vak).ToListAsync();
            var userItem = await _context.UserItems.FindAsync(id);
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Username == item.Username && userItem.Username != item.Username)
                {
                    return BadRequest();
                }
                if (users[i].Id == id)
                {
                    var putuser = users[i];
                    putuser.Opleiding = opleiding;
                    await _context.SaveChangesAsync();
                    await _hub.Clients.All.SendAsync("Users", _context.UserItems.Include(b => b.Examsetting).Include(b => b.Vak).ToListAsync());
                    return Ok(putuser);
                }
            }
            return BadRequest();
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
            await _hub.Clients.All.SendAsync("Users", _context.UserItems.Include(b => b.Examsetting).Include(b => b.Vak).ToListAsync());
            return NoContent();
        }


        // DELETE: bytes/deleteall
        [HttpDelete("deleteall"), Authorize]
        public async Task<IActionResult> DeleteAllGebruikers()
        {
            var alles = await _context.UserItems.ToListAsync();

            for (int i = 0; i < alles.Count; i++)
            {
                if (alles[i].Role == "Student")
                {
                    var todoItem = await _context.UserItems.FindAsync(alles[i].Id);
                    _context.UserItems.Remove(todoItem);
                }
            }

            await _context.SaveChangesAsync();
            var tout = await _context.UserItems.ToListAsync();

            await _hub.Clients.All.SendAsync("Users", _context.UserItems.Include(b => b.Examsetting).Include(b => b.Vak).ToListAsync());
            return NoContent();
        }
        [HttpDelete("deleteall/{school}"), Authorize]
        public async Task<IActionResult> DeleteSchoolGebruikers(string school)
        {
            var alles = await _context.UserItems.ToListAsync();

            for (int i = 0; i < alles.Count; i++)
            {
                if (alles[i].School == school && alles[i].Role == "Student")
                {
                    var todoItem = await _context.UserItems.FindAsync(alles[i].Id);
                    _context.UserItems.Remove(todoItem);
                }
            }

            await _context.SaveChangesAsync();
            var tout = await _context.UserItems.ToListAsync();

            await _hub.Clients.All.SendAsync("Users", _context.UserItems.Include(b => b.Examsetting).Include(b => b.Vak).ToListAsync());
            return NoContent();
        }
        [HttpDelete("deleteall/opleiding/{opleiding}"), Authorize]
        public async Task<IActionResult> DeleteOpleidingResults(string opleiding)
        {
            var alles = await _context.UserItems.ToListAsync();

            for (int i = 0; i < alles.Count; i++)
            {
                if (alles[i].Opleiding == opleiding && alles[i].Role == "Student")
                {
                    var todoItem = await _context.UserItems.FindAsync(alles[i].Id);
                    _context.UserItems.Remove(todoItem);
                }
            }

            await _context.SaveChangesAsync();
            var tout = await _context.UserItems.ToListAsync();

            await _hub.Clients.All.SendAsync("Users", _context.UserItems.Include(b => b.Examsetting).Include(b => b.Vak).ToListAsync());
            return NoContent();
        }










        //NOG AANPASsEN!!!!!!!!!!!!!!!!!!!!!!!!!
        [HttpPut("filename/{filename}/nieuwenaam/{nfname}/school/{school}/opleiding/{opleiding}"), Authorize]
        public async Task<IActionResult> PutUserSettingUpdateItem(string filename, string nfname, string school, string opleiding)
        {
            var users = await _context.UserItems.Include(b => b.Examsetting).Include(b => b.Vak).ToListAsync();

            for (int i = 0; i < users.Count; i++)
            {
                var gebruiker = users[i];
                if (gebruiker.School == school && gebruiker.Opleiding == opleiding)
                {
                    foreach (var d in gebruiker.Examsetting.ToArray())
                    {
                        if (d.Filename == filename)
                        {
                            d.Filename = nfname;
                        }
                    }
                }
            }
            await _context.SaveChangesAsync();
            await _hub.Clients.All.SendAsync("Users", _context.UserItems.Include(b => b.Examsetting).Include(b => b.Vak).ToListAsync());
            return Ok();
        }




        [HttpPost("setting"), Authorize]
        public async Task<ActionResult<User>> PostSettingUserItem(Setting item)
        {
            var users = await _context.UserItems.Include(b => b.Examsetting).Include(b => b.Vak).ToListAsync();
            var userItem = await _context.UserItems.FindAsync(item.User_Id);

            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Id == item.User_Id)
                {
                    var putuser = users[i];

                    for (int a = 0; a < putuser.Examsetting.Count; a++)
                    {
                        if (putuser.Examsetting[a].Filename == item.Filename)
                        {
                            return BadRequest();
                        }
                    }
                    userItem.Status = true;
                    putuser.Examsetting.Add(new Setting
                    {
                        User_Id = users[i].Id,
                        Filename = item.Filename,
                        Exam = item.Exam,
                        Spelling = item.Spelling,
                        Woord = item.Woord,
                        Woordenboek = item.Woordenboek,
                        Vertalen = item.Vertalen,
                        Drive = item.Drive,
                        Online = item.Online
                    });
                    await _context.SaveChangesAsync();
                    await _hub.Clients.All.SendAsync("Users", _context.UserItems.Include(b => b.Examsetting).Include(b => b.Vak).ToListAsync());
                    return Ok(putuser);
                }
            }
            return BadRequest();
        }

        [HttpDelete("setting/{userid}/{id}"), Authorize]
        public async Task<IActionResult> DeleteSettingUserItem(long userid, long id)
        {
            var users = await _context.UserItems.Include(b => b.Examsetting).Include(b => b.Vak).ToListAsync();

            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Id == userid)
                {
                    var gebruiker = users[i];
                    for (int a = 0; a < gebruiker.Examsetting.Count; a++)
                    {
                        if (gebruiker.Examsetting[a].Setting_Id == id)
                        {
                            var settingtodel = gebruiker.Examsetting[a];
                            gebruiker.Examsetting.Remove(settingtodel);
                            await _context.SaveChangesAsync();
                            await _hub.Clients.All.SendAsync("Users", _context.UserItems.Include(b => b.Examsetting).Include(b => b.Vak).ToListAsync());
                            return NoContent();
                        }
                    }
                }
            }
            return BadRequest();
        }





        //delete alle settings ook filename = null
        [HttpDelete("settings/delete/allesetting"), Authorize]
        public async Task<IActionResult> DeleteAlleeSettingUserItem()
        {
            var users = await _context.UserItems.Include(b => b.Examsetting).Include(b => b.Vak).ToListAsync();

            for (int i = 0; i < users.Count; i++)
            {
                var gebruiker = users[i];
                for (int a = 0; a < gebruiker.Examsetting.Count; a++)
                {
                    var settingtodel = gebruiker.Examsetting[a];
                    gebruiker.Examsetting.Clear();
                }
                if (gebruiker.Examsetting.Count == 0)
                {
                    gebruiker.Status = false;
                }
            }
            await _context.SaveChangesAsync();
            await _hub.Clients.All.SendAsync("Users", _context.UserItems.Include(b => b.Examsetting).Include(b => b.Vak).ToListAsync());
            return Ok();
        }






        //delete alle settings behalve die van alle/overige examens m.a.w filename = null   voor als er op delete alle examens geklikt wordt
        [HttpDelete("settings/delete/allenotnull/setting"), Authorize]
        public async Task<IActionResult> DeleteFileAllSettingUserItem()
        {
            var users = await _context.UserItems.Include(b => b.Examsetting).Include(b => b.Vak).ToListAsync();

            for (int i = 0; i < users.Count; i++)
            {
                var gebruiker = users[i];
                foreach (var d in gebruiker.Examsetting.ToArray())
                {
                    if (d.Filename != null)
                    {
                        gebruiker.Examsetting.Remove(d);
                    }
                    if (gebruiker.Examsetting.Count == 0)
                    {
                        gebruiker.Status = false;
                    }
                }
            }
            await _context.SaveChangesAsync();
            await _hub.Clients.All.SendAsync("Users", _context.UserItems.Include(b => b.Examsetting).Include(b => b.Vak).ToListAsync());
            return Ok();
        }
        [HttpDelete("setting/delsetting/{school}"), Authorize]
        public async Task<IActionResult> DeleteFileAllSchoolSettingUserItem(string school)
        {
            var alles = await _context.UserItems.Include(b => b.Examsetting).Include(b => b.Vak).ToListAsync();

            for (int i = 0; i < alles.Count; i++)
            {
                if (alles[i].School == school && alles[i].Role == "Student")
                {
                    var gebruiker = alles[i];
                    foreach (var d in gebruiker.Examsetting.ToArray())
                    {
                        if (d.Filename != null)
                        {
                            gebruiker.Examsetting.Remove(d);
                        }
                        if (gebruiker.Examsetting.Count == 0)
                        {
                            gebruiker.Status = false;
                        }
                    }
                }
            }
            await _context.SaveChangesAsync();
            await _hub.Clients.All.SendAsync("Users", _context.UserItems.Include(b => b.Examsetting).Include(b => b.Vak).ToListAsync());
            return Ok();
        }

        [HttpDelete("setting/delsetting/opleiding/{opleiding}/filenaam/{filename}"), Authorize]
        public async Task<IActionResult> DeleteFileAllOpleidingSettingUserItem(string opleiding, string filename)
        {
            var alles = await _context.UserItems.Include(b => b.Examsetting).Include(b => b.Vak).ToListAsync();

            for (int i = 0; i < alles.Count; i++)
            {
                if (alles[i].Opleiding == opleiding && alles[i].Role == "Student")
                {
                    var gebruiker = alles[i];
                    foreach (var d in gebruiker.Examsetting.ToArray())
                    {
                        if (d.Filename == filename)
                        {
                            gebruiker.Examsetting.Remove(d);
                        }
                        if (gebruiker.Examsetting.Count == 0)
                        {
                            gebruiker.Status = false;
                        }
                    }
                }
            }
            await _context.SaveChangesAsync();
            await _hub.Clients.All.SendAsync("Users", _context.UserItems.Include(b => b.Examsetting).Include(b => b.Vak).ToListAsync());
            return Ok();
        }


        [HttpDelete("deleteschoolsetting/{school}"), Authorize]
        public async Task<IActionResult> DeleteFileAlleSchoolSettingUserItem(string school)
        {
            var alles = await _context.UserItems.Include(b => b.Examsetting).Include(b => b.Vak).ToListAsync();

            for (int i = 0; i < alles.Count; i++)
            {
                if (alles[i].School == school && alles[i].Role == "Student")
                {
                    var gebruiker = alles[i];
                    for (int a = 0; a < gebruiker.Examsetting.Count; a++)
                    {
                        var settingtodel = gebruiker.Examsetting[a];
                        gebruiker.Examsetting.Remove(settingtodel);
                        gebruiker.Examsetting.Clear();
                        if (gebruiker.Examsetting.Count == 0)
                        {
                            gebruiker.Status = false;
                        }
                    }
                }
            }
            await _context.SaveChangesAsync();
            await _hub.Clients.All.SendAsync("Users", _context.UserItems.Include(b => b.Examsetting).Include(b => b.Vak).ToListAsync());
            return Ok();
        }
        [HttpDelete("deleteopleidingsetting/{opleiding}"), Authorize]
        public async Task<IActionResult> DeleteFileAlleOpleidingSettingUserItem(string opleiding)
        {
            var alles = await _context.UserItems.Include(b => b.Examsetting).Include(b => b.Vak).ToListAsync();

            for (int i = 0; i < alles.Count; i++)
            {
                if (alles[i].Opleiding == opleiding && alles[i].Role == "Student")
                {
                    var gebruiker = alles[i];
                    for (int a = 0; a < gebruiker.Examsetting.Count; a++)
                    {
                        var settingtodel = gebruiker.Examsetting[a];
                        gebruiker.Examsetting.Remove(settingtodel);
                        gebruiker.Examsetting.Clear();
                        if (gebruiker.Examsetting.Count == 0)
                        {
                            gebruiker.Status = false;
                        }
                    }
                }
            }
            await _context.SaveChangesAsync();
            await _hub.Clients.All.SendAsync("Users", _context.UserItems.Include(b => b.Examsetting).Include(b => b.Vak).ToListAsync());
            return Ok();
        }


        //[HttpDelete("settingen/opleiding/{opleiding}/id/{id"), Authorize]
        //public async Task<IActionResult> DeldeSettingenUserItem(string opleiding, long id)
        //{
        //    var alles = await _context.UserItems.Include(b => b.Examsetting).Include(b => b.Vak).ToListAsync();
        //    var userItem = await _context.UserItems.FindAsync(id);

        //    for (int i = 0; i < alles.Count; i++)
        //    {
        //        if (alles[i].Opleiding == opleiding && alles[i].Role == "Student")
        //        {
        //            var gebruiker = alles[i];
        //            for (int a = 0; a < gebruiker.Examsetting.Count; a++)
        //            {
        //                for (int b = 0; b < userItem.Vak.Count; b++)
        //                {
        //                    if (gebruiker.Examsetting[a].Filename == userItem.Vak[b].Vakname)
        //                    {
        //                        var settingtodel = gebruiker.Examsetting[a];
        //                        gebruiker.Examsetting.Remove(settingtodel);
        //                        if (gebruiker.Examsetting.Count == 0)
        //                        {
        //                            gebruiker.Status = false;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    await _context.SaveChangesAsync();
        //    await _hub.Clients.All.SendAsync("Users", _context.UserItems.Include(b => b.Examsetting).Include(b => b.Vak).ToListAsync());
        //    return Ok();
        //}



            //NOG AANPASsEN!!!!!!!!!!!!!!!!!!!!!!!!!
        //WERKT!!
        [HttpDelete("setting/opleiding/{opleiding}/{filename}"), Authorize]
        public async Task<IActionResult> DeleteFileSettingUserItem(string opleiding, string filename)
        {
            var alles = await _context.UserItems.Include(b => b.Examsetting).Include(b => b.Vak).ToListAsync();

            for (int i = 0; i < alles.Count; i++)
            {
                if (alles[i].Opleiding == opleiding && alles[i].Role == "Student")
                {
                    var gebruiker = alles[i];
                    for (int a = 0; a < gebruiker.Examsetting.Count; a++)
                    {
                        if (gebruiker.Examsetting[a].Filename == filename)
                        {
                            var settingtodel = gebruiker.Examsetting[a];
                            gebruiker.Examsetting.Remove(settingtodel);
                            if (gebruiker.Examsetting.Count == 0)
                            {
                                gebruiker.Status = false;
                            }
                        }
                        //if (gebruiker.Examsetting.Count == 0)
                        //{
                        //    gebruiker.Status = false;
                        //}
                    }
                }
            }
            await _context.SaveChangesAsync();
            await _hub.Clients.All.SendAsync("Users", _context.UserItems.Include(b => b.Examsetting).Include(b => b.Vak).ToListAsync());
            return Ok();
        }

        [HttpDelete("leerkrachtsetting/opleiding/{opleiding}/{filename}"), Authorize]
        public async Task<IActionResult> DeleteOpleidingFileSettingUserItem(string opleiding, string filename)
        {
            var alles = await _context.UserItems.Include(b => b.Examsetting).Include(b => b.Vak).ToListAsync();

            for (int i = 0; i < alles.Count; i++)
            {
                if (alles[i].Opleiding == opleiding && alles[i].Role == "Student")
                {
                    var gebruiker = alles[i];
                    for (int a = 0; a < gebruiker.Examsetting.Count; a++)
                    {
                        if (gebruiker.Examsetting[a].Filename == filename || gebruiker.Examsetting[a].Filename == null)
                        {
                            var settingtodel = gebruiker.Examsetting[a];
                            gebruiker.Examsetting.Remove(settingtodel);
                            if (gebruiker.Examsetting.Count == 0)
                            {
                                gebruiker.Status = false;
                            }
                        }
                    }
                    if (gebruiker.Examsetting.Count == 0)
                    {
                        gebruiker.Status = false;
                    }
                }
            }
            await _context.SaveChangesAsync();
            await _hub.Clients.All.SendAsync("Users", _context.UserItems.Include(b => b.Examsetting).Include(b => b.Vak).ToListAsync());
            return Ok();
        }

    }
}
