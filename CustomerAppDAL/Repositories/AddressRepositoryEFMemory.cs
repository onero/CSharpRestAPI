using System.Collections.Generic;
using System.Linq;
using RestAppDAL.Context;
using RestAppDAL.Entities;
using RestAppDAL.Interfaces;

namespace RestAppDAL.Repositories
{
    public class AddressRepositoryEFMemory : IRepository<Address>
    {
        private readonly InMemoryContext _context;

        public AddressRepositoryEFMemory(InMemoryContext context)
        {
            _context = context;
        }
        public Address Create(Address entity)
        {
            var createdAddress = _context.Addresses.Add(entity);
            return createdAddress.Entity;
        }

        public IEnumerable<Address> GetAllById(List<int> ids)
        {
            return ids == null ? 
                null : 
                _context.Addresses.Where(a => ids.Contains(a.Id));
        }

        public IEnumerable<Address> GetAll()
        {
            return _context.Addresses.ToList();
        }

        public Address GetById(int id)
        {
            return _context.Addresses.FirstOrDefault(a => a.Id == id);

        }

        public bool Delete(int id)
        {
            var address = GetById(id);
            if (address == null) return false;
            _context.Addresses.Remove(address);
            return true;
        }
    }
}