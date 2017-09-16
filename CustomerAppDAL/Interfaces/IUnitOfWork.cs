using System;
using RestAppDAL.Entities;

namespace RestAppDAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Customer> CustomerRepository { get; }
        IRepository<Order> OrderRepository { get; }
        IRepository<Address> AddressRepository { get; }

        int Complete();
    }
}