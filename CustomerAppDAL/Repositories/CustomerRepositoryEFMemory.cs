using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RestAppDAL.Context;
using RestAppDAL.Entities;
using RestAppDAL.Interfaces;

namespace RestAppDAL.Repositories
{
    internal class CustomerRepositoryEFMemory : IRepository<Customer>
    {
        private readonly SQLContext _context;

        public CustomerRepositoryEFMemory(SQLContext context)
        {
            _context = context;
        }

        public Customer Create(Customer customerToCreate)
        {
            var createdCustomer = _context.Customers.Add(customerToCreate);
            return createdCustomer.Entity;
        }

        public IEnumerable<Customer> GetAllById(List<int> ids)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Customer> GetAll()
        {
            var customers = _context.Customers
                .Include(c => c.Addresses).ToList();
            return customers;
        }

        public Customer GetById(int id)
        {
            return _context.Customers
                .Include(c => c.Addresses)
                .FirstOrDefault(c => c.Id == id);
        }

        public bool Delete(int id)
        {
            var customer = GetById(id);
            if (customer == null) return false;
            _context.Customers.Remove(customer);
            return true;
        }
    }
}