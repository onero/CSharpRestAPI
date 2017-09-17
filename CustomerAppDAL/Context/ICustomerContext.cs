using Microsoft.EntityFrameworkCore;
using RestAppDAL.Entities;

namespace RestAppDAL.Context
{
    public interface ICustomerContext
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<Address> Addresses { get; set; }

    }
}