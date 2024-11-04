using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EducationsController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly TokenController _tokenController;
        public EducationsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Educations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Educations>>> GetEducations()
        {
            return await _context.Educations.ToListAsync();
        }

        // GET: api/Educations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Educations>> GetEducations(int id)
        {
            var educations = await _context.Educations.FindAsync(id);

            if (educations == null)
            {
                return NotFound();
            }

            return educations;
        }

        // PUT: api/Educations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEducations(int id, Educations educations)
        {
            if (id != educations.ID)
            {
                return BadRequest();
            }

            _context.Entry(educations).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EducationsExists(id))
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

        // POST: api/Educations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Educations>> PostEducations(EducationsDTO educationsDTO)
        {
            Educations educations = new()
            {
                Education = educationsDTO.Education,
            };

            _context.Educations.Add(educations);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEducations", new { id = educations.ID }, educations);
        }

        // DELETE: api/Educations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEducations(int id)
        {
            var educations = await _context.Educations.FindAsync(id);
            if (educations == null)
            {
                return NotFound();
            }

            _context.Educations.Remove(educations);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EducationsExists(int id)
        {
            return _context.Educations.Any(e => e.ID == id);
        }
    }
}
