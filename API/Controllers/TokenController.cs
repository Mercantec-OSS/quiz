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
        public async Task<User> GetUserRole(string token)
        {
            var isValid = await _context.Token.AnyAsync(t => t.JWTToken == token && t.ExpiresAt >= DateTime.UtcNow);

            if (!isValid)
            {
                return null; // Indicates an invalid token.
            }

            var user = await _context.Token
                .Include(t => t.user)
                .ThenInclude(u => u.role)
                .Where(t => t.JWTToken == token)
                .Select(t => t.user)
                .FirstOrDefaultAsync();

            return user; // Null if not found.
        }
    }
}
