using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Context.EntityConfig
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Title).IsRequired().HasMaxLength(200);
            builder.Property(b => b.Author).IsRequired().HasMaxLength(100);
            builder.Property(b => b.ISBN).IsRequired().HasMaxLength(32);
            builder.HasIndex(b => b.ISBN)
             .IsUnique();

            builder.HasMany(b => b.BookCopies)
                   .WithOne(bc => bc.Book)
                   .HasForeignKey(bc => bc.BookId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}