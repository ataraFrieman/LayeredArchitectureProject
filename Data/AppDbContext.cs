
using Microsoft.EntityFrameworkCore;
using PublicInquiriesAPI.Models;

namespace PublicInquiriesAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Inquiry> Inquiries { get; set; }
        public DbSet<Attachment> Attachments { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inquiry>().OwnsOne(i => i.PersonalInfo);
            modelBuilder.Entity<Inquiry>().OwnsOne(i => i.ComplaintDetails);
             modelBuilder.Entity<Inquiry>()
                .HasMany(i => i.Attachments)
                .WithOne(a => a.Inquiry)
                .HasForeignKey(a => a.InquiryId);
        }
    }
}
