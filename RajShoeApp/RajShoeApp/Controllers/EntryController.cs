using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RajShoeApp.Model;

namespace RajShoeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntryController : ControllerBase
    {
        private readonly ShoeModelContext _context;

        public EntryController(ShoeModelContext context)
        {
            _context = context;
        }

        // GET: api/Entry
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoeModels>>> GetShoeData()
        {
            return await _context.ShoeData.ToListAsync();
        }

        // GET: api/Entry/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShoeModels>> GetShoeModels(int id)
        {
            var shoeModels = await _context.ShoeData.FindAsync(id);

            if (shoeModels == null)
            {
                return NotFound();
            }

            return shoeModels;
        }

        // PUT: api/Entry/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShoeModels(int id, ShoeModels shoeModels)
        {
            if (id != shoeModels.Id)
            {
                return BadRequest();
            }

            _context.Entry(shoeModels).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoeModelsExists(id))
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

        // POST: api/Entry
        [HttpPost]
        public async Task<ActionResult<ShoeModels>> PostShoeModels(ShoeModels shoeModels)
        {
            _context.ShoeData.Add(shoeModels);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShoeModels", new { id = shoeModels.Id }, shoeModels);
        }

        // DELETE: api/Entry/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ShoeModels>> DeleteShoeModels(int id)
        {
            var shoeModels = await _context.ShoeData.FindAsync(id);
            if (shoeModels == null)
            {
                return NotFound();
            }

            _context.ShoeData.Remove(shoeModels);
            await _context.SaveChangesAsync();

            return shoeModels;
        }

        private bool ShoeModelsExists(int id)
        {
            return _context.ShoeData.Any(e => e.Id == id);
        }
    }
}
