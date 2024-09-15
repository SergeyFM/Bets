using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using NotificationService.Domain;
using NotificationService.Domain.Directories;

namespace NotificationService.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() { }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<IncomingMessages> IncomingMessages { get; set; }
        public DbSet<Messengers> Messengers { get; set; }
        public DbSet<Bettors> Bettors { get; set; }
        public DbSet<MessageSources> MessageSources { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            IncomingMessagesModelCreating(modelBuilder);
            MessengersModelCreating(modelBuilder);
            BettorsModelCreating(modelBuilder);
            BettorAddressesModelCreating(modelBuilder);
            MessageSourcesModelCreating(modelBuilder);
        }

        private void IncomingMessagesModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IncomingMessages>().HasKey(e => e.Id);

            modelBuilder.Entity<IncomingMessages>().Property(b => b.SourceId).IsRequired();
            modelBuilder.Entity<IncomingMessages>().Property(b => b.TargetId).IsRequired();
            modelBuilder.Entity<IncomingMessages>().Property(b => b.CreatedDate).IsRequired();
            modelBuilder.Entity<IncomingMessages>().Property(b => b.Message).IsRequired()
                .HasMaxLength(1000);
            modelBuilder.Entity<IncomingMessages>().Property(b => b.CreatedBy).IsRequired()
                .HasMaxLength(60);
        }

        private void MessengersModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Messengers>().HasKey(e => e.Id);

            modelBuilder.Entity<Messengers>().Property(b => b.Name).IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<Messengers>().Property(b => b.CreatedBy).IsRequired()
                .HasMaxLength(60);
            modelBuilder.Entity<Messengers>().Property(b => b.ModifiedBy).HasMaxLength(60);
            modelBuilder.Entity<Messengers>().Property(b => b.DeletedBy).HasMaxLength(60);
        }

        private void BettorsModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bettors>().HasKey(e => e.Id);

            modelBuilder.Entity<Bettors>().Property(b => b.Nickname).IsRequired()
                .HasMaxLength(250);
            modelBuilder.Entity<Bettors>().Property(b => b.CreatedBy).IsRequired()
                .HasMaxLength(60);
            modelBuilder.Entity<Bettors>().Property(b => b.ModifiedBy).HasMaxLength(60);
            modelBuilder.Entity<Bettors>().Property(b => b.DeletedBy).HasMaxLength(60);
        }

        private void MessageSourcesModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MessageSources>().HasKey(e => e.Id);

            modelBuilder.Entity<MessageSources>().Property(b => b.Description).HasMaxLength(1000);
            modelBuilder.Entity<MessageSources>().Property(b => b.CreatedBy).IsRequired()
                .HasMaxLength(60);
            modelBuilder.Entity<MessageSources>().Property(b => b.ModifiedBy).HasMaxLength(60);
            modelBuilder.Entity<MessageSources>().Property(b => b.DeletedBy).HasMaxLength(60);
        }

        private void BettorAddressesModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BettorAddresses>()
                .HasOne(r => r.Bettor)
                .WithMany(b => b.BettorMessangers)
                .HasForeignKey(k => k.BettorId)
                .IsRequired();

            modelBuilder.Entity<BettorAddresses>()
                .HasOne(r => r.Messenger)
                .WithMany(b => b.BettorAddresses)
                .HasForeignKey(k => k.MessengerId)
                .IsRequired();

            modelBuilder.Entity<BettorAddresses>().HasKey(e => e.Id);

            modelBuilder.Entity<BettorAddresses>().Property(b => b.Address).IsRequired()
                .HasMaxLength(250);
            modelBuilder.Entity<BettorAddresses>().Property(b => b.CreatedBy).IsRequired()
                .HasMaxLength(60);
            modelBuilder.Entity<BettorAddresses>().Property(b => b.ModifiedBy).HasMaxLength(60);
            modelBuilder.Entity<BettorAddresses>().Property(b => b.DeletedBy).HasMaxLength(60);
        }
    }
}
