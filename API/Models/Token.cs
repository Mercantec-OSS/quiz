namespace API.Models;

public class Token
{
    [Key]
    public int ID { get; set; }

    [ForeignKey("User")]
    public User user { get; set; }

    public string JWTToken {  get; set; } = string.Empty;

    public DateTime ExpiresAt { get; set; } = DateTime.UtcNow.AddDays(1);
}
