using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ApplicationData
{
    public class MnagementBdContext : DbContext
    {
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Exhibition> Exhibitions { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public MnagementBdContext(DbContextOptions<MnagementBdContext> options) : base(options)
        {
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    var connectionString = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = CulturalCnterDB ";
        //    optionsBuilder.UseSqlServer(connectionString);
        //    base.OnConfiguring(optionsBuilder);
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // تحويلات الـ Enums
            modelBuilder.Entity<Author>().Property(a => a.Gender).HasConversion<int>();
            modelBuilder.Entity<Book>().Property(b => b.Status).HasConversion<int>();
            modelBuilder.Entity<Book>().Property(b => b.Section).HasConversion<int>();

            // العلاقة بين Visitor و Activity (One-to-Many)
            modelBuilder.Entity<Visitor>()
                .HasMany(v => v.activities)
                .WithOne(a => a.Visitor)
                .HasForeignKey(a => a.VisitorId)
                .OnDelete(DeleteBehavior.NoAction);

            // العلاقة بين Visitor و Reservation (One-to-Many)
           /* modelBuilder.Entity<Visitor>()
                .HasMany(v => v.Reservations)
                .WithOne(r => r.Visitor)
                .HasForeignKey(r => r.VisitorId)
                .OnDelete(DeleteBehavior.NoAction);*/

            // العلاقة بين Visitor و Notification (One-to-Many)
            modelBuilder.Entity<Visitor>()
                .HasMany(v => v.Notifications)
                .WithOne(n => n.Visitor)
                .HasForeignKey(n => n.VisitorId)
                .OnDelete(DeleteBehavior.NoAction);

          
            // العلاقة بين Author و Book (One-to-Many)
            modelBuilder.Entity<Author>()
                .HasMany(a => a.Books)
                .WithOne(b => b.Author)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);

            // العلاقة بين Book و Loan (One-to-Many)
            modelBuilder.Entity<Book>()
                .HasMany(b => b.Loans)
                .WithOne(l => l.Book)
                .HasForeignKey(l => l.BookId)
                .OnDelete(DeleteBehavior.NoAction);

            
            base.OnModelCreating(modelBuilder);
        }
    }
}
