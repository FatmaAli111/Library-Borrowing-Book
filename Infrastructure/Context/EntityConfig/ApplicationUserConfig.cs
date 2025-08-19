using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Context.EntityConfig
{
    public class ApplicationUserConfig : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("AspNetUsers"); 

            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.Address)
                .HasMaxLength(250);

            builder.Property(u => u.MembershipType)
                .HasMaxLength(50);

            builder.Property(u => u.MembershipStatus)
                .HasMaxLength(50);

            builder.HasMany(u => u.Checkouts)
                   .WithOne(c => c.User)
                   .HasForeignKey(c => c.UserID)
                   .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
