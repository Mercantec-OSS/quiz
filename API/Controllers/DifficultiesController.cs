namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DifficultiesController : ControllerBase
    {
        private readonly AppDBContext _context;

        public DifficultiesController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Difficulties
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Difficulty>>> GetDifficulty()
        {
            return await _context.Difficulty.ToListAsync();
        }

        // GET: api/Difficulties/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Difficulty>> GetDifficulty(int id)
        {
            var difficulty = await _context.Difficulty.FindAsync(id);

            if (difficulty == null)
            {
                return NotFound();
            }

            return difficulty;
        }

        [HttpGet("Level/{Level}")]
        public async Task<ActionResult<List<Difficulty>>> GetQuestionTime(int Level)
        {
            // Casting the Level integer to Difficulty.Levels enum
            Difficulty.Levels levelEnum = (Difficulty.Levels)Level;

            var levelList = await _context.Difficulty.Where(l => l.DifficultyLevel == levelEnum).ToListAsync();

            if (levelList == null || !levelList.Any())
            {
                return NotFound();
            }

            return levelList;
        }


        // PUT: api/Difficulties/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDifficulty(int id, Difficulty difficulty)
        {
            if (id != difficulty.Id)
            {
                return BadRequest();
            }

            _context.Entry(difficulty).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DifficultyExists(id))
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

        // POST: api/Difficulties
        [HttpPost]
        public async Task<ActionResult<Difficulty>> PostDifficulty(DifficultyDTO difficultyDTO)
        {
            Difficulty difficulty = new Difficulty()
            {
                DifficultyLevel = (Difficulty.Levels)Enum.Parse(typeof(Difficulty.Levels), difficultyDTO.DifficultyLevel),
                Points = difficultyDTO.Points,
            };

            _context.Difficulty.Add(difficulty);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDifficulty", new { id = difficulty.Id }, difficulty);
        }

        // DELETE: api/Difficulties/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDifficulty(int id)
        {
            var difficulty = await _context.Difficulty.FindAsync(id);
            if (difficulty == null)
            {
                return NotFound();
            }

            _context.Difficulty.Remove(difficulty);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DifficultyExists(int id)
        {
            return _context.Difficulty.Any(e => e.Id == id);
        }
    }
}
