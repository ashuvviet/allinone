namespace moviebooking_api.Model
{
    public class CinemaMovie
    {
        public long Id { get; set; }

        public long MovieId { get; set; }

        public long CinemaId { get; set; }

        public IEnumerable<string> ShowTimings { get; set; }
    }
}
