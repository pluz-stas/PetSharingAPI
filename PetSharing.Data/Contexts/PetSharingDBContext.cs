using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using PetSharing.Data.Entities;


namespace PetSharing.Data.Contexts
{
    public class PetSharingDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<PetProfile> PetProfiles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public PetSharingDbContext(DbContextOptions<PetSharingDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subscription>().HasKey(sc => new { sc.UserId, sc.PetId });
            modelBuilder.Entity<Subscription>()
                .HasOne<User>(sc => sc.User)
                .WithMany(s => s.Subscriptions)
                .HasForeignKey(sc => sc.UserId);

            modelBuilder.Entity<Subscription>()
                .HasOne<PetProfile>(sc => sc.PetProfile)
                .WithMany(s => s.Folowers)
                .HasForeignKey(sc => sc.PetId);
            base.OnModelCreating(modelBuilder);
        }

    }
}
