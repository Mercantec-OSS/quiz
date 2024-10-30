using NuGet.Packaging.Signing;

namespace API.Models
{
    public class Token
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("User")]
        public User user { get; set; }

        public string JWTToken {  get; set; }

        public DateTime ExpiresAt { get; set; } = DateTime.UtcNow.AddDays(1);
    }

    public class TokenDTO
    {
        public int user { get; set; }

        public string jWTToken { get; set; }


    }
}
