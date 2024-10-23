using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnderCategoriesController : ControllerBase
    {
        private readonly AppDBContext _context;

        public UnderCategoriesController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/UnderCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnderCategories>>> GetUnderCategories()
        {
            return await _context.UnderCategories.ToListAsync();
        }

        // GET: api/UnderCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UnderCategories>> GetUnderCategories(int id)
        {
            var underCategories = await _context.UnderCategories.FindAsync(id);

            if (underCategories == null)
            {
                return NotFound();
            }

            return underCategories;
        }

        // PUT: api/UnderCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUnderCategories(int id, UnderCategories underCategories)
        {
            if (id != underCategories.ID)
            {
                return BadRequest();
            }

            _context.Entry(underCategories).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UnderCategoriesExists(id))
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

        // POST: api/UnderCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UnderCategories>> PostUnderCategories(UnderCategoriesDTO underCategoriesDTO)
        {
            var category = await _context.Categories.FindAsync(underCategoriesDTO.CategoryID);
            if (category == null)
            {
                return NotFound();
            }

            UnderCategories underCategories = new()
            {
                UnderCategory = underCategoriesDTO.UnderCategory,
                category = category
            };

            _context.UnderCategories.Add(underCategories);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUnderCategories", new { id = underCategories.ID }, underCategories);
        }

        // DELETE: api/UnderCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnderCategories(int id)
        {
            var underCategories = await _context.UnderCategories.FindAsync(id);
            if (underCategories == null)
            {
                return NotFound();
            }

            _context.UnderCategories.Remove(underCategories);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UnderCategoriesExists(int id)
        {
            return _context.UnderCategories.Any(e => e.ID == id);
        }
    }
}
