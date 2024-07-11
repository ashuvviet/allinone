namespace moviebooking_api.Model
{
    public class Cinema
    {
        public long Id { get; set; }

        public string OwnerId { get; set; }

        public string Name { get; set; }

        public long CityId { get; set; }
    }
}
