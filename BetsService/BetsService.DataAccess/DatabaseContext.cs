using Microsoft.EntityFrameworkCore;
using BetsService.Domain;

namespace BetsService.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() { }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<EventOutcomes> EventOutcomes { get; set; }
        public DbSet<Events> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            EventsModelCreating(modelBuilder);
            EventOutcomesModelCreating(modelBuilder);
        }

        private void EventsModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Events>().HasKey(e => e.Id);

            modelBuilder.Entity<Events>().Property(b => b.EventStartTime).IsRequired();
            modelBuilder.Entity<Events>().Property(b => b.BetsEndTime).IsRequired();
            modelBuilder.Entity<Events>().Property(b => b.CreatedDate).IsRequired();
            modelBuilder.Entity<Events>().Property(b => b.IsOver).IsRequired();
            modelBuilder.Entity<Events>().Property(b => b.IsCanceled).IsRequired();
            modelBuilder.Entity<Events>().Property(b => b.Description).IsRequired()
                .HasMaxLength(1000);
            modelBuilder.Entity<Events>().Property(b => b.CreatedBy).IsRequired()
                .HasMaxLength(60);
            modelBuilder.Entity<Events>().Property(b => b.ModifiedBy).HasMaxLength(60);
            modelBuilder.Entity<Events>().Property(b => b.DeletedBy).HasMaxLength(60);
        }

        private void EventOutcomesModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventOutcomes>().HasKey(e => e.Id);

            modelBuilder.Entity<EventOutcomes>().Property(b => b.CreatedDate).IsRequired();
            modelBuilder.Entity<EventOutcomes>().Property(b => b.BetsClosed).IsRequired();
            modelBuilder.Entity<EventOutcomes>().Property(b => b.Description).IsRequired()
                .HasMaxLength(1000);
            modelBuilder.Entity<EventOutcomes>().Property(b => b.CreatedBy).IsRequired()
                .HasMaxLength(60);
            modelBuilder.Entity<EventOutcomes>().Property(b => b.ModifiedBy).HasMaxLength(60);
            modelBuilder.Entity<EventOutcomes>().Property(b => b.DeletedBy).HasMaxLength(60);

            modelBuilder.Entity<EventOutcomes>()
                .HasOne(r => r.Event)
                .WithMany(b => b.EventOutcomes)
                .HasForeignKey(k => k.EventId)
                .IsRequired();
        }
    }
}
