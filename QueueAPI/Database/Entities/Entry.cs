namespace QueueAPI.Database.Entities
{
    public class Entry
    {
        public int Id { get; set; }
        public string Currency { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public Boolean WasConsumed { get; set; }
    }

}
