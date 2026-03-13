
namespace API.Models
{
    public class Job
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? PostedAt { get; set; }
    }
}
