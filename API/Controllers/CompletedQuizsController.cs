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
    public class CompletedQuizsController : ControllerBase
    {
        private readonly AppDBContext _context;

        public CompletedQuizsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/CompletedQuizs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompletedQuiz>>> GetCompletedQuizs()
        {
            return await _context.CompletedQuizs.ToListAsync();
        }

        // GET: api/CompletedQuizs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CompletedQuiz>> GetCompletedQuiz(int id)
        {
            var completedQuiz = await _context.CompletedQuizs.FindAsync(id);

            if (completedQuiz == null)
            {
                return NotFound();
            }

            return completedQuiz;
        }

        // PUT: api/CompletedQuizs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompletedQuiz(int id, CompletedQuiz completedQuiz)
        {
            if (id != completedQuiz.ID)
            {
                return BadRequest();
            }

            _context.Entry(completedQuiz).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompletedQuizExists(id))
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

        // POST: api/CompletedQuizs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CompletedQuiz>> PostCompletedQuiz(CompletedQuiz completedQuiz)
        {
            _context.CompletedQuizs.Add(completedQuiz);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompletedQuiz", new { id = completedQuiz.ID }, completedQuiz);
        }

        // DELETE: api/CompletedQuizs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompletedQuiz(int id)
        {
            var completedQuiz = await _context.CompletedQuizs.FindAsync(id);
            if (completedQuiz == null)
            {
                return NotFound();
            }

            _context.CompletedQuizs.Remove(completedQuiz);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompletedQuizExists(int id)
        {
            return _context.CompletedQuizs.Any(e => e.ID == id);
        }
    }
}
