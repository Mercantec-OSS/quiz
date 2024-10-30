namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class User_QuizController : ControllerBase
    {
        private readonly AppDBContext _context;

        public User_QuizController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/User_Quiz
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User_QuizDTO>>> GetUser_Quiz()
        {
            return (await _context.User_Quiz.ToListAsync()).Select(uq => new User_QuizDTO()
            {
                Completed = uq.Completed,
                QuizID = uq.quiz.ID,
                Results = uq.Results,
                UserID = uq.user.ID,
            }).ToList();
        }

        //GET: api/User_Quiz/5/2
        [HttpGet("AllUserQuiz/{userId}")]
        public async Task<ActionResult<IEnumerable<User_QuizInfoDTO>>> GetAllUserQuiz(int userId)
        {
            var user_Quiz = (await _context.User_Quiz.
                Include(uq => uq.quiz).
                Include(uq => uq.quiz.difficulty).
                Include(uq => uq.quiz.education).
                Include(uq => uq.quiz.category).
                Include(uq => uq.quiz.creator).
                Include(uq => uq.user).
                Where(uq => uq.user.ID == userId).ToListAsync())
                .Select(uq => new User_QuizInfoDTO()
                {
                    Completed = uq.Completed,
                    Results = uq.Results,
                    TimeUsed = uq.TimeUsed,
                    QuizEndDate = uq.QuizEndDate,

                    quiz = new QuizDTO
                    {
                        Category = uq.quiz.category.Category,
                        Creator = uq.quiz.creator.Username,
                        Difficulty = uq.quiz.difficulty.Difficulty,
                        Education = uq.quiz.education.Education,
                        ID = uq.quiz.ID,
                        Title = uq.quiz.Title
                    }
                }).ToList();

            if (user_Quiz == null)
            {
                return NotFound("User have not completed any quiz");
            }

            return user_Quiz;
        }

        // GET: api/User_Quiz/5/2
        [HttpGet("{quizId}/{userId}")]
        public async Task<ActionResult<User_QuizDTO>> GetUser_Quiz(int quizId, int userId)
        {
            var user_Quiz = await _context.User_Quiz.
                FirstOrDefaultAsync(uq => uq.quiz.ID == quizId && uq.user.ID == userId);

            if (user_Quiz == null)
            {
                return NotFound();
            }

            return new User_QuizDTO
            {
                QuizID = user_Quiz.quiz.ID,
                Results = user_Quiz.Results,
                Completed = user_Quiz.Completed,
                UserID = user_Quiz.user.ID,
            };
        }

        // PUT: api/User_Quiz/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutUser_Quiz(User_QuizDTO user_QuizDTO)
        {
            var user_Quiz = await _context.User_Quiz.
                FirstOrDefaultAsync(uq => uq.quiz.ID == user_QuizDTO.QuizID
                && uq.user.ID == user_QuizDTO.UserID);

            if (user_Quiz == null)
            {
                return NotFound();
            }

            user_Quiz.QuizEndDate = user_QuizDTO.QuizEndDate;
            user_Quiz.Results = user_QuizDTO.Results;
            user_Quiz.Completed = user_QuizDTO.Completed;

            _context.Entry(user_Quiz).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest("Something went wrong");
            }

            return NoContent();
        }

        // POST: api/User_Quiz
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User_Quiz>> PostUser_Quiz(User_QuizDTO user_QuizDTO)
        {
            var quiz = await _context.Quizs.FindAsync(user_QuizDTO.QuizID);
            if (quiz == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(user_QuizDTO.UserID);
            if (user == null)
            {
                return NotFound();
            }

            User_Quiz user_Quiz = new()
            {
                quiz = quiz,
                user = user,
            };

            _context.User_Quiz.Add(user_Quiz);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser_Quiz", user_Quiz);
        }

        // DELETE: api/User_Quiz/5/2
        [HttpDelete("{quizId}/{userId}")]
        public async Task<IActionResult> DeleteUser_Quiz(int quizId, int userId)
        {
            var user_Quiz = await _context.User_Quiz.
                 FirstOrDefaultAsync(uq => uq.quiz.ID == quizId && uq.user.ID == userId);

            if (user_Quiz == null)
            {
                return NotFound();
            }

            _context.User_Quiz.Remove(user_Quiz);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
