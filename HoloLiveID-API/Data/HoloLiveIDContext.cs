using HoloLiveID_API.Model;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace HoloLiveID_API.Data
{
    public class HoloLiveIDContext : DbContext
    {
        public HoloLiveIDContext(DbContextOptions<HoloLiveIDContext> options) : base(options) 
        {
        }
        public DbSet<HoloUser> HoloUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultContainer("HoloUsers");

            builder.Entity<HoloUser>()
             .ToContainer(nameof(HoloUser))
             .HasPartitionKey(c => c.HoloId)
             .HasNoDiscriminator();
        }
    }
}
