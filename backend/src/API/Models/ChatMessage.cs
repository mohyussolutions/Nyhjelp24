using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class ChatMessage
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string From { get; set; }
        [Required]
        public required string To { get; set; }
        [Required]
        public required string Message { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}