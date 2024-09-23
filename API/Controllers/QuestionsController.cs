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
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestions()
        {
            return await _context.Questions.ToListAsync();
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

        [HttpGet("Time/{Time}")]
        public async Task<ActionResult<List<Question>>> GetQuestionTime(int Time)
        {
            var timeList = await _context.Questions.Where(t => t.Time == Time).ToListAsync();

            if (timeList == null)
            {
                return NotFound();
            }

            return timeList;
        }

        [HttpGet("Status/{Status}")]
        public async Task<ActionResult<List<Question>>> GetQuestionStatus(bool Status)
        {
            var statusList = await _context.Questions.Where(s => s.QuestionStatus == Status).ToListAsync();

            if (statusList == null)
            {
                return NotFound();
            }

            return statusList;
        }

        [HttpGet("Category/{Category}")]
        public async Task<ActionResult<List<Question>>> GetQuestionCategory(string Category)
        {
            var categoryList = await _context.Questions.Where(c => c.Category == Category).ToListAsync();

            if (categoryList == null)
            {
                return NotFound();
            }

            return categoryList;
        }

        [HttpGet("UnderCategory/{UnderCategory}")]
        public async Task<ActionResult<List<Question>>> GetQuestionUnderCategory(string Category)
        {
            var underCategoryList = await _context.Questions.Where(c => c.Category == Category).ToListAsync();

            if (underCategoryList == null)
            {
                return NotFound();
            }

            return underCategoryList;
        }

        [HttpGet("Difficulty/{Difficulty}")]
        public async Task<ActionResult<List<Question>>> GetQuestionDifficulty(string Difficulty)
        {
            var difficultyList = await _context.Questions.Where(d => d.DifficultyLevel == Difficulty).ToListAsync();

            if (difficultyList == null)
            {
                return NotFound();
            }

            return difficultyList;
        }

        [HttpGet("{Category}/{Difficulty}")]
        public async Task<ActionResult<List<Question>>> GetQuestionCategoryDifficulty(string Category, string Difficulty)
        {
            var categoryDifficultyList = await _context.Questions.Where(c => c.Category == Category).Where(d => d.DifficultyLevel == Difficulty).ToListAsync();

            if (categoryDifficultyList == null)
            {
                return NotFound();
            }

            return categoryDifficultyList;
        }

        // PUT: api/Questions/id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestion(int id, Question question)
        {
            if (id != question.QuestionID)
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
        public async Task<ActionResult<Question>> PostQuestion(QuestionDTO questionDTO)
        {
            Question question = new()
            {
                Title = questionDTO.Title,
                Category = questionDTO.Category,
                UnderCategory = questionDTO.UnderCategory,
                PossibleAnswers = questionDTO.PossibleAnswers,
                CorrectAnswer = questionDTO.CorrectAnswer, // Convert the result to an array
                Picture = questionDTO.Picture,
                DifficultyLevel = questionDTO.DifficultyLevel,
                Time = questionDTO.Time,
                UserID = questionDTO.UserID,
            };

            _context.Questions.Add(question);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuestion", new { id = question.QuestionID }, question);
        }

        // DELETE: api/Questions/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id, int userId)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }

            if (question.UserID == userId)

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuestionExists(int id)
        {
            return _context.Questions.Any(e => e.QuestionID == id);
        }
    }
}
