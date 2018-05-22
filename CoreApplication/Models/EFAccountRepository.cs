using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoreApplication.Models
{
    public class EFAccountRepository : IAccountRepository
    {
        private ApplicationContext _context;
        public EFAccountRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IQueryable<RoleIdentityAccess> getUserMenu(string roleId)
        {        
            return _context.RoleIdentityAccesses.Where(r => r.RoleId == roleId).Include(m => m.ApplicationMenu);
        }
    }
}
