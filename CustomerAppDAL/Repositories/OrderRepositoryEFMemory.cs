using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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
            _context.Orders.Add(entity);
            return entity;
        }

        public IEnumerable<Order> GetAll()
        {
            return _context.Orders.ToList();
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