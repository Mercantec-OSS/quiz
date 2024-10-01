using API.Models.API.Models;
using System.Security.AccessControl;

namespace API.Models {

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
            public string Password { get; set; }

            // This is used for security
            public string HashedPassword { get; set; }
            public string Salt { get; set; }

            // This is used to check whenever the user was last loged in
            public DateTime LastLogin { get; set; }

            // This is used to check the users
            public int RolesID { get; set; }
            public virtual Roles Role { get; set; }
        }
    }

    public class UserDTO
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public Roles Role { get; set; }
    }

    public class LoginRequest
    {
        // This is the login requirements
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class SignUpRequest
    {
        // This is the sign up requirements
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

    }
}