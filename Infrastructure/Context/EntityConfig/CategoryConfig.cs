using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context.EntityConfig
{
    using Data.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    namespace Infrastructure.Configurations
    {
        public class CategoryConfig : IEntityTypeConfiguration<Category>
        {
            public void Configure(EntityTypeBuilder<Category> builder)
            {
            
                builder.HasKey(c => c.Id);

                builder.Property(c => c.Name)
                       .IsRequired()
                       .HasMaxLength(100);

                builder.HasMany(c => c.Books)
                       .WithOne(b => b.Category)
                       .HasForeignKey(b => b.CategoryId)
                       .OnDelete(DeleteBehavior.Cascade); 
            }
        }
    }

}
