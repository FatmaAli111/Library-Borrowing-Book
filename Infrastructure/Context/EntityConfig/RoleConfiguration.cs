using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
namespace Infrastructure.Context.EntityConfig
{
    public class RoleConfiguration : IEntityTypeConfiguration<Data.Entities.ApplicationRole>
    {

        public void Configure(EntityTypeBuilder<Data.Entities.ApplicationRole> builder)
        {
            builder.HasData(
                new Data.Entities.ApplicationRole
                {
                    Id = RoleDefault.AdminRoleId,
                    Name = RoleDefault.Admin,
                    NormalizedName = RoleDefault.Admin.ToUpper(),
                    ConcurrencyStamp = RoleDefault.Admincurrency
                },
                 new Data.Entities.ApplicationRole
                 {
                     Id = RoleDefault.MemberRoleId,
                     Name = RoleDefault.Member,
                     NormalizedName = RoleDefault.Member.ToUpper(),
                     ConcurrencyStamp = RoleDefault.Membercurrency,
                     IsDefault = true
                 }
            );
        }
    }
}
