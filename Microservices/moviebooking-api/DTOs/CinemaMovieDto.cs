
namespace moviebooking_api.DTOs
{
    public class CinemaMovieDto
    {
        public string Name { get; set; }

        public string City { get; set; }

        public IEnumerable<string> ShowTimings { get; set; }
    }
}
