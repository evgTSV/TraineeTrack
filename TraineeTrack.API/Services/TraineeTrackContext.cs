using Microsoft.EntityFrameworkCore;
using TraineeTrack.API.Models.DB;

namespace TraineeTrack.API.Services;

public class TraineeTrackContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username).IsUnique();
    }
}