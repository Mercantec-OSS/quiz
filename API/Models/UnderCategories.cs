namespace API.Models
{
    public class UnderCategories
    {
        [Key]
        public int ID { get; set; }
        public string UnderCategory { get; set; }

        [ForeignKey("Category")]
        public virtual Categories Categories { get; set; }
    }

    public class UnderCategoriesDTO
    {
        public string UnderCategory { get; set; }

        public int CategoryID { get; set; }
    }
}
