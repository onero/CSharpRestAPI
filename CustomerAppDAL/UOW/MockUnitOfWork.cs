using RestAppDAL.Entities;
using RestAppDAL.Interfaces;
using RestAppDAL.Repositories;

namespace RestAppDAL.UOW
{
    internal class MockUnitOfWork : IUnitOfWork
    {
        public MockUnitOfWork()
        {
            CustomerRepository = new MockCustomerRepository();
        }

        public IRepository<Customer> CustomerRepository { get; }

        public int Complete()
        {
            return 0;
        }

        public void Dispose()
        {
        }
    }
}