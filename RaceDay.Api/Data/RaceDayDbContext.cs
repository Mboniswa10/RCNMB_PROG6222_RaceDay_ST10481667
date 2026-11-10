
using Microsoft.EntityFrameworkCore;
using RaceDay.Api.Models;

namespace RaceDay.Api.Data
{
    public class RaceDayDbContext : DbContext
    {
        public RaceDayDbContext(
            DbContextOptions<RaceDayDbContext> options)
            : base(options)
        {
        }

        public DbSet<AppUser> Users => Set<AppUser>();
        public DbSet<Event> Events => Set<Event>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<EventCategory> EventCategories => Set<EventCategory>();
        public DbSet<Enrolment> Enrolments => Set<Enrolment>();
        public DbSet<Result> Results => Set<Result>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Users
            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(u => u.UserId);

                entity.Property(u => u.FullName)
                    .HasMaxLength(100).IsRequired();

                entity.Property(u => u.Email)
                    .HasMaxLength(150).IsRequired();

                entity.HasIndex(u => u.Email).IsUnique();

                entity.Property(u => u.PasswordHash)
                    .HasMaxLength(255).IsRequired();

                entity.Property(u => u.Role)
                    .HasMaxLength(20).IsRequired();

                entity.Property(u => u.CreatedAt)
                    .HasColumnType("datetime2")
                    .HasDefaultValueSql("GETDATE()");
            });

            // Events
            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("Events");
                entity.HasKey(e => e.EventId);

                entity.Property(e => e.EventName)
                    .HasMaxLength(150).IsRequired();

                entity.Property(e => e.Description)
                    .HasMaxLength(500);

                entity.Property(e => e.EventType)
                    .HasMaxLength(20).IsRequired();

                entity.Property(e => e.EventDate)
                    .HasColumnType("date");

                entity.Property(e => e.StartTime)
                    .HasColumnType("time");

                entity.Property(e => e.Venue)
                    .HasMaxLength(150).IsRequired();

                entity.Property(e => e.City)
                    .HasMaxLength(80).IsRequired();

                entity.Property(e => e.Province)
                    .HasMaxLength(80).IsRequired();

                entity.Property(e => e.DistanceKm)
                    .HasPrecision(6, 2);

                entity.Property(e => e.EntryFee)
                    .HasPrecision(10, 2)
                    .HasDefaultValue(0m);

                entity.Property(e => e.RouteInformation)
                    .HasMaxLength(500);

                entity.Property(e => e.WeatherInformation)
                    .HasMaxLength(500);

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .HasDefaultValue("Open")
                    .IsRequired();

                entity.HasOne(e => e.Organiser)
                    .WithMany(u => u.OrganisedEvents)
                    .HasForeignKey(e => e.OrganiserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Categories
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Categories");
                entity.HasKey(c => c.CategoryId);

                entity.Property(c => c.CategoryName)
                    .HasMaxLength(80).IsRequired();

                entity.HasIndex(c => c.CategoryName)
                    .IsUnique();

                entity.Property(c => c.Description)
                    .HasMaxLength(255);
            });

            // EventCategories: composite primary key
            modelBuilder.Entity<EventCategory>(entity =>
            {
                entity.ToTable("EventCategories");

                entity.HasKey(ec => new
                {
                    ec.EventId,
                    ec.CategoryId
                });

                entity.HasOne(ec => ec.Event)
                    .WithMany()
                    .HasForeignKey(ec => ec.EventId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ec => ec.Category)
                    .WithMany(c => c.EventCategories)
                    .HasForeignKey(ec => ec.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Enrolments
            modelBuilder.Entity<Enrolment>(entity =>
            {
                entity.ToTable("Enrolments");
                entity.HasKey(e => e.EnrolmentId);

                entity.Property(e => e.EnrolmentDate)
                    .HasColumnType("datetime2")
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .HasDefaultValue("Enrolled")
                    .IsRequired();

                entity.HasIndex(e => new
                {
                    e.EventId,
                    e.ParticipantId
                })
                    .IsUnique()
                    .HasDatabaseName("UQ_Enrolment");

                entity.HasOne(e => e.Event)
                    .WithMany(ev => ev.Enrolments)
                    .HasForeignKey(e => e.EventId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Participant)
                    .WithMany(u => u.Enrolments)
                    .HasForeignKey(e => e.ParticipantId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Category)
                    .WithMany(c => c.Enrolments)
                    .HasForeignKey(e => e.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Results
            modelBuilder.Entity<Result>(entity =>
            {
                entity.ToTable("Results");
                entity.HasKey(r => r.ResultId);

                entity.Property(r => r.ResultStatus)
                    .HasMaxLength(20).IsRequired();

                entity.HasOne(r => r.Enrolment)
                    .WithMany()
                    .HasForeignKey(r => r.EnrolmentId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
