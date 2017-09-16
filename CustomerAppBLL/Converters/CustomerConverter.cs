using System.Linq;
using RestAppBLL.BusinessObjects;
using RestAppDAL.Entities;

namespace RestAppBLL.Converters
{
    internal class CustomerConverter
    {
        /// <summary>
        /// Convert CustomerBO to Customer for DAL
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>Customer</returns>
        internal Customer Convert(CustomerBO customer)
        {
            if (customer == null) return null;

            return new Customer()
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Addresses = customer.AddressIds?.Select(aId => new CustomerAddress()
                {
                    AddressId = aId,
                    CustomerId = customer.Id
                }).ToList()
            };
        }

        /// <summary>
        /// Convert Customer to CustomerBO for BLL
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>CustomerBO</returns>
        internal CustomerBO Convert(Customer customer)
        {
            if (customer == null) return null;

            return new CustomerBO()
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                AddressIds = customer.Addresses?.Select(a => a.AddressId).ToList()
            };
        }
    }
}