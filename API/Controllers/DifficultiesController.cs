namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DifficultiesController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly TokenController _tokenController;

        public DifficultiesController(AppDBContext context)
        {
            _context = context;
            _tokenController = new TokenController(context);
        }

        // GET: api/Difficulties
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DifficultiesDTO>>> GetDifficulties()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userResult = await _tokenController.GetUserRole(token);

            if (userResult == null)
            {
                return Unauthorized("Invalid Token");
            }
            return await _context.Difficulties.Select(d => new DifficultiesDTO
            {
                ID = d.ID,
                Difficulty = d.Difficulty,
            }).ToListAsync();
        }

        // GET: api/Difficulties/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DifficultiesDTO>> GetDifficulties(int id)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userResult = await _tokenController.GetUserRole(token);

            if (userResult == null)
            {
                return Unauthorized("Invalid Token");
            }

            var difficulties = await _context.Difficulties.Where(d => d.ID == id).Select(d => new DifficultiesDTO
            {
                ID = d.ID,
                Difficulty = d.Difficulty,
            }).FirstOrDefaultAsync();

            if (difficulties == null)
            {
                return NotFound();
            }

            return difficulties;
        }

        // PUT: api/Difficulties/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDifficulties(int id, DifficultiesDTO difficulties)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userResult = await _tokenController.GetUserRole(token);

            if (userResult == null)
            {
                return Unauthorized("Invalid Token");
            }
            else if (userResult.role.Role != "Administrator")
            {
                return Unauthorized("Unauthorized");
            }

            if (id != difficulties.ID)
            {
                return BadRequest();
            }

            Difficulties difficulty = new()
            {
                ID = id,
                Difficulty = difficulties.Difficulty,
            };

            _context.Entry(difficulty).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DifficultiesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("sucessfully updated");
        }

        // POST: api/Difficulties
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DifficultiesDTO>> PostDifficulties(DifficultiesCreateDTO difficultiesCreateDTO)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userResult = await _tokenController.GetUserRole(token);

            if (userResult == null)
            {
                return Unauthorized("Invalid Token");
            }
            else if (userResult.role.Role != "Administrator")
            {
                return Unauthorized("Unauthorized");
            }

            Difficulties difficulties = new()
            {
                Difficulty = difficultiesCreateDTO.Difficulty,
            };

            _context.Difficulties.Add(difficulties);
            await _context.SaveChangesAsync();

            DifficultiesDTO difficultiesDTO = new()
            {
                Difficulty = difficulties.Difficulty,
                ID = difficulties.ID,
            };

            return CreatedAtAction("GetDifficulties", new { id = difficulties.ID }, difficultiesDTO);
        }

        // DELETE: api/Difficulties/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDifficulties(int id)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userResult = await _tokenController.GetUserRole(token);

            if (userResult == null)
            {
                return Unauthorized("Invalid Token");
            }
            else if (userResult.role.Role != "Administrator")
            {
                return Unauthorized("Unauthorized");
            }

            var difficulties = await _context.Difficulties.FindAsync(id);
            if (difficulties == null)
            {
                return NotFound();
            }

            _context.Difficulties.Remove(difficulties);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DifficultiesExists(int id)
        {
            return _context.Difficulties.Any(e => e.ID == id);
        }
    }
}
