using SharedModels.DTO;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly TokenController _tokenController;

        public CategoriesController(AppDBContext context)
        {
            _context = context;
            _tokenController = new TokenController(context);
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userResult = await _tokenController.GetUserRole(token);

            if (userResult == null)
            {
                return Unauthorized("Invalid Token");
            }
            return await _context.Categories.Include(c => c.education).Select(c => new CategoryDTO
            {
                ID = c.ID,
                Category = c.Category,
                Education = c.education.Education
            }).ToListAsync();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategories(int id)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userResult = await _tokenController.GetUserRole(token);

            if (userResult == null)
            {
                return Unauthorized("Invalid Token");
            }
            var categories = await _context.Categories
                .Include(c => c.education)
                .Where(c => c.ID == id)
                .Select(c => new CategoryDTO
                {
                    ID = c.ID,
                    Category = c.Category,
                    Education = c.education.Education
                }).FirstOrDefaultAsync(); ;

            if (categories == null)
            {
                return NotFound();
            }

            return categories;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategories(int id, CategoryDTO categories)
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

            if (id != categories.ID)
            {
                return BadRequest();
            }

            var education = await _context.Educations.Where(e => e.Education == categories.Education).FirstOrDefaultAsync();
            if (education == null)
            {
                return NotFound("Education");
            }

            Categories category = new()
            {
                ID = id,
                Category = categories.Category,
                education = education,
            };

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriesExists(id))
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

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> PostCategories(CategoryCreateDTO categoriesDTO)
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

            var education = await _context.Educations.FindAsync(categoriesDTO.EducationID);
            if (education == null)
            {
                return NotFound("Education");
            }

            Categories categories = new()
            {
                Category = categoriesDTO.Category,
                education = education,
            };

            _context.Categories.Add(categories);
            await _context.SaveChangesAsync();

            CategoryDTO response = new()
            {
                ID = categories.ID,
                Category = categories.Category,
                Education = education.Education,
            };

            return CreatedAtAction("GetCategories", new { id = categories.ID }, response);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategories(int id)
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

            var categories = await _context.Categories.FindAsync(id);
            if (categories == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(categories);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoriesExists(int id)
        {
            return _context.Categories.Any(e => e.ID == id);
        }
    }
}
