namespace API.Models
{
    public class UnderCategories
    {
        [Key]
        public int ID { get; set; }
        public int UnderCategory { get; set; }

        public int CategoryID { get; set; }
        public virtual Categories Categories { get; set; }
    }
}
