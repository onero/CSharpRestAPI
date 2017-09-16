using RestAppDAL.Context;
using RestAppDAL.Entities;
using RestAppDAL.Interfaces;
using RestAppDAL.Repositories;

namespace RestAppDAL.UOW
{
    internal class UnitOfWorkMem : IUnitOfWork
    {
        private readonly InMemoryContext _context;

        public IRepository<Customer> CustomerRepository { get; }
        public IRepository<Order> OrderRepository { get; }
        public IRepository<Address> AddressRepository { get; }

        public UnitOfWorkMem()
        {
            _context = new InMemoryContext();
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