namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UnderCategoriesController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly TokenController _tokenController;

        public UnderCategoriesController(AppDBContext context)
        {
            _context = context;
            _tokenController = new TokenController(context);
        }

        // GET: api/UnderCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnderCategories>>> GetUnderCategories()
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

            return await _context.UnderCategories.ToListAsync();
        }

        // GET: api/UnderCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UnderCategories>> GetUnderCategories(int id)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userResult = await _tokenController.GetUserRole(token);

            if (userResult == null)
            {
                return Unauthorized("Invalid Token");
            }

            var underCategories = await _context.UnderCategories.FindAsync(id);

            if (underCategories == null)
            {
                return NotFound();
            }

            return underCategories;
        }

        [HttpGet("CategoryID/{id}")]
        public async Task<ActionResult<IEnumerable<UnderCategoryGetDTO>>> GetUnderCategoriesByCategoryID(int id)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userResult = await _tokenController.GetUserRole(token);

            if (userResult == null)
            {
                return Unauthorized("Invalid Token");
            }

            var underCategories = await _context.UnderCategories
                .Include(u => u.category)
                .Where(u => u.category.ID == id)
                .Select(u => new UnderCategoryGetDTO
                {
                    ID = u.ID,
                    UnderCategory = u.UnderCategory,
                })
                .ToListAsync();
            if (underCategories == null)
            {
                return NotFound();
            }

            return underCategories;
        }

        // PUT: api/UnderCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUnderCategories(int id, UnderCategoryGetDTO underCategoriesDTO)
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

            if (id != underCategoriesDTO.ID)
            {
                return BadRequest();
            }

            UnderCategories underCategories = new()
            {
                ID = id,
                UnderCategory = underCategoriesDTO.UnderCategory,
            };

            _context.Entry(underCategories).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UnderCategoriesExists(id))
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

        // POST: api/UnderCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UnderCategories>> PostUnderCategories(UnderCategoryCreateDTO underCategoriesDTO)
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

            var category = await _context.Categories.FindAsync(underCategoriesDTO.CategoryID);
            if (category == null)
            {
                return NotFound();
            }

            UnderCategories underCategories = new()
            {
                UnderCategory = underCategoriesDTO.UnderCategory,
                category = category
            };

            _context.UnderCategories.Add(underCategories);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUnderCategories", new { id = underCategories.ID }, underCategories);
        }

        // DELETE: api/UnderCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnderCategories(int id)
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

            var underCategories = await _context.UnderCategories.FindAsync(id);
            if (underCategories == null)
            {
                return NotFound();
            }

            _context.UnderCategories.Remove(underCategories);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UnderCategoriesExists(int id)
        {
            return _context.UnderCategories.Any(e => e.ID == id);
        }
    }
}
