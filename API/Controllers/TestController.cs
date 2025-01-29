namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() 
        {
            return Ok("If you get this its working.");
        }

        [HttpGet("Ldap")]
        public IActionResult GetLdap() 
        {
            string connectionString = Environment.GetEnvironmentVariable("AD_CONNECTION_STRING") ?? "";
            if (connectionString != "") 
            { 
            return Ok("the sting exist");
            }
            return Ok("string is missing");
        }
    }
}
