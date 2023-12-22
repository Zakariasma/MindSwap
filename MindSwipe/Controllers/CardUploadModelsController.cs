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

        // POST: api/CardUploadModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CardUploadModel>> PostCardUploadModel([FromForm]CardUploadModel cardUploadModel)
        {
          if (_context.CardUploadModel == null)
          {
              return Problem("Entity set 'MindSwipeContext.CardUploadModel'  is null.");
          }
          // avant tout, on doit vérifier que la carte portant le même id que l'upload existe ( on choisi l'id lors de l'upload)
          Card cardFind = _context.Card.Find(cardUploadModel.CardID);
            if (cardFind == null)
            {
                return Problem("The card don't exist");
            }
            // si la carte existe, on continue
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Images");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            // Générez des noms de fichiers uniques pour les images.
            var extensionFrontImg = "." + cardUploadModel.FrontImg.FileName.Split('.')[cardUploadModel.FrontImg.FileName.Split('.').Length - 1];
            var extensionBackImg = "." + cardUploadModel.BackImg.FileName.Split('.')[cardUploadModel.BackImg.FileName.Split('.').Length - 1];
            cardUploadModel.FrontImgName = DateTime.Now.Ticks.ToString() + extensionFrontImg;
            cardUploadModel.BackImgName = DateTime.Now.Ticks.ToString() + extensionBackImg;
            // Construisez les chemins complets pour les fichiers d'image.
            string frontImgPath = Path.Combine(uploadsFolder,cardUploadModel.FrontImgName);
            string backImgPath = Path.Combine(uploadsFolder,cardUploadModel.BackImgName);
            // Enregistrez les fichiers d'image sur le serveur.
            using (var frontImgStream = new FileStream(frontImgPath, FileMode.Create))
            {
                await cardUploadModel.FrontImg.CopyToAsync(frontImgStream);
            }

            using (var backImgStream = new FileStream(backImgPath, FileMode.Create))
            {
                await cardUploadModel.BackImg.CopyToAsync(backImgStream);
            }

            // On ajoute ensuite les filenames dans la carte associée. 
            cardFind.FrontImg = cardUploadModel.FrontImgName;
            cardFind.BackImg = cardUploadModel.BackImgName;
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
