using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Context.EntityConfig
{
    public class ReminderConfig : IEntityTypeConfiguration<Reminder>
    {
        public void Configure(EntityTypeBuilder<Reminder> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Message).IsRequired().HasMaxLength(512);

            builder.HasOne(r => r.Checkout)
                   .WithMany(c => c.Reminders)
                   .HasForeignKey(r => r.CheckoutID)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}