using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Context.EntityConfig
{
    public class CheckoutConfig : IEntityTypeConfiguration<Checkout>
    {
        public void Configure(EntityTypeBuilder<Checkout> builder)
        {
            builder.HasKey(c => c.CheckoutID);

            builder.Property(c => c.CheckoutDate).IsRequired();
            builder.Property(c => c.DueDate).IsRequired();
            builder.Property(c => c.FineAmount).HasColumnType("decimal(18,2)");

            builder.HasOne(c => c.User)
                   .WithMany(u => u.Checkouts)
                   .HasForeignKey(c => c.UserID)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.BookCopy)
                   .WithMany(bc => bc.Checkouts)
                   .HasForeignKey(c => c.BookCopyID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Reminders)
                   .WithOne(r => r.Checkout)
                   .HasForeignKey(r => r.CheckoutID)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}