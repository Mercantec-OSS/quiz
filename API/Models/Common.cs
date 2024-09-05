namespace API.Models
{
    public class Common
    {
        [Key]
        public int Id { get; set; }

        // Foreign key property for User
        public int UserId { get; set; }

        // Navigation property for related User
        public virtual User User { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
