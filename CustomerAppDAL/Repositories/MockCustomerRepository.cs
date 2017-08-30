using System.Collections.Generic;
using System.Linq;
using RestAppDAL.Context;
using RestAppDAL.Entities;
using RestAppDAL.Interfaces;

namespace RestAppDAL.Repositories
{
    internal class MockCustomerRepository : IRepository<Customer>
    {
        private static int _id;
        private readonly MockContext _mockContext;

        public MockCustomerRepository()
        {
            _mockContext = new MockContext();
            _id = _mockContext.Customers.Count;
        }

        public Customer Create(Customer customerToCreate)
        {
            if (CustomerExists(customerToCreate)) return null;
            if (CustomerIsEmpty(customerToCreate)) return null;

            var newCustomer = new Customer
            {
                Id = _id++,
                FirstName = customerToCreate.FirstName,
                LastName = customerToCreate.LastName,
                Address = customerToCreate.Address
            };
            _mockContext.Customers.Add(newCustomer);
            return newCustomer;
        }

        public IEnumerable<Customer> GetAll()
        {
            return new List<Customer>(_mockContext.Customers);
        }

        public Customer GetById(int id)
        {
            return _mockContext.Customers.FirstOrDefault(c => c.Id == id);
        }

        public bool Delete(int id)
        {
            var customerToDelete = GetById(id);
            if (customerToDelete == null) return false;
            _mockContext.Customers.Remove(customerToDelete);
            return true;
        }

        private bool CustomerIsEmpty(Customer customerToCreate)
        {
            return customerToCreate.FirstName == null
                   || customerToCreate.LastName == null
                   || customerToCreate.Address == null;
        }

        private bool CustomerExists(Customer customerToCreate)
        {
            return _mockContext.Customers.Exists(c =>
                c.FirstName.Equals(customerToCreate.FirstName) &&
                c.LastName.Equals(customerToCreate.LastName) &&
                c.Address.Equals(customerToCreate.Address));
        }
    }
}