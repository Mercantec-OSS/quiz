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
        public async Task<ActionResult<IEnumerable<User_Quiz>>> GetUser_Quiz()
        {
            return await _context.User_Quiz.ToListAsync();
        }

        // GET: api/User_Quiz/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User_Quiz>> GetUser_Quiz(int id)
        {
            var user_Quiz = await _context.User_Quiz.FindAsync(id);

            if (user_Quiz == null)
            {
                return NotFound();
            }

            return user_Quiz;
        }

        // PUT: api/User_Quiz/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser_Quiz(int id, User_Quiz user_Quiz)
        {
            if (id != user_Quiz.ID)
            {
                return BadRequest();
            }

            _context.Entry(user_Quiz).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!User_QuizExists(id))
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

        // POST: api/User_Quiz
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User_Quiz>> PostUser_Quiz(User_QuizDTO user_QuizDTO)
        {
            User_Quiz user_Quiz = new()
            {
                QuizID = user_QuizDTO.QuizID,
                UserID = user_QuizDTO.UserID,
            };

            _context.User_Quiz.Add(user_Quiz);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser_Quiz", new { id = user_Quiz.ID }, user_Quiz);
        }

        // DELETE: api/User_Quiz/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser_Quiz(int id)
        {
            var user_Quiz = await _context.User_Quiz.FindAsync(id);
            if (user_Quiz == null)
            {
                return NotFound();
            }

            _context.User_Quiz.Remove(user_Quiz);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool User_QuizExists(int id)
        {
            return _context.User_Quiz.Any(e => e.ID == id);
        }
    }
}
