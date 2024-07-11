
namespace moviebooking_api.DTOs
{
    public class MovieDto
    {
        public string Name { get; set; }

        public CinemaDto Cinema { get; set; }

        public IEnumerable<string> ShowTimings { get; set; }
    }
}
