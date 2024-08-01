using API.Models;

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

        [HttpGet("Category/{Category}")]
        public async Task<ActionResult<List<Question>>> GetQuestionCategory(string Category)
        {
            var questionsList = await _context.Questions.Where(c => c.Category == Category).ToListAsync();

            if (questionsList == null)
            {
                return NotFound();
            }

            return questionsList;
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
            var questionsDifficultyList = await _context.Questions.Where(c => c.Category == Category).Where(d => d.DifficultyLevel == Difficulty).ToListAsync();

            if (questionsDifficultyList == null)
            {
                return NotFound();
            }

            return questionsDifficultyList;
        }

        // PUT: api/Questions/id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestion(int id, Question question)
        {
            if (id != question.Id)
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
                CorrectAnswer = questionDTO.CorrectAnswer,
                Picture = questionDTO.Picture,
                DifficultyLevel = questionDTO.DifficultyLevel,
                Time = questionDTO.Time,

                CreatorId = questionDTO.CreatorId,

                CreatedAt = DateTime.UtcNow.AddHours(2),
                UpdatedAt = DateTime.UtcNow.AddHours(2)
            };

            _context.Questions.Add(question);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuestion", new { id = question.Id }, question);
        }

        // DELETE: api/Questions/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id, int creatorId)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }

            if (question.CreatorId == creatorId)

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuestionExists(int id)
        {
            return _context.Questions.Any(e => e.Id == id);
        }
    }
}
