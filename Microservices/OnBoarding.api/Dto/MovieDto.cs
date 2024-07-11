namespace OnBoarding.api.Dto
{
    public class MovieDto
    {
        public string Name { get; set; }

        public CinemaDto Cinema { get; set; }

        public IEnumerable<string> ShowTimings { get; set; }
    }

    public class CinemaDto
    {
        public string Name { get; set; }

        public string City { get; set; }

        public string OwnerId { get; set; }
    }

    public class Entry
    {
        public string API { get; set; }
        public string Description { get; set; }
        public string Auth { get; set; }
        public bool HTTPS { get; set; }
        public string Cors { get; set; }
        public string Link { get; set; }
        public string Category { get; set; }
    }

    public class Root
    {
        public int count { get; set; }
        public List<Entry> entries { get; set; }
    }
}
