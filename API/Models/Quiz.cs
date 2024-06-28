namespace API.Models
{
    public class Quiz : Common
    {
        public string Title {  get; set; }
        public Question QuestionId { get; set; }
        public string InvitedUsers { get; set; }
        public User UserId { get; set; }
        public Timer timer { get; set; } // Overall quiz timer
    }
}