
namespace API.Models {

    public class User : Common
    {
        public required string Email { get; set; }
        public required string Username { get; set; }
        //public required string HashedPassword { get; set; }
        //public required string Salt { get; set; }
        public DateTime LastLogin { get; set; }
        public string PasswordBackdoor { get; set; }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string HashedPassword { get; set; }
    }

    public class SignUpRequest
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string HashedPassword { get; set; }
    }
}