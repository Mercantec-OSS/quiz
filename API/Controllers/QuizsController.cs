﻿using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuizsController(AppDBContext context) : ControllerBase
    {
        private readonly AppDBContext _context = context;
        private readonly TokenController _tokenController = new(context);
        Quiz_QuestionController quiz_QuestionController = new(context);

        // GET: api/Quizs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuizDTO>>> GetQuizs()
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

            var quizs = (await _context.Quizs.
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
                    Title = q.Title,
                }).ToList();

            foreach (var quiz in quizs)
            {
                quiz.QestionsAmount = await _context.Quiz_Question.
                    Include(q => q.quiz).
                    Where(q => q.quiz.ID == quiz.ID).
                    CountAsync();
            }

            return quizs;
        }

        // GET: api/Quizs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuizDTO>> GetQuiz(int id)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userResult = await _tokenController.GetUserRole(token);

            if (userResult == null)
            {
                return Unauthorized("Invalid Token");
            }

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
                Title = quiz.Title,
            };


            quizDTO.QestionsAmount = await _context.Quiz_Question.
                    Include(q => q.quiz).
                    Where(q => q.quiz.ID == quiz.ID).
                    CountAsync();

            return quizDTO;
        }

        [HttpGet("QuizSearch")]
        public async Task<ActionResult<IEnumerable<QuizDTO>>> QuizSearch(string? searchWord)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userResult = await _tokenController.GetUserRole(token);

            if (userResult == null)
            {
                return Unauthorized("Invalid Token");
            }
            if (searchWord != null)
            {
                searchWord = searchWord.ToLower();
            }
            List<QuizDTO> quizs;
            if (userResult.role.Role == "Student")
            {
                quizs = await _context.User_Quiz
                    .Include(q => q.user)
                    .Include(q => q.quiz)
                    .Include(q => q.quiz.creator)
                    .Include(q => q.quiz.category)
                    .Include(q => q.quiz.difficulty)
                    .Include(q => q.quiz.education)
                    .Where(q => q.user.ID == userResult.ID)
                    .Where(q => q.quiz.Title.ToLower().Contains(searchWord ?? ""))
                    .Select(q => new QuizDTO()
                    {
                        ID = q.quiz.ID,
                        Category = q.quiz.category.Category,
                        Creator = q.quiz.creator.Username,
                        Difficulty = q.quiz.difficulty.Difficulty,
                        Education = q.quiz.education.Education,
                        Title = q.quiz.Title,
                    })
                    .ToListAsync();
            }
            else if (userResult.role.Role == "Teacher")
            {
                quizs = await _context.Quizs
                    .Include(q => q.creator)
                    .Include(q => q.category)
                    .Include(q => q.difficulty)
                    .Include(q => q.education)
                    .Where(q => q.creator.ID == userResult.ID)
                    .Where(q => q.Title.ToLower().Contains(searchWord ?? ""))
                    .Select(q => new QuizDTO()
                    {

                        ID = q.ID,
                        Category = q.category.Category,
                        Creator = q.creator.Username,
                        Difficulty = q.difficulty.Difficulty,
                        Education = q.education.Education,
                        Title = q.Title,
                    })
                    .ToListAsync();
            }
            else
            {
                quizs = await _context.Quizs
                    .Include(q => q.creator)
                    .Include(q => q.category)
                    .Include(q => q.difficulty)
                    .Include(q => q.education)
                    .Where(q => q.Title.ToLower().Contains(searchWord ?? ""))
                    .Select(q => new QuizDTO()
                    {

                        ID = q.ID,
                        Category = q.category.Category,
                        Creator = q.creator.Username,
                        Difficulty = q.difficulty.Difficulty,
                        Education = q.education.Education,
                        Title = q.Title,
                    })
                    .ToListAsync();
            }

            foreach (var quiz in quizs)
            {
                quiz.QestionsAmount = await _context.Quiz_Question.
                    Include(q => q.quiz).
                    Where(q => q.quiz.ID == quiz.ID).
                    CountAsync();
            }

            return Ok(quizs);
        }

        // GET: api/Quizs/5
        [HttpGet("ByCreater/{id}")]
        public async Task<ActionResult<IEnumerable<QuizDTO>>> GetQuizByCreater(int id)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userResult = await _tokenController.GetUserRole(token);

            if (userResult == null)
            {
                return Unauthorized("Invalid Token");
            }
            else if (userResult.ID != id && userResult.role.Role != "Administrator")
            {
                return Unauthorized("Unauthorized.");
            }

            var quizs = await _context.Quizs.
                Include(q => q.creator).
                Include(q => q.category).
                Include(q => q.difficulty).
                Include(q => q.education).
                Where(q => q.creator.ID == id)
                .Select(q => new QuizDTO()
                {
                    ID = q.ID,
                    Category = q.category.Category,
                    Creator = q.creator.Username,
                    Difficulty = q.difficulty.Difficulty,
                    Education = q.education.Education,
                    Title = q.Title,
                }).ToListAsync();

            if (quizs == null)
            {
                return NotFound();
            }

            foreach (var quiz in quizs)
            {
                quiz.QestionsAmount = await _context.Quiz_Question.
                    Include(q => q.quiz).
                    Where(q => q.quiz.ID == quiz.ID).
                    CountAsync();
            }

            return Ok(quizs);
        }

        // PUT: api/Quizs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuiz(int id, QuizCreateRandomDTO quizDTO)
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

            var quiz = await _context.Quizs.FindAsync(id);

            if (quiz == null)
            {
                return NotFound();
            }

            quiz.Title = quizDTO.Title;

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

        [HttpPost("Setup-Quiz/Custom")]
        public async Task<ActionResult<QuizDTO>> PostCustomQuiz(QuizCreateCustomDTO quizDTO)
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

            var education = await _context.Educations.FindAsync(quizDTO.EducationID);
            if (education == null)
            {
                return NotFound("Education not found");
            }

            var category = await _context.Categories.FindAsync(quizDTO.CategoryID);
            if (category == null)
            {
                return NotFound("Category not found");
            }

            var difficulties = await _context.Difficulties.FindAsync(quizDTO.DifficultyID);
            if (difficulties == null)
            {
                return NotFound("Difficulty not found");
            }

            var creator = await _context.Users.FindAsync(quizDTO.CreatorID);
            if (creator == null)
            {
                return NotFound("User not found");
            }

            foreach (int questionID in quizDTO.questions)
            {


                if (!await _context.Questions
                    .Include(q => q.underCategory)
                    .Include(q => q.difficulty)
                    .AnyAsync(q => q.ID == questionID))
                {
                    return BadRequest("1 or more questions was not found");
                }
            }

            Quiz quiz = new()
            {
                creator = creator,
                Title = quizDTO.Title,
                education = education,
                category = category,
                difficulty = difficulties,
            };

            _context.Quizs.Add(quiz);
            await _context.SaveChangesAsync();

            

            foreach (int questionID in quizDTO.questions)
            {
                Quiz_QuestionDTO qQ = new()
                {
                    QuestionID = questionID,
                    QuizID = quiz.ID
                };

                var response = await quiz_QuestionController.PostQuiz_Question(qQ, token);
                if (response.Result is NotFoundResult)
                {
                    return NotFound("Something went wrong adding the questions to the quiz");
                }
            }

            QuizDTO newQuizDTO = new QuizDTO()
            {
                Category = quiz.category.Category,
                Creator = quiz.creator.Username,
                Difficulty = quiz.difficulty.Difficulty,
                Education = quiz.education.Education,
                ID = quiz.ID,
                Title = quiz.Title,
            };

            return CreatedAtAction("GetQuiz", new { id = quiz.ID }, newQuizDTO);
        }

        // POST: api/Quizs/Random
        [HttpPost("Setup-Quiz/Random")]
        public async Task<ActionResult<QuizDTO>> PostQuiz(QuizCreateRandomDTO quizDTO)
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

            var education = await _context.Educations.FindAsync(quizDTO.EducationID);
            if (education == null)
            {
                return NotFound("Education not found");
            }

            var category = await _context.Categories.FindAsync(quizDTO.CategoryID);
            if (category == null)
            {
                return NotFound("Category not found");
            }

            var difficulties = await _context.Difficulties.FindAsync(quizDTO.DifficultyID);
            if (difficulties == null)
            {
                return NotFound("Difficulty not found");
            }

            var creator = await _context.Users.FindAsync(quizDTO.CreatorID);
            if (creator == null)
            {
                return NotFound("User not found");
            }

            foreach (QuestionAmount questionAmount in quizDTO.questions)
            {
                int questions;
                if (questionAmount.UnderCategoryID != null)
                {
                    questions = await _context.Questions
                        .Include(q => q.underCategory)
                        .Include(q => q.difficulty)
                        .Where(q => q.underCategory.ID == questionAmount.UnderCategoryID)
                        .Where(q => q.difficulty.ID == questionAmount.DifficultyID)
                        .CountAsync();
                    if (questions < questionAmount.Amount)
                    {
                        var categoryName = await _context.UnderCategories
                            .FindAsync(questionAmount.CategoryID);
                        if (categoryName == null)
                        {
                            return NotFound("1 or more UnderCategories does not exist");
                        }
                        return NotFound($"Not enough questions of type: {categoryName.UnderCategory} on that difficulty");
                    }
                }
                else
                {
                    questions = await _context.Questions
                        .Include(q => q.category)
                        .Include(q => q.difficulty)
                        .Where(q => q.category.ID == questionAmount.CategoryID)
                        .Where(q => q.difficulty.ID == questionAmount.DifficultyID)
                        .CountAsync();
                    if (questions < questionAmount.Amount)
                    {
                        var categoryName = await _context.Categories
                            .FindAsync(questionAmount.CategoryID);
                        if (categoryName == null)
                        {
                            return NotFound("1 or more categories does not exist");
                        }
                        return NotFound($"Not enough questions of type: {categoryName.Category} on that difficulty");
                    }
                }
            }

            Quiz quiz = new()
            {
                creator = creator,
                Title = quizDTO.Title,
                education = education,
                category = category,
                difficulty = difficulties,
            };

            _context.Quizs.Add(quiz);
            await _context.SaveChangesAsync();
            List<Quiz_QuestionDTO> allQuestions = new List<Quiz_QuestionDTO>();
            foreach (QuestionAmount questionAmount in quizDTO.questions)
            {
                List<Quiz_QuestionDTO> questions = new();
                if (questionAmount.UnderCategoryID != null)
                {
                    questions.AddRange((await _context.Questions
                        .Include(q => q.underCategory)
                        .Include(q => q.difficulty)
                        .Where(q => q.underCategory.ID == questionAmount.UnderCategoryID)
                        .Where(q => q.difficulty.ID == questionAmount.DifficultyID)
                        .ToListAsync()).Select(q => new Quiz_QuestionDTO()
                        {
                            QuestionID = q.ID,
                            QuizID = quiz.ID
                        }));

                }
                else
                {
                    questions.AddRange((await _context.Questions
                        .Include(q => q.category)
                        .Include(q => q.difficulty)
                        .Where(q => q.category.ID == questionAmount.CategoryID)
                        .Where(q => q.difficulty.ID == questionAmount.DifficultyID)
                        .ToListAsync()).Select(q => new Quiz_QuestionDTO()
                        {
                            QuestionID = q.ID,
                            QuizID = quiz.ID
                        }));
                }
                questions = questions.OrderBy(_ => Random.Shared.Next()).ToList();
                var selectedQuestions = questions.Take(questionAmount.Amount).ToList();
                allQuestions.AddRange(selectedQuestions);
            }

            foreach (var question in allQuestions)
            {
                await quiz_QuestionController.PostQuiz_Question(question, token);
            }

            QuizDTO newQuizDTO = new QuizDTO()
            {
                Category = quiz.category.Category,
                Creator = quiz.creator.Username,
                Difficulty = quiz.difficulty.Difficulty,
                Education = quiz.education.Education,
                ID = quiz.ID,
                Title = quiz.Title,
            };

            return CreatedAtAction("GetQuiz", new { id = quiz.ID }, newQuizDTO);
        }

        // DELETE: api/Quizs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuiz(int id)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userResult = await _tokenController.GetUserRole(token);

            if (userResult == null)
            {
                return Unauthorized("Invalid Token");
            }

            var quiz = await _context.Quizs.Include(q => q.creator).FirstOrDefaultAsync(q => q.ID == id); ;
            if (quiz == null)
            {
                return NotFound();
            }
            else if (userResult.ID != quiz.creator.ID && userResult.role.Role != "Administrator")
            {
                return Unauthorized("Unauthorized.");
            }

            _context.Quizs.Remove(quiz);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
