using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MindSwipe.Data;
using MindSwipe.Models;

namespace MindSwipe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CardsController : ControllerBase
    {
        private readonly MindSwipeContext _context;

        public CardsController(MindSwipeContext context)
        {
            _context = context;
        }

        // GET: api/Cards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Card>>> GetCard()
        {
          if (_context.Card == null)
          {
              return NotFound();
          }
            return await _context.Card.Include(card => card.Deck).Include(card => card.Deck.User).ToListAsync();
        }

        // GET: api/Cards/Deck/{id}
        [HttpGet("Deck/{id}")]
        public async Task<ActionResult<IEnumerable<Card>>> GetCardsByDeckId(int id)
        {
            if (_context.Card == null)
            {
                return NotFound();
            }
            return await _context.Card.Where(card => card.DeckId == id).Include(card => card.Deck).Include(card => card.Deck.User).ToListAsync();
        }


        // GET: api/Cards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Card>> GetCard(int id)
        {
          if (_context.Card == null)
          {
              return NotFound();
          }
            var card = await _context.Card.FindAsync(id);

            if (card == null)
            {
                return NotFound();
            }

            return card;
        }

        // PUT: api/Cards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCard(int id, Card card)
        {
            if (id != card.Id)
            {
                return BadRequest();
            }
            card.LastModifiedDate = DateTime.Now;
            _context.Entry(card).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardExists(id))
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

        // POST: api/Cards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Card>> PostCard(Card card)
        {
          if (_context.Card == null)
          {
              return Problem("Entity set 'MindSwipeContext.Card'  is null.");
          }

          if(card.FrontText != "" && card.BackText != "") 
          {
                if (card.Deck == null)
                {
                    Deck deckFind = _context.Deck.Find(card.DeckId);
                    if (deckFind != null)
                    {
                        card.Deck = deckFind;
                    }
                }
                _context.Card.Add(card);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetCard", new { id = card.Id }, card);
            }
           return BadRequest();
        }
        
        // DELETE: api/Cards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(int id)
        {
            if (_context.Card == null)
            {
                return NotFound();
            }
            var card = await _context.Card.FindAsync(id);
            if (card == null)
            {
                return NotFound();
            }

            var imageFront = Path.Combine("Images", card.FrontImg);

            if (System.IO.File.Exists(imageFront))
            {
                System.IO.File.Delete(imageFront);
            }

            var imageBack = Path.Combine("Images", card.BackImg);

            if (System.IO.File.Exists(imageBack))
            {
                System.IO.File.Delete(imageBack);
            }

            _context.Card.Remove(card);

            // Récupérer le CardUploadModel associé
            var cardUploadModel = await _context.CardUploadModel.FirstOrDefaultAsync(c => c.CardID == id);
            if (cardUploadModel != null)
            {
                _context.CardUploadModel.Remove(cardUploadModel);
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CardExists(int id)
        {
            return (_context.Card?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
