using Microsoft.EntityFrameworkCore;
using secre_chat_api.chat.Domain.Entities;

namespace secre_chat_api.chat.Application.MiddleWares
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<UserEntity> userEntities { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().HasIndex(e => e.phoneNumber).IsUnique();
            modelBuilder.Entity<UserEntity>().ToTable("Users");
        }
    }
}
