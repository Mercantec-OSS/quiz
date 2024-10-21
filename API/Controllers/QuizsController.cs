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
        public async Task<ActionResult<IEnumerable<Quiz>>> GetQuizs()
        {
            return await _context.Quizs.ToListAsync();
        }

        // GET: api/Quizs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Quiz>> GetQuiz(int id)
        {
            var quiz = await _context.Quizs.FindAsync(id);

            if (quiz == null)
            {
                return NotFound();
            }

            return quiz;
        }

        // PUT: api/Quizs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuiz(int id, QuizDTO quizDTO)
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
        public async Task<ActionResult<Quiz>> PostQuiz(QuizDTO quizDTO)
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
                CreatorID = creator,
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
