namespace API.Models
{
    public class Difficulty : Common
    {
        public int GF2 { get; set; }
        public int H1 { get; set; }
        public int H2 { get; set; }
        public int H3 { get; set; }
        public int H4 { get; set; }
        public int H5 { get; set; }
    }

    public class GF2_Difficulty
    {
        public int GF2 { get; set; } = 100;
        public int H1 { get; set; } = 110;
        public int H2 { get; set; } = 120;
        public int H3 { get; set; } = 130;
        public int H4 { get; set; } = 140;
        public int H5 { get; set; } = 150;
    }

    public class H1_Difficulty
    {
        public int GF2 { get; set; } = 90;
        public int H1 { get; set; } = 100;
        public int H2 { get; set; } = 110;
        public int H3 { get; set; } = 120;
        public int H4 { get; set; } = 130;
        public int H5 { get; set; } = 140;
    }

    public class H2_Difficulty
    {
        public int GF2 { get; set; } = 80;
        public int H1 { get; set; } = 90;
        public int H2 { get; set; } = 100;
        public int H3 { get; set; } = 110;
        public int H4 { get; set; } = 120;
        public int H5 { get; set; } = 130;
    }

    public class H3_Difficulty
    {
        public int GF2 { get; set; } = 70;
        public int H1 { get; set; } = 80;
        public int H2 { get; set; } = 90;
        public int H3 { get; set; } = 100;
        public int H4 { get; set; } = 110;
        public int H5 { get; set; } = 120;
    }

    public class H4_Difficulty
    {
        public int GF2 { get; set; } = 60;
        public int H1 { get; set; } = 70;
        public int H2 { get; set; } = 80;
        public int H3 { get; set; } = 90;
        public int H4 { get; set; } = 100;
        public int H5 { get; set; } = 110;
    }

    public class H5_Difficulty
    {
        public int GF2 { get; set; } = 50;
        public int H1 { get; set; } = 60;
        public int H2 { get; set; } = 70;
        public int H3 { get; set; } = 80;
        public int H4 { get; set; } = 90;
        public int H5 { get; set; } = 100;
    }
}
