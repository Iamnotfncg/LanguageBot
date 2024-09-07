using Microsoft.EntityFrameworkCore;
using LanguageBot.Data;

public class BotDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public BotDbContext(DbContextOptions<BotDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.UserId);
            entity.Property(u => u.UserId).ValueGeneratedNever();
            entity.OwnsOne(u => u.UserProgress, progress =>
            {
                progress.Property(p => p.SelectedLanguages).HasColumnName("SelectedLanguages");
            });
        });
    }
}