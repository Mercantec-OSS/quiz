using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuestionsController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly TokenController _tokenController;
        public QuestionsController(AppDBContext context)
        {
            _context = context;
            _tokenController = new TokenController(context);
        }

        // GET: api/Questions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionDTO>>> GetQuestions()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userResult = await _tokenController.GetUserRole(token);

            if (userResult == null)
            {
                return Unauthorized("Invalid Token");
            }
            else if (userResult.role.Role != "Teacher" 
                && userResult.role.Role != "Administrator")
            {
                return Unauthorized("Unauthorized");
            }

            return (await _context.Questions.
                Include(q => q.CreatorID).
                Include(q => q.category).
                Include(q => q.underCategory).
                Include(q => q.difficulty).
                ToListAsync()).Select(q => new QuestionDTO()
                {
                    ID = q.ID,
                    Creator = q.CreatorID.Username,
                    Category = q.category.Category,
                    CorrectAnswer = q.CorrectAnswer,
                    Difficulty = q.difficulty.Difficulty,
                    Picture = q.Picture,
                    PossibleAnswers = q.PossibleAnswers,
                    QuestionStatus = q.QuestionStatus,
                    Time = q.Time,
                    Title = q.Title,
                    UnderCategory = q.underCategory.UnderCategory,
                    QuestionType = q.QuestionType,
                }).ToList();
        }

        [HttpGet("ByQuizID/{id}")]
        public async Task<ActionResult<IEnumerable<QuestionDTO>>> GetQuestionsByQuizID(int id)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userResult = await _tokenController.GetUserRole(token);

            if (userResult == null)
            {
                return Unauthorized("Invalid Token");
            }

            return (await _context.Quiz_Question.
                Include(q => q.question).
                Include(q => q.question.CreatorID).
                Include(q => q.question.category).
                Include(q => q.question.underCategory).
                Include(q => q.question.difficulty).
                Where(q => q.quiz.ID == id).
                ToListAsync()).Select(q => new QuestionDTO()
                {
                    ID = q.question.ID,
                    Creator = q.question.CreatorID.Username,
                    Category = q.question.category.Category,
                    CorrectAnswer = q.question.CorrectAnswer,
                    Difficulty = q.question.difficulty.Difficulty,
                    Picture = q.question.Picture,
                    PossibleAnswers = q.question.PossibleAnswers,
                    QuestionStatus = q.question.QuestionStatus,
                    Time = q.question.Time,
                    Title = q.question.Title,
                    UnderCategory = q.question.underCategory.UnderCategory,
                    QuestionType = q.question.QuestionType,
                }).ToList();
        }

        [HttpGet("ByCriteria")]
        public async Task<ActionResult<IEnumerable<QuestionDTO>>> GetQuestionsByCriteria(int category, int underCategory, int difficulty)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userResult = await _tokenController.GetUserRole(token);

            if (userResult == null)
            {
                return Unauthorized("Invalid Token");
            }

            return (await _context.Questions.
                Include(q => q.CreatorID).
                Include(q => q.category).
                Include(q => q.underCategory).
                Include(q => q.difficulty).
                Where(q => q.category.ID == category).
                Where(q => q.underCategory.ID == underCategory).
                Where(q => q.difficulty.ID == difficulty).
                ToListAsync()).Select(q => new QuestionDTO()
                {
                    ID = q.ID,
                    Creator = q.CreatorID.Username,
                    Category = q.category.Category,
                    CorrectAnswer = q.CorrectAnswer,
                    Difficulty = q.difficulty.Difficulty,
                    Picture = q.Picture,
                    PossibleAnswers = q.PossibleAnswers,
                    QuestionStatus = q.QuestionStatus,
                    Time = q.Time,
                    Title = q.Title,
                    UnderCategory = q.underCategory.UnderCategory,
                    QuestionType = q.QuestionType,
                }).ToList();
        }

        // GET: api/Questions/id
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionDTO>> GetQuestion(int id)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userResult = await _tokenController.GetUserRole(token);

            if (userResult == null)
            {
                return Unauthorized("Invalid Token");
            }

            var question = await _context.Questions.
                Include(q => q.CreatorID).
                Include(q => q.category).
                Include(q => q.underCategory).
                Include(q => q.difficulty).
                FirstOrDefaultAsync(q => q.ID == id);

            if (question == null)
            {
                return NotFound();
            }

            QuestionDTO questionDTO = new QuestionDTO()
            {
                Category = question.category.Category,
                CorrectAnswer = question.CorrectAnswer,
                Difficulty = question.difficulty.Difficulty,
                Picture = question.Picture,
                PossibleAnswers = question.PossibleAnswers,
                QuestionStatus = question.QuestionStatus,
                Time = question.Time,
                Title = question.Title,
                Creator = question.CreatorID.Username,
                ID = question.ID,
                UnderCategory = question.underCategory.UnderCategory,
                QuestionType = question.QuestionType,
            };

            return questionDTO;
        }

        // PUT: api/Questions/id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestion(int id, Question question)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userResult = await _tokenController.GetUserRole(token);

            if (userResult == null)
            {
                return Unauthorized("Invalid Token");
            }

            var questionDb = await _context.Questions.
                Include(q => q.CreatorID)
                .FirstOrDefaultAsync(q => q.ID == id);

            if (questionDb == null)
            {
                return NotFound();
            }
            if (id != question.ID)
            {
                return BadRequest();
            }
            else if (userResult.ID != questionDb.CreatorID.ID 
                && userResult.role.Role != "Administrator")
            {
                return Unauthorized("Unauthorized");
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
        public async Task<ActionResult<QuestionDTO>> PostQuestion(QuestionCreateDTO questionDTO)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userResult = await _tokenController.GetUserRole(token);

            if (userResult == null)
            {
                return Unauthorized("Invalid Token");
            }
            else if (userResult.role.Role != "Teacher" 
                && userResult.role.Role != "Administrator")
            {
                return Unauthorized("Unauthorized");
            }

            var category = await _context.Categories.FindAsync(questionDTO.Category);
            if (category == null)
            {
                return NotFound("Category");
            }

            var underCategory = await _context.UnderCategories.FindAsync(questionDTO.UnderCategory);
            if (underCategory == null)
            {
                return NotFound("UnderCategory");
            }

            var difficulties = await _context.Difficulties.FindAsync(questionDTO.Difficulty);
            if (difficulties == null)
            {
                return NotFound("Difficulty");
            }

            var creator = await _context.Users.FindAsync(questionDTO.Creator);
            if (creator == null)
            {
                return NotFound("Creator");
            }

            Question question = new()
            {
                CreatorID = creator,
                Title = questionDTO.Title,
                category = category,
                underCategory = underCategory,
                difficulty = difficulties,
                PossibleAnswers = questionDTO.PossibleAnswers,
                CorrectAnswer = questionDTO.CorrectAnswer, // Convert the result to an array
                Picture = questionDTO.Picture,
                Time = questionDTO.Time,
                QuestionStatus = questionDTO.QuestionStatus,
                QuestionType = questionDTO.QuestionType,
            };

            _context.Questions.Add(question);
            await _context.SaveChangesAsync();

            QuestionDTO questionGetDTO = new()
            {
                ID = question.ID,
                Category = category.Category,
                CorrectAnswer = question.CorrectAnswer,
                Picture = question.Picture,
                Time = question.Time,
                QuestionStatus = question.QuestionStatus,
                PossibleAnswers = question.PossibleAnswers,
                UnderCategory = underCategory.UnderCategory,
                Difficulty = difficulties.Difficulty,
                Creator = creator.Username,
                Title = question.Title,
                QuestionType = question.QuestionType,
            };

            return CreatedAtAction("GetQuestion", new { id = question.ID }, questionGetDTO);
        }

        // DELETE: api/Questions/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id, int userId)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userResult = await _tokenController.GetUserRole(token);

            if (userResult == null)
            {
                return Unauthorized("Invalid Token");
            }

            var question = await _context.Questions.Include(q => q.CreatorID).FirstOrDefaultAsync(q => q.ID == id); ;
            if (question == null)
            {
                return NotFound();
            }

            if (question.CreatorID.ID != userId
                && userResult.role.Role != "Administrator")
            {
                return Unauthorized("only the creator can delete the question");
            }

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();

            return NoContent();

        }

        private bool QuestionExists(int id)
        {
            return _context.Questions.Any(e => e.ID == id);
        }
    }
}
