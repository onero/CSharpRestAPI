using System.Collections.Generic;
using System.Linq;
using RestAppDAL.Context;
using RestAppDAL.Entities;
using RestAppDAL.Interfaces;

namespace RestAppDAL.Repositories
{
    internal class OrderRepositoryEFMemory : IRepository<Order>
    {
        private readonly InMemoryContext _context;

        public OrderRepositoryEFMemory(InMemoryContext context)
        {
            _context = context;
        }

        public Order Create(Order entity)
        {
            var createdOrder = _context.Orders.Add(entity);
            return createdOrder.Entity;
        }

        public IEnumerable<Order> GetAll()
        {
            var orders = _context.Orders.ToList();
            return orders;
        }

        public Order GetById(int id)
        {
            return _context.Orders.FirstOrDefault(c => c.Id == id);
        }

        public bool Delete(int id)
        {
            var order = GetById(id);
            if (order == null) return false;
            _context.Orders.Remove(order);
            return true;
        }
    }
}