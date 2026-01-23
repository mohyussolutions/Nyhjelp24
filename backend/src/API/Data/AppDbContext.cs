using Microsoft.EntityFrameworkCore;
using Core.Entities;

namespace API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<County> Counties { get; set; }
        public DbSet<Municipality> Municipalities { get; set; }
        public DbSet<API.Models.ContactUs> ContactMessages { get; set; }
        public DbSet<API.Models.ChatMessage> ChatMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Job>()
                .HasOne(j => j.CreatedBy)
                .WithMany()
                .HasForeignKey(j => j.CreatedByUserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
