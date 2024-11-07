using Microsoft.AspNetCore.Authorization;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using NuGet.Protocol.Plugins;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly IConfiguration _configuration;
        private readonly TokenController _tokenController;

        public UsersController(AppDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _tokenController = new TokenController(context);
        }

        // GET: api/Users

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
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

            return (await _context.Users
                .Include(u => u.role)
                .ToListAsync())
                .Select(u => new UserDTO()
            {
                ID = u.ID,
                username = u.Username,
                email = u.Email,
                role = u.role.Role,
            }).ToList();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userResult = await _tokenController.GetUserRole(token);

            if (userResult == null)
            {
                return Unauthorized("Invalid Token");
            }

            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            UserDTO userDTO = new UserDTO
            {
                ID = user.ID,
                username = user.Username,
                email = user.Email,
                role = user.role.Role,
            };

            return userDTO;
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UpdateUserDTO user)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userResult = await _tokenController.GetUserRole(token);

            if (userResult == null)
            {
                return Unauthorized("Invalid Token");
            }
            else if (userResult.ID != id && userResult.role.Role != "Administrator")
            {
                return Unauthorized("You can only edit your own profile.");
            }

            if (id != user.ID)
            {
                return BadRequest();
            }
            if (!UserExists(id))
            {
                return NotFound();
            }
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Could not save changes");
            }

            return StatusCode(201, "Sucess");
        }

        // POST: api/Users
        [AllowAnonymous]
        [HttpPost("signUp")]
        public async Task<ActionResult<UserDTO>> PostUser(SignUpRequest userSignUp)
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
            if(await _context.Users.
                Include(item => item.role).
                FirstOrDefaultAsync(item => item.Email == userSignUp.email) != null)
            {
                return BadRequest("Email allready excist.");
            }
            if(await _context.Users.
            Include(item => item.role).
                FirstOrDefaultAsync(item => item.Username == userSignUp.username) != null)
            {
                return BadRequest("Username allready excist.");
            }

            var HashedPassword = BCrypt.Net.BCrypt.HashPassword(userSignUp.password);

            var Roles = await _context.Roles.FindAsync(1);
            if (Roles == null)
            {
                return NotFound();
            }

            User user = new()
            {
                Email = userSignUp.email,
                Username = userSignUp.username,
                HashedPassword = HashedPassword,
                role = Roles,
                Salt = HashedPassword.Substring(0, 29),
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            var Token = GenerateJwtToken(user);
            _context.Token.Add(Token);
            await _context.SaveChangesAsync();

            return Ok(new UserDTO()
            {
                ID = user.ID,
                email = user.Email,
                username = user.Username,
                role = user.role.Role,
                token = Token.JWTToken
            });
        }

        // POST: api/Users
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> LoginUser(LoginRequest login)
        {
            User? userFinder = await _context.Users.
                Include(item => item.role).
                FirstOrDefaultAsync(item => item.Email == login.email);

            if (userFinder == null || !BCrypt.Net.BCrypt.Verify(login.password, userFinder.HashedPassword))
            {
                return BadRequest("Incorrect username or password");
            }

            var Token = GenerateJwtToken(userFinder);
            _context.Token.Add(Token);
            await _context.SaveChangesAsync();

            return Ok(new UserDTO()
            {
                ID = userFinder.ID,
                email = userFinder.Email,
                username = userFinder.Username,
                role = userFinder.role.Role,
                token = Token.JWTToken
            });
        }


        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userResult = await _tokenController.GetUserRole(token);

            if (userResult == null)
            {
                return Unauthorized("Invalid Token");
            }
            else if (userResult.role.Role != "Administrator")
            {
                return Unauthorized("Unauthorized.");
            }

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
            return _context.Users.Any(e => e.ID == id);
        }

        // JWT Token used to login users and how long their session is valid for
        private Token GenerateJwtToken(User user)
        {

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                new Claim(ClaimTypes.Name, user.Username),

                new Claim(ClaimTypes.SerialNumber, user.ID.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"] ?? Environment.GetEnvironmentVariable("Key")));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(

            _configuration["JwtSettings:Issuer"] ?? Environment.GetEnvironmentVariable("Issuer"),

            _configuration["JwtSettings:Audience"] ?? Environment.GetEnvironmentVariable("Audience"),

            claims,

            expires: DateTime.Now.AddDays(1),

            signingCredentials: creds);

            Token userToken = new()
            {
                JWTToken = new JwtSecurityTokenHandler().WriteToken(token),
                user = user
            };

            return userToken;
        }
    }
}
