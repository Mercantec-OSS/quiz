namespace API.Models;

public class UnderCategories
{
    [Key]
    public int ID { get; set; }
    public string UnderCategory { get; set; }

    [ForeignKey("Category")]
    public virtual Categories category { get; set; }
}
