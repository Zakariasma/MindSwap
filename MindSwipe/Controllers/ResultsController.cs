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
    //[Authorize]
    public class ResultsController : ControllerBase
    {
        private readonly MindSwipeContext _context;

        public ResultsController(MindSwipeContext context)
        {
            _context = context;
        }

        // GET: api/Results
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Result>>> GetResult()
        {
          if (_context.Result == null)
          {
              return NotFound();
          }
            return await _context.Result.Include(result => result.Deck).Include(result => result.User).ToListAsync();
        }

        // GET: api/Results/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Result>> GetResult(int id)
        {
          if (_context.Result == null)
          {
              return NotFound();
          }
            var result = await _context.Result.FindAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

        // GET: api/Results/5
        [HttpGet("byUserId/{userId}")]

        public async Task<ActionResult<IEnumerable<Result>>> GetResultsByUserId(int userId)
        {
            if (_context.Result == null)
            {
                return NotFound();
            }

            var results = await _context.Result
                .Include(result => result.Deck)
                .Include(result => result.User)
                .Where(result => result.UserId == userId)
                .ToListAsync();

            if (results == null || !results.Any())
            {
                return NotFound();
            }

            return results;
        }

        // PUT: api/Results/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResult(int id, Result result)
        {
            if (id != result.Id)
            {
                return BadRequest();
            }

            _context.Entry(result).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResultExists(id))
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

        // POST: api/Results
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Result>> PostResult(Result result)
        {
          if (_context.Result == null)
          {
              return Problem("Entity set 'MindSwipeContext.Result'  is null.");
          }
            if (result.Deck == null)
            {
                Deck deckFind = _context.Deck.Find(result.DeckId);
                if (deckFind != null)
                {
                    result.Deck = deckFind;
                }
            }

            if (result.User == null)
            {
                Users userFind = _context.Users.Find(result.UserId);
                if (userFind != null)
                {
                    result.User = userFind;
                }
            }
            _context.Result.Add(result);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetResult", new { id = result.Id }, result);
        }

        // DELETE: api/Results/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResult(int id)
        {
            if (_context.Result == null)
            {
                return NotFound();
            }
            var result = await _context.Result.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            _context.Result.Remove(result);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ResultExists(int id)
        {
            return (_context.Result?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
