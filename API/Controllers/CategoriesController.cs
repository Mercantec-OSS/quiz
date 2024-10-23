namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDBContext _context;

        public CategoriesController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categories>>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Categories>> GetCategories(int id)
        {
            var categories = await _context.Categories.FindAsync(id);

            if (categories == null)
            {
                return NotFound();
            }

            return categories;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategories(int id, Categories categories)
        {
            if (id != categories.ID)
            {
                return BadRequest();
            }

            _context.Entry(categories).State = EntityState.Modified;

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

            return NoContent();
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Categories>> PostCategories(CategoryCreateDTO categoriesDTO)
        {
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

            foreach (var underCategory in categoriesDTO.UnderCategories)
            {
                UnderCategories underCategories = new()
                {
                    category = categories,
                    UnderCategory = underCategory
                };
                _context.UnderCategories.Add(underCategories);
            }
            await _context.SaveChangesAsync();

            var response = new
            {
                Message = $"1 Category and {categoriesDTO.UnderCategories.Length} under Categories successfully created.", // Custom message
                CreatedCount = categoriesDTO.UnderCategories.Length + 1  // Number of created categories
            };

            return CreatedAtAction("GetCategories", new { count = categoriesDTO.UnderCategories.Length }, response);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategories(int id)
        {
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
