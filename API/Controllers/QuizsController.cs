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
        public async Task<IActionResult> PutQuiz(int id, Quiz quiz)
        {
            if (id != quiz.Id)
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

        //// POST: api/Quizs
        //[HttpPost("/Setup-Quiz")]
        //public async Task<ActionResult<Quiz>> PostQuiz(QuizDTO quizDTO)
        //{
        //    List<Question> selectedQuestions = new List<Question>();

        //    Quiz quiz = new()
        //    {
        //        InvitedUsers = quizDTO.InvitedUsers,
        //        Title = quizDTO.Title,
        //        Category = quizDTO.Category,
        //        AddedTime = quizDTO.AddedTime,
        //        Timer = quizDTO.Timer,
        //        CreatorId = quizDTO.CreatorId,
        //        Maindifficulty = quizDTO.Maindifficulty,
        //        QuestionAmount = selectedQuestions.Count,
        //        Questions = selectedQuestions // assuming Quiz entity has a collection of Questions
        //    };

        //    _context.Quizs.Add(quiz);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetQuiz", new { id = quiz.Id }, quiz);
        //}

        //// POST: api/Quizs
        //[HttpPost("/Setup-Quiz-Auto-Select")]
        //public async Task<ActionResult<Quiz>> PostQuizAutoSelect(QuizDTO quizDTO)
        //{
        //    List<Question> selectedQuestions = new List<Question>();

        //    // Fetch and randomly select questions for each specified difficulty level
        //    foreach (var entry in quizDTO.QuestionAmountsByDifficulty)
        //    {
        //        string difficulty = entry.Key;
        //        int amount = entry.Value;

        //        var questions = await _context.Questions.Where(q => q.DifficultyLevel == difficulty).ToListAsync();

        //        if (questions.Count < amount)
        //        {
        //            return BadRequest($"Not enough questions available for difficulty level {difficulty}.");
        //        }

        //        var randomQuestions = questions.OrderBy(q => Guid.NewGuid()).Take(amount).ToList();
        //        selectedQuestions.AddRange(randomQuestions);
        //    }

        //    // Map the selected questions to the quiz
        //    Quiz quiz = new()
        //    {
        //        InvitedUsers = quizDTO.InvitedUsers,
        //        Title = quizDTO.Title,
        //        Category = quizDTO.Category,
        //        AddedTime = quizDTO.AddedTime,
        //        Timer = quizDTO.Timer,
        //        CreatorId = quizDTO.CreatorId,
        //        Maindifficulty = quizDTO.Maindifficulty,
        //        QuestionAmount = selectedQuestions.Count,
        //        Questions = selectedQuestions // assuming Quiz entity has a collection of Questions
        //    };

        //    _context.Quizs.Add(quiz);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetQuiz", new { id = quiz.Id }, quiz);
        //}

        //[HttpPost("SaveAnswers")]
        //public async Task<ActionResult<Quiz>> PostAnswers(QuizDTO quizDTO)
        //{
        //    _context.Quizs.Add(quiz);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetQuiz", new { id = quiz.Id }, quiz);
        //}

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
            return _context.Quizs.Any(e => e.Id == id);
        }
    }
}
