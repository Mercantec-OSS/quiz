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
    public class Quiz_QuestionController : ControllerBase
    {
        private readonly AppDBContext _context;

        public Quiz_QuestionController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Quiz_Question
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quiz_Question>>> GetQuiz_Question()
        {
            return await _context.Quiz_Question.ToListAsync();
        }

        // GET: api/Quiz_Question/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Quiz_Question>> GetQuiz_Question(int id)
        {
            var quiz_Question = await _context.Quiz_Question.FindAsync(id);

            if (quiz_Question == null)
            {
                return NotFound();
            }

            return quiz_Question;
        }

        // PUT: api/Quiz_Question/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuiz_Question(int id, Quiz_Question quiz_Question)
        {
            if (id != quiz_Question.QuizID)
            {
                return BadRequest();
            }

            _context.Entry(quiz_Question).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Quiz_QuestionExists(id))
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

        // POST: api/Quiz_Question
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Quiz_Question>> PostQuiz_Question(Quiz_Question quiz_Question)
        {
            _context.Quiz_Question.Add(quiz_Question);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuiz_Question", new { id = quiz_Question.QuizID }, quiz_Question);
        }

        // DELETE: api/Quiz_Question/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuiz_Question(int id)
        {
            var quiz_Question = await _context.Quiz_Question.FindAsync(id);
            if (quiz_Question == null)
            {
                return NotFound();
            }

            _context.Quiz_Question.Remove(quiz_Question);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Quiz_QuestionExists(int id)
        {
            return _context.Quiz_Question.Any(e => e.QuizID == id);
        }
    }
}
