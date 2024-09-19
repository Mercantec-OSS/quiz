using API.Models;
using API.Models.API.Models;

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
        public async Task<IActionResult> PutQuiz(int id, Quiz quiz)
        {
            if (id != quiz.QuizID)
            {
                return BadRequest();
            }

            _context.Entry(quiz).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuizExists(id))
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

        // POST: api/Quizs
        [HttpPost("/Setup-Quiz")]
        public async Task<ActionResult<Quiz>> PostQuiz(QuizDTO quizDTO)
        {

            Quiz quiz = new()
            {
                InvitedUsers = quizDTO.InvitedUsers,
                Title = quizDTO.Title,
                Category = quizDTO.Category,
                QuizDate = quizDTO.QuizDate,
                Timer = quizDTO.Timer,
                CreatorID = quizDTO.CreatorId,
                MainDifficulty = quizDTO.Maindifficulty,
                QuestionAmount = quizDTO.QuestionAmount,
                Questions = quizDTO.Questions // assuming Quiz entity has a collection of Questions
            };

            _context.Quizs.Add(quiz);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuiz", new { id = quiz.QuizID }, quiz);
        }

        [HttpPost("SaveAnswers")]
        public async Task<ActionResult<Quiz>> PostAnswers(QuizAnswers quizAnswers)
        {
            Quiz quiz = new()
            {
                UserAnswer = quizAnswers.UserAnswer,
                QuizAnswer = quizAnswers.QuizAnswer
            };

             return Ok(quiz);
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
            return _context.Quizs.Any(e => e.QuizID == id);
        }
    }
}
