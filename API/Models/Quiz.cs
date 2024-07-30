using API.Models;

namespace API.Models
{
    public class Quiz : Common
    {
        public string Title {  get; set; }
        public List<Question> Question { get; set; }
        public int QuestionId {  get; set; }
        public List<string> InvitedUsers { get; set; }
		public User Creator { get; set; }
		public int CreatorId { get; set; }
        public DateTime Timer { get; set; } // Custom timer for more complex functionality
        public Difficulty Maindifficulty { get; set; }
        public string DifficultyLevel { get; set; }
    }

    public class QuizDTO
    {
        public string Title { get; set; }
        public int CreatorId { get; set; }
        public List<string> InvitedUsers { get; set; } 
        public DateTime Timer { get; set; } // Custom timer for more complex functionality
        public string DifficultyLevel { get; set;}
    }
}