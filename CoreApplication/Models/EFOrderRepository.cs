using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CoreApplication.Models
{
    public class EFOrderRepository : IOrderRepository
    {
        private BookContext context;
        public EFOrderRepository(BookContext bkContext)
        {
            context = bkContext;
        }
        public IQueryable<Order> Orders => context.Orders
            .Include(o=>o.Book)
                .ThenInclude(b=>b.Author);

        public void Create(Order order)
        {
            context.Add(order);
        }

        public void Create(List<Order> orders)
        {
            context.AddRange(orders);
        }

        public void Delete(int id)
        {
            Order order = context.Orders.Find(id);
            if (order != null)
                context.Orders.Remove(order);
        }

        public Order GetOrderById(int id)
        {
            return context.Orders.Where(o => o.OrderId == id).FirstOrDefault();

        }

        public void Save()
        {
           context.SaveChanges();
        }

        public void Update(Order order)
        {
            context.Entry(order).State = EntityState.Modified;
        }
    }

}
