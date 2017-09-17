using System;
using System.Collections.Generic;
using System.Linq;
using RestAppBLL.BusinessObjects;
using RestAppBLL.Converters;
using RestAppDAL.Interfaces;

namespace RestAppBLL.Services
{
    public class CustomerService : IService<CustomerBO>
    {
        private readonly CustomerConverter _converter;
        private readonly AddressConverter _addressConverter;
        private readonly IDALFacade _dalFacade;

        public CustomerService(IDALFacade facade)
        {
            _dalFacade = facade;
            _addressConverter = new AddressConverter();
            _converter = new CustomerConverter();
        }

        public CustomerBO Create(CustomerBO customerToCreate)
        {
            using (var uow = _dalFacade.UnitOfWork)
            {
                var newCustomer = uow.CustomerRepository.Create(_converter.Convert(customerToCreate));
                uow.Complete();
                return _converter.Convert(newCustomer);
            }
        }

        public IList<CustomerBO> CreateAll(IList<CustomerBO> customers)
        {
            using (var uow = _dalFacade.UnitOfWork)
            {
                var listOfCustomers = new List<CustomerBO>();
                foreach (var customer in customers)
                {
                    var createdCustomer = uow.CustomerRepository.Create(_converter.Convert(customer));
                    listOfCustomers.Add(_converter.Convert(createdCustomer));
                }
                uow.Complete();
                return listOfCustomers;
            }
        }

        public IEnumerable<CustomerBO> GetAll()
        {
            using (var uow = _dalFacade.UnitOfWork)
            {
                // Customer -> CustomerBO
                return uow.CustomerRepository.GetAll().Select(_converter.Convert).ToList();
            }
        }

        public CustomerBO GetById(int id)
        {
            using (var uow = _dalFacade.UnitOfWork)
            {
                var customer = uow.CustomerRepository.GetById(id);
                if (customer == null) return null;
                var convertedCustomer = _converter.Convert(customer);

                /*convertedCustomer.Addresses = convertedCustomer.AddressIds?
                    .Select(addressId => _addressConverter.Convert(uow.AddressRepository.GetById(addressId)))
                    .ToList();*/

                convertedCustomer.Addresses = uow.AddressRepository.GetAllById(convertedCustomer.AddressIds)
                    .Select(a => _addressConverter.Convert(a))
                    .ToList();
                return convertedCustomer;
            }
        }

        public bool Delete(int id)
        {
            using (var uow = _dalFacade.UnitOfWork)
            {
                var customerFromDB = uow.CustomerRepository.GetById(id);
                if (customerFromDB == null) return false;
                var customerDeleted = uow.CustomerRepository.Delete(id);
                uow.Complete();
                return customerDeleted;
            }
        }

        public CustomerBO Update(CustomerBO updatedCustomer)
        {
            using (var uow = _dalFacade.UnitOfWork)
            {
                var customerFromDb = uow.CustomerRepository.GetById(updatedCustomer.Id);
                if (customerFromDb == null) return null;

                var customerUpdated = _converter.Convert(updatedCustomer);
                customerFromDb.FirstName = customerUpdated.FirstName;
                customerFromDb.LastName = customerUpdated.LastName;

                // Remove all addresses from old customer, that doesn't exist in updated
                customerFromDb.Addresses.RemoveAll(
                    ca => !customerUpdated.Addresses.Exists(
                    a => a.AddressId == ca.AddressId &&
                    a.CustomerId == ca.CustomerId));

                // Remove all "old" addresses, so we only add new one to DB!
                customerUpdated.Addresses.RemoveAll(
                    ca => customerFromDb.Addresses.Exists(
                        a => a.AddressId == ca.AddressId &&
                        a.CustomerId == ca.CustomerId));

                // Add all new addresses
                customerFromDb.Addresses.AddRange(
                    customerUpdated.Addresses);

                uow.Complete();
                return _converter.Convert(customerFromDb);
            }
        }
    }
}