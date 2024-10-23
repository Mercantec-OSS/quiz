using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizsController(AppDBContext context) : ControllerBase
    {
        private readonly AppDBContext _context = context;

        // GET: api/Quizs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuizDTO>>> GetQuizs()
        {
            return (await _context.Quizs.
                Include(q => q.creator).
                Include(q => q.category).
                Include(q => q.difficulty).
                Include(q => q.creator).
                Include(q => q.education).
                ToListAsync()).Select(q => new QuizDTO()
                {
                    ID = q.ID,
                    Category = q.category.Category,
                    Creator = q.creator.Username,
                    Difficulty = q.difficulty.Difficulty,
                    Education = q.education.Education,
                    QuestionAmount = q.QuestionAmount,
                    Timer = q.Timer,
                    Title = q.Title,
                }).ToList();
        }

        // GET: api/Quizs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuizDTO>> GetQuiz(int id)
        {
            var quiz = await _context.Quizs.
                Include(q => q.creator).
                Include(q => q.category).
                Include(q => q.difficulty).
                Include(q => q.creator).
                Include(q => q.education).
                FirstOrDefaultAsync(q => q.ID == id);

            if (quiz == null)
            {
                return NotFound();
            }

            QuizDTO quizDTO = new QuizDTO()
            {
                ID = quiz.ID,
                Category = quiz.category.Category,
                Creator = quiz.creator.Username,
                Difficulty = quiz.difficulty.Difficulty,
                Education = quiz.education.Education,
                QuestionAmount = quiz.QuestionAmount,
                Timer = quiz.Timer,
                Title = quiz.Title,
            };
            return quizDTO;
        }

        // PUT: api/Quizs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuiz(int id, QuizCreateDTO quizDTO)
        {
            var quiz = await _context.Quizs.FindAsync(id);
            
            if(quiz == null)
            {
                return NotFound();
            }

            quiz.Title = quizDTO.Title;
            quiz.Timer = quizDTO.Timer;
            quiz.QuestionAmount = quizDTO.QuestionAmount;

            _context.Entry(quiz).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Unauthorized("Something went wrong");
            }

            return NoContent();
        }

        // POST: api/Quizs
        [HttpPost("Setup-Quiz")]
        public async Task<ActionResult<Quiz>> PostQuiz(QuizCreateDTO quizDTO)
        {
            var education = await _context.Educations.FindAsync(quizDTO.EducationID);
            if (education == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(quizDTO.CategoryID);
            if (category == null)
            {
                return NotFound();
            }

            var difficulties = await _context.Difficulties.FindAsync(quizDTO.DifficultyID);
            if (difficulties == null)
            {
                return NotFound();
            }

            var creator = await _context.Users.FindAsync(quizDTO.CreatorID);
            if (creator == null)
            {
                return NotFound();
            }

            Quiz quiz = new()
            {
                creator = creator,
                Title = quizDTO.Title,
                education = education,
                category = category,
                difficulty = difficulties,
                Timer = quizDTO.Timer,
                QuestionAmount = quizDTO.QuestionAmount,
            };

            _context.Quizs.Add(quiz);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuiz", new { id = quiz.ID }, quizDTO);
        }

        // DELETE: api/Quizs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuiz(int id)
        {
            var quiz = await _context.Quizs.FindAsync(id);
            if (quiz == null)
            {
                return NotFound();
            }

            _context.Quizs.Remove(quiz);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuizExists(int id)
        {
            return _context.Quizs.Any(e => e.ID == id);
        }
    }
}
