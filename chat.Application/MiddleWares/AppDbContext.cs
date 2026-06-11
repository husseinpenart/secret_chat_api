using Microsoft.EntityFrameworkCore;
using secre_chat_api.chat.Domain.Entities;

namespace secre_chat_api.chat.Application.MiddleWares
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ContactDtos> Contacts { get; set; }
        public DbSet<ChatDtos> Chats { get; set; }
        public DbSet<ChatParticipantDtos> ChatParticipants { get; set; }
        public DbSet<ChannelDtos> Channels { get; set; }
        public DbSet<ChannelMemberDtos> ChannelMembers { get; set; }
        public DbSet<MessageDtos> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User
            modelBuilder.Entity<UserEntity>()
                .HasIndex(e => e.PhoneNumber).IsUnique();

            // Contact: one user has many contacts, no cascade on TargetUser to avoid cycles
            modelBuilder.Entity<ContactDtos>()
                .HasOne(c => c.Owner)
                .WithMany(u => u.Contacts)
                .HasForeignKey(c => c.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ContactDtos>()
                .HasOne(c => c.TargetUser)
                .WithMany()
                .HasForeignKey(c => c.TargetUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Message: restrict deletes to avoid multiple cascade paths
            modelBuilder.Entity<MessageDtos>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MessageDtos>()
                .HasOne(m => m.Chat)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ChatId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MessageDtos>()
                .HasOne(m => m.Channel)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ChannelId)
                .OnDelete(DeleteBehavior.Cascade);

            // Channel owner
            modelBuilder.Entity<ChannelDtos>()
                .HasOne(c => c.Owner)
                .WithMany()
                .HasForeignKey(c => c.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
