namespace API.Models
{
    public class Categories
    {
        [Key]
        public int ID { get; set; }
        public string Category { get; set; }

        public int EducationID { get; set; }
        public virtual Educations Educations { get; set; }
    }

    public class CategoriesDTO
    {
        public string Category { get; set; }

        public int EducationID { get; set; }
    }
}
