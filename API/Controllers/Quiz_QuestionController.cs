﻿namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class Quiz_QuestionController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly TokenController _tokenController;
        public Quiz_QuestionController(AppDBContext context)
        {
            _context = context;
            _tokenController = new TokenController(context);
        }
        
        // GET: api/Quiz_Question
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quiz_QuestionDTO>>> GetQuiz_Question()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userResult = await _tokenController.GetUserRole(token);

            if (userResult == null)
            {
                return Unauthorized("Invalid Token");
            }
            else if (userResult.role.Role != "Teacher" && userResult.role.Role != "Administrator")
            {
                return Unauthorized("Unauthorized");
            }

            return (await _context.Quiz_Question
                .Include(qq => qq.quiz)
                .Include(qq => qq.question)
                .ToListAsync()).Select(qq => new Quiz_QuestionDTO()
            {
                QuestionID = qq.question.ID,
                QuizID = qq.quiz.ID,
            }).ToList();
        }

        // GET: api/Quiz_Question/5
        [HttpGet("{quizId}/{questionId}")]
        public async Task<ActionResult<Quiz_Question>> GetQuiz_Question(int quizId, int questionId)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userResult = await _tokenController.GetUserRole(token);

            if (userResult == null)
            {
                return Unauthorized("Invalid Token");
            }

            var quiz_Question = await _context.Quiz_Question.
                FirstOrDefaultAsync(qq => qq.quiz.ID == quizId && qq.question.ID == questionId); ;

            if (quiz_Question == null)
            {
                return NotFound();
            }

            return Ok(quiz_Question);
        }

        // PUT: api/Quiz_Question/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public ActionResult PutQuiz_Question(Quiz_QuestionDTO quiz_Question)
        {
            return Unauthorized("Literly no reason to change this, as it can break stoff.");
        }

        // POST: api/Quiz_Question
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Quiz_Question>> PostQuiz_Question(Quiz_QuestionDTO quiz_QuestionDTO, string token)
        {
            var userResult = await _tokenController.GetUserRole(token);

            if (userResult == null)
            {
                return Unauthorized("Invalid Token");
            }
            else if (userResult.role.Role != "Teacher" && userResult.role.Role != "Administrator")
            {
                return Unauthorized("Unauthorized");
            }

            var newQuiz = await _context.Quizs.FindAsync(quiz_QuestionDTO.QuizID);
            if (newQuiz == null)
            {
                return NotFound();
            }

            var newQuestion = await _context.Questions.FindAsync(quiz_QuestionDTO.QuestionID);
            if (newQuestion == null)
            {
                return NotFound();
            }

            Quiz_Question quiz_Question = new()
            {
                quiz = newQuiz,
                question = newQuestion
            };

            _context.Quiz_Question.Add(quiz_Question);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuiz_Question", quiz_Question );
        }

        // DELETE: api/Quiz_Question/5
        [HttpDelete("{quizId}/{questionId}")]
        public async Task<IActionResult> DeleteQuiz_Question(int quizId, int questionId)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userResult = await _tokenController.GetUserRole(token);

            if (userResult == null)
            {
                return Unauthorized("Invalid Token");
            }
            
            var quiz_Question = await _context.Quiz_Question
                .Include(qq => qq.quiz)
                .ThenInclude(q => q.creator)
                .FirstOrDefaultAsync(qq => qq.quiz.ID == quizId && qq.question.ID == questionId); ;

            if (quiz_Question == null)
            {
                return NotFound();
            }
            else if (userResult.ID != quiz_Question.quiz.creator.ID && userResult.role.Role != "Administrator")
            {
                return Unauthorized("Unauthorized");
            }

            _context.Quiz_Question.Remove(quiz_Question);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
