using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MindSwipe.Data;
using MindSwipe.Models;

namespace MindSwipe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class DecksController : ControllerBase
    {
        private readonly MindSwipeContext _context;

        public DecksController(MindSwipeContext context)
        {
            _context = context;
        }

        // GET: api/Decks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Deck>>> GetDeck()
        {
          if (_context.Deck == null)
          {
              return NotFound();
          }
            return await _context.Deck.Include(deck => deck.User).ToListAsync();
        }

        // GET: api/Decks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Deck>> GetDeck(int id)
        {
          if (_context.Deck == null)
          {
              return NotFound();
          }
            var deck = await _context.Deck.FindAsync(id);

            if (deck == null)
            {
                return NotFound();
            }

            return deck;
        }

        // PUT: api/Decks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeck(int id, Deck deck)
        {
            if (id != deck.Id)
            {
                return BadRequest();
            }
            deck.LastModifiedDate = DateTime.Now;
            _context.Entry(deck).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeckExists(id))
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

        // POST: api/Decks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Deck>> PostDeck(Deck deck)
        {
          if (_context.Deck == null)
          {
              return Problem("Entity set 'MindSwipeContext.Deck'  is null.");
          }
            
            if (deck.User == null)
            {
                Users userFind = _context.Users.Find(deck.UserID);
                if (userFind != null)
                {
                    deck.User = userFind;
                }
            }
            _context.Deck.Add(deck);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetDeck", new { id = deck.Id }, deck);
        }

        // DELETE: api/Decks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeck(int id)
        {
            if (_context.Deck == null)
            {
                return NotFound();
            }
            var deck = await _context.Deck.FindAsync(id);
            if (deck == null)
            {
                return NotFound();
            }

            _context.Deck.Remove(deck);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DeckExists(int id)
        {
            return (_context.Deck?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
