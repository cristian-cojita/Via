using Microsoft.EntityFrameworkCore;
using Via.Models;

namespace Via.Data
{
    public class ViaDbContext : DbContext
    {
        public ViaDbContext(DbContextOptions<ViaDbContext> options) : base(options)
        {
        }

        public DbSet<Attendee> Attendees { get; set; }
        public DbSet<Picture> Pictures { get; set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Attendee>().ToTable("Attendee");
        //}
    }
}
