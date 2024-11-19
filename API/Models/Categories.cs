namespace API.Models;

public class Categories
{
    [Key]
    public int ID { get; set; }
    public string Category { get; set; }

    [ForeignKey("Education")]
    public virtual Educations education { get; set; }
}
