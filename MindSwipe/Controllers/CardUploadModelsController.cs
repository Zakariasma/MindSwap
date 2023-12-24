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
    public class CardUploadModelsController : ControllerBase
    {
        private readonly MindSwipeContext _context;

        public CardUploadModelsController(MindSwipeContext context)
        {
            _context = context;
        }

        // GET: api/CardUploadModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CardUploadModel>>> GetCardUploadModel()
        {
          if (_context.CardUploadModel == null)
          {
              return NotFound();
          }
            return await _context.CardUploadModel.ToListAsync();
        }

        // GET: api/CardUploadModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CardUploadModel>> GetCardUploadModel(int id)
        {
          if (_context.CardUploadModel == null)
          {
              return NotFound();
          }
            var cardUploadModel = await _context.CardUploadModel.FindAsync(id);

            if (cardUploadModel == null)
            {
                return NotFound();
            }

            return cardUploadModel;
        }

        // PUT: api/CardUploadModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCardUploadModel(int id, CardUploadModel cardUploadModel)
        {
            if (id != cardUploadModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(cardUploadModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardUploadModelExists(id))
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

        [HttpPost]
        public async Task<ActionResult<CardUploadModel>> PostCardUploadModel([FromForm] CardUploadModel cardUploadModel)
        {
            if (_context.CardUploadModel == null)
            {
                return Problem("Entity set 'MindSwipeContext.CardUploadModel'  is null.");
            }

            Card cardFind = _context.Card.Find(cardUploadModel.CardID);
            if (cardFind == null)
            {
                return Problem("The card don't exist");
            }

            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Images");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            if (cardUploadModel.FrontImg != null)
            {
                var extensionFrontImg = "." + cardUploadModel.FrontImg.FileName.Split('.')[cardUploadModel.FrontImg.FileName.Split('.').Length - 1];
                cardUploadModel.FrontImgName = DateTime.Now.Ticks.ToString() + extensionFrontImg;
                string frontImgPath = Path.Combine(uploadsFolder, cardUploadModel.FrontImgName);
                using (var frontImgStream = new FileStream(frontImgPath, FileMode.Create))
                {
                    await cardUploadModel.FrontImg.CopyToAsync(frontImgStream);
                }
                cardFind.FrontImg = cardUploadModel.FrontImgName;
            }

            if (cardUploadModel.BackImg != null)
            {
                var extensionBackImg = "." + cardUploadModel.BackImg.FileName.Split('.')[cardUploadModel.BackImg.FileName.Split('.').Length - 1];
                cardUploadModel.BackImgName = DateTime.Now.Ticks.ToString() + extensionBackImg;
                string backImgPath = Path.Combine(uploadsFolder, cardUploadModel.BackImgName);
                using (var backImgStream = new FileStream(backImgPath, FileMode.Create))
                {
                    await cardUploadModel.BackImg.CopyToAsync(backImgStream);
                }
                cardFind.BackImg = cardUploadModel.BackImgName;
            }

            _context.Card.Update(cardFind);
            _context.CardUploadModel.Add(cardUploadModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCardUploadModel", new { id = cardUploadModel.Id }, cardUploadModel);
        }


        // DELETE: api/CardUploadModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCardUploadModel(int id)
        {
            if (_context.CardUploadModel == null)
            {
                return NotFound();
            }

            var cardUploadModel = await _context.CardUploadModel.FindAsync(id);
            if (cardUploadModel == null)
            {
                return NotFound();
            }

            _context.CardUploadModel.Remove(cardUploadModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CardUploadModelExists(int id)
        {
            return (_context.CardUploadModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
