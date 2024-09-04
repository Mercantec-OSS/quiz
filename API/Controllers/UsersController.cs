namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly IConfiguration _configuration;

        public UsersController(AppDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/Users
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [HttpPost("signUp")]
        public async Task<ActionResult<User>> PostUser(SignUpRequest userSignUp)
        {
            var HashedPassword = BCrypt.Net.BCrypt.HashPassword(userSignUp.Password);

            User user = new()
            {
                Email = userSignUp.Email,
                Username = userSignUp.Username,
                HashedPassword = HashedPassword,
                Salt = HashedPassword.Substring(0, 29),

                CreatedAt = DateTime.UtcNow.AddHours(2),
                UpdatedAt = DateTime.UtcNow.AddHours(2)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // POST: api/Users
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginRequest login)
        {
            User? userFinder = await _context.Users.FirstOrDefaultAsync(item => item.Email == login.Email);

            if (userFinder == null || !BCrypt.Net.BCrypt.Verify(login.Password, userFinder.HashedPassword))
            {
                return BadRequest("Try again");
            }

            var token = GenerateJwtToken(userFinder);

            return Ok(token);
        }

        // JWT Token used to login users and how long their session is valid for
        private string GenerateJwtToken(User user)
        {

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                new Claim(ClaimTypes.Name, user.Username),

                new Claim(ClaimTypes.SerialNumber, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"] ?? Environment.GetEnvironmentVariable("Key")));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(

            _configuration["JwtSettings:Issuer"] ?? Environment.GetEnvironmentVariable("Issuer"),

            _configuration["JwtSettings:Audience"] ?? Environment.GetEnvironmentVariable("Audience"),

            claims,

            expires: DateTime.Now.AddDays(1),

            signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
