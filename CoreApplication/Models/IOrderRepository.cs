using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApplication.Models
{
    public interface IOrderRepository
    {
        IQueryable<Order> Orders { get; }
        Order GetOrderById(int id);
        void Save();
        void Create(Order order);
        void Create(List<Order> orders);
        void Update(Order order);
        void Delete(int id);
    }
}
