using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MTG_Tavern_MVP.Data;
using MTG_Tavern_MVP.Models;
using MTG_Tavern_MVP.Services;

namespace MTG_Tavern_MVP.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CardController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Card>>> GetAllCards(string userId)
        {
            var user = await _context.Users.Include(u => u.Cards).FirstOrDefaultAsync(u => u.Id == userId);
            return Ok(user.Cards.ToList());
        }

        [HttpPost]
        public async Task<ActionResult<Card>> AddCardToUser(Card card, string userId)
        {
            var user = await _context.Users.Include(u => u.Cards).FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            user.Cards.Add(card);
            _context.Users.Update(user);

            await _context.SaveChangesAsync();
            return Ok(card);
        }
    }
}
