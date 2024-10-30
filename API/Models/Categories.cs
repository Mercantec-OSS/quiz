namespace API.Models
{
    public class Categories
    {
        [Key]
        public int ID { get; set; }
        public string Category { get; set; }

        [ForeignKey("Education")]
        public virtual Educations education { get; set; }
    }

    public class CategoriesDTO
    {
        public int ID { get; set; }
        public string Category { get; set; }

        public int EducationID { get; set; }
        
    }

    public class CategoryCreateDTO
    {
        public string Category { get; set; }
        public int EducationID { get; set; }
    }
}
