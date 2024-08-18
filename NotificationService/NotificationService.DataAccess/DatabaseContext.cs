using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using NotificationService.Domain;

namespace NotificationService.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() { }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<IncomingMessages> IncomingMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            IncomingMessagesModelCreating(modelBuilder);

            //modelBuilder.Entity<Customer>()
            //    .HasMany(c => c.Promocodes)
            //    .WithOne(p => p.Customer);

            //modelBuilder.Entity<Preference>()
            //        .HasMany(p => p.Customers)
            //        .WithMany(c => c.Preferences);
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
    }
}
