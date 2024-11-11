namespace API.Models
{
    public class User
    {
        [Key]

        // This is the ID of the User
        public int ID { get; set; }

        // This is the sign up/ login requirements
        public string Email { get; set; }
        public string Username { get; set; }

        // This is used for security
        public string? HashedPassword { get; set; }
        public string Salt { get; set; }

        // This is used to check whenever the user was last loged in
        public DateTime LastLogin { get; set; }

        // This is used to check the users
        [ForeignKey("Role")]
        public virtual Roles role { get; set; }
    }


    public class UserDTO
    {
        public int ID { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string role { get; set; }
        public string? token { get; set; }
    }


    public class LoginRequest
    {
        // This is the login requirements
        public string username { get; set; }
        public string password { get; set; }
    }

    public class SignUpRequest
    {
        public string username { get; set; }
        public string password { get; set; }

    }

    public class UpdateUserDTO
    {
        public int ID { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int role { get; set; }
    }
}
