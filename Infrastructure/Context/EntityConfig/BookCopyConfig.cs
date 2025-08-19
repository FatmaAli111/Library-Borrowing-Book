using Data.Entities.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class BookCopyConfig : IEntityTypeConfiguration<BookCopy>
{
    public void Configure(EntityTypeBuilder<BookCopy> builder)
    {
        builder.HasKey(bc => bc.Id);

        builder.Property(bc => bc.Status)
               .IsRequired();

        builder.HasOne(bc => bc.Book)
               .WithMany(b => b.BookCopies)
               .HasForeignKey(bc => bc.BookId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(bc => bc.Checkouts)
               .WithOne(c => c.BookCopy)
               .HasForeignKey(c => c.BookCopyID)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
