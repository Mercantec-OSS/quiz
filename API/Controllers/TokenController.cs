namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly AppDBContext _context;

        public TokenController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet("UserAuth")]
        public async Task<ActionResult<User>> GetUserRole(string token)
        {
            // Check if the token is valid
            var isValid = await _context.Token.AnyAsync(t => t.JWTToken == token && t.ExpiresAt >= DateTime.UtcNow);

            if (!isValid)
            {
                return Unauthorized("Invalid token.");
            }

            // Retrieve the user based on the token
            var user = await _context.Token
                .Include(t => t.user)
                .ThenInclude(u => u.role)
                .Where(t => t.JWTToken == token)
                .Select(t => t.user)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user);
        }
    }
}
