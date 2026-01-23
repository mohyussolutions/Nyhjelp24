using System;

namespace Core.Entities
{
    public class Job
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Status { get; set; } = "Open"; // Open, InProgress, Completed
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign Key
        public int CreatedByUserId { get; set; }
        public User? CreatedBy { get; set; }
    }
}
