using Microsoft.EntityFrameworkCore;
using RestAppDAL.Entities;

namespace RestAppDAL.Context
{
    public class InMemoryContext : DbContext
    {
        //In memory setup
        private static readonly DbContextOptions<InMemoryContext> Options =
            new DbContextOptionsBuilder<InMemoryContext>().UseInMemoryDatabase("TheDB").Options;

        //Options that we want in memory
        public InMemoryContext() : base(Options)
        {
        }

        public DbSet<Customer> Customers { set; get; }
    }
}