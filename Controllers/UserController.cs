using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MTG_Tavern_MVP.Data;
using MTG_Tavern_MVP.Models;
using MTG_Tavern_MVP.Models.DTOs;
using MTG_Tavern_MVP.Services;

namespace MTG_Tavern_MVP.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {   
        private readonly AppDbContext _context;
        private readonly PasswordHashingService _passwordHashingService;

        public UserController(AppDbContext context, PasswordHashingService passwordHashingService) { 
            _context = context;
            _passwordHashingService = passwordHashingService;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUsers(){
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return BadRequest("User was not found");
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<List<User>>> AddUser(UserDTO user)
        {
            var generateSalt = _passwordHashingService.GenerateSalt();

            var newUser = new User
            {
                Id = Guid.NewGuid().ToString(),
                Name = user.Name,
                Email = user.Email,
                Password = _passwordHashingService.HashPassword(user.Password, generateSalt),
                Salt = generateSalt,
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return Ok(await _context.Users.ToListAsync());
        }
    }
}
