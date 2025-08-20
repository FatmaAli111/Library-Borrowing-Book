using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{

    public static class RoleDefault
    {

        public static string Admin => nameof(Admin);
        public static string AdminRoleId = "019791d7-92ab-7022-8774-1eecdba1f2a2";
        public static string Admincurrency = "019791d9c60a7d889dab3f7809e740f3";

        public static string Member => nameof(Member);
        public static string MemberRoleId = "019791d7-da52-70db-96ec-87c909bc811a";
        public static string Membercurrency = "019791d864df7179a5d6d15c06eb1e83";


    }
}
