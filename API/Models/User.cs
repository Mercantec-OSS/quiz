using API.Models.API.Models;
using System.Security.AccessControl;

namespace API.Models {

    namespace API.Models
    {
        public class User
        {
            [Key]
            public int UserID { get; set; }
            public string Email { get; set; }
            public string Username { get; set; }
            public string HashedPassword { get; set; }
            public string Salt { get; set; }
            public DateTime LastLogin { get; set; }
            public bool Role { get; set; } = false;
            
            public List<string> UserAnswer { get; set; }
            public List<string> QuizAnswer { get; set; }

            // Navigation property to relate to quizzes
            public virtual ICollection<Quiz> Quizzes { get; set; }

        }
    }


    public class UserDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public bool Role { get; set; } = false;
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

    public class QuizAnswers
    {
        public int Id { get; set; }
        public Quiz QuizId { get; set; }
        public User UserId { get; set; }
        public List<string> UserAnswer { get; set; }
        public List<string> QuizAnswer { get; set; }
    }
}