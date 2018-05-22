using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApplication.Models
{
    public class ApplicationContext : IdentityDbContext<UserIdentity>
    {
        public DbSet<RoleIdentityAccess> RoleIdentityAccesses { get; set; }
        public DbSet<ApplicationMenu> ApplicationMenus { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
           : base(options)
        {
        }

    }
}
