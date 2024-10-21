using API.Models;
using Microsoft.AspNetCore.Cors;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly AppDBContext _context;

        public QuestionsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Questions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionDTO>>> GetQuestions()
        {
            return (await _context.Questions.
                Include(q => q.CreatorID).
                Include(q => q.category).
                Include(q => q.underCategory).
                Include(q => q.difficulty).
                ToListAsync()).Select(q => new QuestionDTO()
                {
                    ID = q.ID,
                    Creator = q.CreatorID.Username,
                    Category = q.category.Category,
                    CorrectAnswer = q.CorrectAnswer,
                    Difficulty = q.difficulty.Difficulty,
                    Picture = q.Picture,
                    PossibleAnswers = q.PossibleAnswers,
                    QuestionStatus = q.QuestionStatus,
                    Time = q.Time,
                    Title = q.Title,
                    UnderCategory = q.underCategory.UnderCategory,
                }).ToList();
        }

        // GET: api/Questions/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Question>> GetQuestion(int id)
        {
            var question = await _context.Questions.FindAsync(id);

            if (question == null)
            {
                return NotFound();
            }

            return question;
        }

        // PUT: api/Questions/id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestion(int id, Question question)
        {
            if (id != question.ID)
            {
                return BadRequest();
            }

            _context.Entry(question).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionExists(id))
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

        // POST: api/Questions
        [HttpPost]
        public async Task<ActionResult<QuestionDTO>> PostQuestion(QuestionCreateDTO questionDTO)
        {
            var category = await _context.Categories.FindAsync(questionDTO.Category);
            if (category == null)
            {
                return NotFound();
            }

            var underCategory = await _context.UnderCategories.FindAsync(questionDTO.UnderCategory);
            if (underCategory == null)
            {
                return NotFound();
            }

            var difficulties = await _context.Difficulties.FindAsync(questionDTO.Difficulty);
            if (difficulties == null)
            {
                return NotFound();
            }

            var creator = await _context.Users.FindAsync(questionDTO.Creator);
            if (creator == null)
            {
                return NotFound();
            }

            Question question = new()
            {
                CreatorID = creator,
                Title = questionDTO.Title,
                category = category,
                underCategory = underCategory,
                difficulty = difficulties,
                PossibleAnswers = questionDTO.PossibleAnswers,
                CorrectAnswer = questionDTO.CorrectAnswer, // Convert the result to an array
                Picture = questionDTO.Picture,
                Time = questionDTO.Time,
                QuestionStatus = questionDTO.QuestionStatus
            };

            _context.Questions.Add(question);
            await _context.SaveChangesAsync();

            QuestionDTO questionGetDTO = new()
            {
                Category = category.Category,
                CorrectAnswer = question.CorrectAnswer,
                Picture = question.Picture,
                Time = question.Time,
                QuestionStatus = question.QuestionStatus,
                PossibleAnswers = question.PossibleAnswers,
                UnderCategory = underCategory.UnderCategory,
                Difficulty = difficulties.Difficulty,
                Creator = creator.Username,
                Title = question.Title,
            };

            return CreatedAtAction("GetQuestion", new { id = question.ID }, questionDTO);
        }

        // DELETE: api/Questions/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id, int userId)
        {
            var question = await _context.Questions.Include(q => q.CreatorID).FirstOrDefaultAsync(q => q.ID == id); ;
            if (question == null)
            {
                return NotFound();
            }

            if (question.CreatorID.ID != userId)
            {
                return Unauthorized("only the creator can delete the question");
            }

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();

            return NoContent();

        }

        private bool QuestionExists(int id)
        {
            return _context.Questions.Any(e => e.ID == id);
        }
    }
}
