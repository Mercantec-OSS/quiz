namespace API.Models;

public class User
{
    [Key]
    public int ID { get; set; }

    public string Email { get; set; }
    public string Username { get; set; }

    public string? HashedPassword { get; set; }
    public string Salt { get; set; }

    public DateTime LastLogin { get; set; }

    [ForeignKey("Role")]
    public virtual Roles role { get; set; }
}
