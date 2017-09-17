using Microsoft.EntityFrameworkCore;
using RestAppDAL.Context;
using RestAppDAL.Entities;
using RestAppDAL.Interfaces;
using RestAppDAL.Repositories;

namespace RestAppDAL.UOW
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly SQLContext _context;

        public IRepository<Customer> CustomerRepository { get; }
        public IRepository<Order> OrderRepository { get; }
        public IRepository<Address> AddressRepository { get; }

        public UnitOfWork()
        {
            _context = new SQLContext();
            _context.Database.EnsureCreated();
            AddressRepository = new AddressRepositoryEFMemory(_context);
            CustomerRepository = new CustomerRepositoryEFMemory(_context);
            OrderRepository = new OrderRepositoryEFMemory(_context);
        }

        public int Complete()
        {
            // Return the number of objects written to the underlying database
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}