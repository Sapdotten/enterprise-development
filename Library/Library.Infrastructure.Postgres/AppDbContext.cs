using Microsoft.EntityFrameworkCore;
using Library.Domain.Entities;

namespace Library.Infrastructure.Postgres;

/// <summary>
/// Database context for the library system using Entity Framework Core.
/// Manages DbSet instances for Book, Reader, and LoanRecord entities.
/// Configures schema, constraints, relationships, and type mappings in OnModelCreating.
/// </summary>
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Gets or sets the DbSet of books.
    /// </summary>
    public DbSet<Book> Books { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of readers.
    /// </summary>
    public DbSet<Reader> Readers { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of loan records.
    /// </summary>
    public DbSet<LoanRecord> LoanRecords { get; set; }

    /// <summary>
    /// Configures the model using the Fluent API.
    /// Sets primary keys, property constraints, enum conversions, and relationships with cascade delete.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Reader>(c =>
        {
            c.HasKey(c => c.Id);
            c.Property(c => c.Id)
                .ValueGeneratedOnAdd();
            c.Property(c => c.FirstName)
                .IsRequired()
                .HasMaxLength(128);
            c.Property(c => c.LastName)
                .IsRequired()
                .HasMaxLength(128);
            c.Property(c => c.PatronymicName)
                .IsRequired()
                .HasMaxLength(128);
            c.Property(c => c.Address)
                .IsRequired()
                .HasMaxLength(128);
            c.Property(c => c.PhoneNumber)
                .IsRequired()
                .HasMaxLength(18);
            c.Property(c => c.RegistrationDate)
                .IsRequired()
                .HasColumnType("date");
        });

        modelBuilder.Entity<Book>(b =>
        {
            b.HasKey(b => b.Id);
            b.Property(b => b.Id)
                .ValueGeneratedOnAdd();
            b.Property(b => b.InventoryNumber)
                .IsRequired();
            b.Property(b => b.Code)
               .IsRequired()
               .HasMaxLength(6);
            b.Property(b => b.Authors)
               .IsRequired()
               .HasMaxLength(128);
            b.Property(b => b.Title)
               .IsRequired()
               .HasMaxLength(128);
            b.Property(b => b.Year)
               .IsRequired();
            b.Property(b => b.Publisher)
               .HasConversion<string>()
               .IsRequired();
            b.Property(b => b.PublisherType)
               .HasConversion<string>()
               .IsRequired();
        });

        modelBuilder.Entity<LoanRecord>(b =>
        {
            b.HasKey(b => b.Id);
            b.Property(b => b.Id)
                .ValueGeneratedOnAdd();
            b.HasOne<Book>()
                .WithMany()
                .HasForeignKey(b => b.BookId)
                .OnDelete(DeleteBehavior.Cascade);
            b.HasOne<Reader>()
                .WithMany()
                .HasForeignKey(b => b.ReaderId)
                .OnDelete(DeleteBehavior.Cascade);
            b.Property(b => b.IssueDate)
                .IsRequired()
                .HasColumnType("date");
            b.Property(b => b.LoanTerm)
                .IsRequired();
        });
    }
}