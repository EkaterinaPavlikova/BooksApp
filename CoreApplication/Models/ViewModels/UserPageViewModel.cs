using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApplication.Models.ViewModels
{
    public class UserPageViewModel
    {
        public IEnumerable<Order> UserOrders { get; set; }
        public IEnumerable<Order> OrdersOnProsessing { get; set; }
    }
}
