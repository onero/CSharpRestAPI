using System;
using RestAppBLL.BusinessObjects;
using RestAppDAL.Entities;

namespace RestAppBLL.Converters
{
    internal class AddressConverter
    {
        internal Address Convert(AddressBO address)
        {
            if (address == null) return null;

            return new Address()
            {
                Id = address.Id,
                City = address.City,
                Number = address.Number,
                Street = address.Street
            };
        }
        internal AddressBO Convert(Address address)
        {
            if (address == null) return null;

            return new AddressBO()
            {
                Id = address.Id,
                City = address.City,
                Number = address.Number,
                Street = address.Street
            };
        }
    }
}