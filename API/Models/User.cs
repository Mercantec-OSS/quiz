using API.Models.API.Models;
using System.Security.AccessControl;

namespace API.Models {

    namespace API.Models
    {
        public class User
        {
            [Key]
            public int ID { get; set; }
            public string Email { get; set; }
            public string Username { get; set; }
            public string HashedPassword { get; set; }
            public string Salt { get; set; }
            public DateTime LastLogin { get; set; }
            public bool Role { get; set; }  // Could represent a boolean flag for admin/user role

            // Navigation properties
            public virtual ICollection<Quiz> CreatedQuizzes { get; set; }  // Quizzes created by the user
            public virtual ICollection<CompletedQuiz> CompletedQuizzes { get; set; }  // Quizzes completed by the user
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