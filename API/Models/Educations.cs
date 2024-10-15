﻿namespace API.Models
{
    public class Educations
    {
        [Key]
        public int ID { get; set; }
        public string Education { get; set; }
    }

    public class EducationsDTO
    {
        public string Education { get; set; }
    }
}