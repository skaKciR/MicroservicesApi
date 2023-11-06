namespace MicroservicesApi
{
    public class News
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public News()
        {
            DateAdded = DateTime.UtcNow;
        }
    }
}
