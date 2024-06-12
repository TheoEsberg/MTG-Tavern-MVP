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
    public class LoginController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly PasswordHashingService _passwordHashingService;

        public LoginController(AppDbContext context, PasswordHashingService passwordHashingService)
        {
            _context = context;
            _passwordHashingService = passwordHashingService;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Login(LoginDTO data)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == data.Email);

            if (user != null)
            {
                var correctPassword = _passwordHashingService.VerifyPassword(data.Password, user.Password);
                if (correctPassword)
                {
                    return Ok(user);
                }
            }

            return BadRequest("Wrong email or password");
        }
    }
}
