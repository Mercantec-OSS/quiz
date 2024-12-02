namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class User_QuizController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly TokenController _tokenController;

        public User_QuizController(AppDBContext context)
        {
            _context = context;
            _tokenController = new TokenController(context);
        }

        // GET: api/User_Quiz
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User_QuizDTO>>> GetUser_Quiz()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userResult = await _tokenController.GetUserRole(token);

            if (userResult == null)
            {
                return Unauthorized("Invalid Token");
            }
            else if (userResult.role.Role != "Teacher" && userResult.role.Role != "Administrator")
            {
                return Unauthorized("Unauthorized.");
            }

            return (await _context.User_Quiz.
                Include(uq => uq.quiz).
                Include(uq => uq.user).
                ToListAsync()).Select(uq => new User_QuizDTO()
                {
                    Completed = uq.Completed,
                    QuizID = uq.quiz.ID,
                    Results = uq.Results,
                    UserID = uq.user.ID,
                }).ToList();
        }

        //GET: api/User_Quiz/5/2
        [HttpGet("AllUserQuiz/{userId}")]
        public async Task<ActionResult<IEnumerable<User_QuizQuizInfoDTO>>> GetAllUserQuiz(int userId)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userResult = await _tokenController.GetUserRole(token);

            if (userResult == null)
            {
                return Unauthorized("Invalid Token");
            }
            else if (userResult.ID != userId && userResult.role.Role != "Teacher" && userResult.role.Role != "Administrator")
            {
                return Unauthorized("Unauthorized.");
            }

            var user_Quiz = (await _context.User_Quiz.
                Include(uq => uq.quiz).
                Include(uq => uq.quiz.difficulty).
                Include(uq => uq.quiz.education).
                Include(uq => uq.quiz.category).
                Include(uq => uq.quiz.creator).
                Include(uq => uq.user).
                Where(uq => uq.user.ID == userId).ToListAsync())
                .Select(uq => new User_QuizQuizInfoDTO()
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

        [HttpGet("AllStudents/{quizId}")]
        public async Task<ActionResult<IEnumerable<User_QuizUserInfoDTO>>> GetAllStudents(int quizId)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userResult = await _tokenController.GetUserRole(token);

            if (userResult == null)
            {
                return Unauthorized("Invalid Token");
            }
            else if (userResult.role.Role != "Teacher" && userResult.role.Role != "Administrator")
            {
                return Unauthorized("Unauthorized.");
            }

            var users = (await _context.User_Quiz.
                Include(uq => uq.user).
                Include(uq => uq.quiz.difficulty).
                Include(uq => uq.quiz.education).
                Include(uq => uq.quiz.category).
                Include(uq => uq.quiz.creator).
                Include(uq => uq.user.role).
                Where(uq => uq.quiz.ID == quizId).ToListAsync()).
                Select(uq => new User_QuizUserInfoDTO()
                {
                    Completed = uq.Completed,
                    Results = uq.Results,
                    TimeUsed = uq.TimeUsed,
                    QuizEndDate = uq.QuizEndDate,

                    User = new UserDTO
                    {
                        ID = uq.user.ID,
                        email = uq.user.Email,
                        role = uq.user.role.Role,
                        username = uq.user.Username,
                    }
                }).ToList();
            if (users == null)
            {
                return NotFound("No users are added to this quiz");
            }
            return Ok(users);
        }

        //used to get all students available to be added to a quiz
        [HttpGet("AllStudentsNotOnQuiz/{quizId}")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllStudentsNotOnQuiz(int quizId)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userResult = await _tokenController.GetUserRole(token);

            if (userResult == null)
            {
                return Unauthorized("Invalid Token");
            }
            else if (userResult.role.Role != "Teacher" && userResult.role.Role != "Administrator")
            {
                return Unauthorized("Unauthorized.");
            }

            var usersOnQuiz = (await _context.User_Quiz.
                Include(uq => uq.user).
                Include(uq => uq.quiz.difficulty).
                Include(uq => uq.quiz.education).
                Include(uq => uq.quiz.category).
                Include(uq => uq.quiz.creator).
                Include(uq => uq.user.role).
                Where(uq => uq.quiz.ID == quizId).ToListAsync()).
                Select(uq => new User_QuizUserInfoDTO()
                {
                    Completed = uq.Completed,
                    Results = uq.Results,
                    TimeUsed = uq.TimeUsed,
                    QuizEndDate = uq.QuizEndDate,

                    User = new UserDTO
                    {
                        ID = uq.user.ID,
                        email = uq.user.Email,
                        role = uq.user.role.Role,
                        username = uq.user.Username,
                    }
                }).ToList();
            
            var allStudents = await _context.Users
                .Include(u => u.role)
                .Where(u => u.role.Role == "Student")
                .Select(u => new UserDTO()
                {
                    ID = u.ID,
                    username = u.Username,
                    email = u.Email,
                    role = u.role.Role,
                })
                .ToListAsync();
            
            var usersOnQuizIds = new HashSet<int>(usersOnQuiz.Select(uq => uq.User.ID));
            List<UserDTO> users = new();
            
            foreach(UserDTO user in allStudents)
            {
                if (!usersOnQuizIds.Contains(user.ID))
                {
                    users.Add(user);
                }
            }


            if (users == null)
            {
                return NotFound("No users can be added to this quiz");
            }
            return Ok(users);
        }

        // GET: api/User_Quiz/5/2
        [HttpGet("{quizId}/{userId}")]
        public async Task<ActionResult<User_QuizDTO>> GetUser_Quiz(int quizId, int userId)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userResult = await _tokenController.GetUserRole(token);

            if (userResult == null)
            {
                return Unauthorized("Invalid Token");
            }
            else if (userResult.ID != userId && userResult.role.Role != "Teacher" && userResult.role.Role != "Administrator")
            {
                return Unauthorized("Unauthorized.");
            }

            var user_Quiz = await _context.User_Quiz.
                Include(uq => uq.quiz).
                Include(uq => uq.user).
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

            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userResult = await _tokenController.GetUserRole(token);

            if (userResult == null)
            {
                return Unauthorized("Invalid Token");
            }


            var user_Quiz = await _context.User_Quiz.
                Include(uq => uq.user).
                FirstOrDefaultAsync(uq => uq.quiz.ID == user_QuizDTO.QuizID
                && uq.user.ID == user_QuizDTO.UserID);

            if (user_Quiz == null)
            {
                return NotFound();
            }
            else if (userResult.ID != user_Quiz.user.ID && userResult.role.Role != "Teacher" && userResult.role.Role != "Administrator")
            {
                return Unauthorized("Unauthorized.");
            }

            if (user_QuizDTO.QuizEndDate != null)
            {
                user_Quiz.QuizEndDate = user_QuizDTO.QuizEndDate.Value;
            }
            if (user_QuizDTO.Results != 0 && user_QuizDTO.Results != null)
            {
                user_Quiz.Results = user_QuizDTO.Results;
            }
            user_Quiz.Completed = user_QuizDTO.Completed;

            _context.Entry(user_Quiz).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok("Sucessfully updated");
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpPost("MoreUsers")]
        public async Task<ActionResult<List<User_QuizInfoDTO>>> PostUsers_Quiz(CreateMultiUsers_QuizDTO users_QuizDTO)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userResult = await _tokenController.GetUserRole(token);

            if (userResult == null)
            {
                return Unauthorized("Invalid Token");
            }
            else if (userResult.role.Role != "Teacher" && userResult.role.Role != "Administrator")
            {
                return Unauthorized("Unauthorized.");
            }

            List<User_QuizInfoDTO> user_QuizDTOs = new List<User_QuizInfoDTO>();

            var userQuizzes = new List<User_Quiz>();
            var quiz = await _context.Quizs
                                .Include(q => q.education)
                                .Include(q => q.difficulty)
                                .Include(q => q.category)
                                .Include(q => q.creator)
                                .FirstOrDefaultAsync(q => q.ID == users_QuizDTO.QuizID);
            for (int i = 0; i < users_QuizDTO.UserID.Length; i++)
            {

                var user = await _context.Users
                    .Include(u => u.role)
                    .FirstOrDefaultAsync(u => u.ID == users_QuizDTO.UserID[i]);

                if (quiz == null || user == null)
                {
                    return NotFound("Quiz or User not found.");
                }
                int questionAmount = await _context.Quiz_Question.CountAsync(qa => qa.quiz.ID == quiz.ID);

                userQuizzes.Add(new User_Quiz
                {
                    quiz = quiz,
                    user = user,
                    Completed = false,
                    QuizEndDate = DateTime.UtcNow.AddDays(1),
                    Results = 0,
                    TimeUsed = 0,
                });
                user_QuizDTOs.Add(new User_QuizInfoDTO
                {
                    Quiz = new QuizDTO
                    {
                        Category = quiz.category.Category,
                        Difficulty = quiz.difficulty.Difficulty,
                        Education = quiz.education.Education,
                        Creator = quiz.creator.Username,
                        ID = quiz.ID,
                        QestionsAmount = questionAmount,
                        Title = quiz.Title
                    },
                    User = new UserDTO
                    {
                        ID = user.ID,
                        email = user.Email,
                        role = user.role.Role,
                        username = user.Username,
                    },
                    Completed = false,
                    QuizEndDate = DateTime.UtcNow.AddDays(1),
                    Results = 0,
                    TimeUsed = 0,
                });
            }
            _context.User_Quiz.AddRange(userQuizzes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser_Quiz", user_QuizDTOs);
        }
        // POST: api/User_Quiz
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User_QuizInfoDTO>> PostUser_Quiz(User_QuizDTO user_QuizDTO)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userResult = await _tokenController.GetUserRole(token);

            if (userResult == null)
            {
                return Unauthorized("Invalid Token");
            }
            else if (userResult.role.Role != "Teacher" && userResult.role.Role != "Administrator")
            {
                return Unauthorized("Unauthorized.");
            }

            var quiz = await _context.Quizs.FindAsync(user_QuizDTO.QuizID);
            if (quiz == null)
            {
                return NotFound("Quiz not found.");
            }

            var user = await _context.Users.FindAsync(user_QuizDTO.UserID);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            User_Quiz user_Quiz = new()
            {
                quiz = quiz,
                user = user,
            };

            _context.User_Quiz.Add(user_Quiz);
            await _context.SaveChangesAsync();

            var User_QuizInfoDTO = await _context.User_Quiz
                .Include(uq => uq.quiz)
                .Include(uq => uq.quiz.category)
                .Include(uq => uq.quiz.education)
                .Include(uq => uq.quiz.difficulty)
                .Include(uq => uq.quiz.creator)
                .Include(uq => uq.user)
                .ThenInclude(uq => uq.role)
                .Where(uq => uq.quiz.ID == user_Quiz.quiz.ID)
                .Where(uq => uq.user.ID == user_Quiz.user.ID)
                .Select(uq => new User_QuizInfoDTO
                {
                    Completed = false,
                    Results = user_QuizDTO.Results,
                    QuizEndDate = user_QuizDTO.QuizEndDate ?? DateTime.UtcNow.AddDays(1),
                    TimeUsed = user_QuizDTO.TimeUsed,
                    User = new UserDTO
                    {
                        email = uq.user.Email,
                        ID = uq.user.ID,
                        role = uq.user.role.Role,
                        username = uq.user.Username,
                    },
                    Quiz = new QuizDTO
                    {
                        ID = uq.quiz.ID,
                        Category = uq.quiz.category.Category,
                        Creator = uq.quiz.creator.Username,
                        Difficulty = uq.quiz.difficulty.Difficulty,
                        Education = uq.quiz.education.Education,
                        Title = uq.quiz.Title,
                        //could add question count but as this is for adding a student to a quiz i dont see the need
                    },
                }).FirstOrDefaultAsync();

            return CreatedAtAction("GetUser_Quiz", User_QuizInfoDTO);
        }

        // DELETE: api/User_Quiz/5/2
        [HttpDelete("{quizId}/{userId}")]
        public async Task<IActionResult> DeleteUser_Quiz(int quizId, int userId)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userResult = await _tokenController.GetUserRole(token);

            if (userResult == null)
            {
                return Unauthorized("Invalid Token");
            }
            else if (userResult.role.Role != "Administrator")
            {
                return Unauthorized("Unauthorized.");
            }

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
