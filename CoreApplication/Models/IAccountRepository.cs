using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApplication.Models
{
    public interface IAccountRepository
    {
        IQueryable<RoleIdentityAccess> getUserMenu(string roleId);
    }
}
