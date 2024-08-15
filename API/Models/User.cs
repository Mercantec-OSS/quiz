namespace API.Models {

    public class User : Common
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
        public DateTime LastLogin { get; set; }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class SignUpRequest
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

    }
}