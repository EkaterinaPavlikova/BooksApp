using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApplication.Models
{
    public class RoleIdentityAccess
    {
        public int Id { get; set; }

        public string RoleId { get; set; }
        
        public int ApplicationMenuId { get; set; }
        public ApplicationMenu ApplicationMenu{ get; set; }
    }
}
