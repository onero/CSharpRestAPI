using System.Collections.Generic;
using System.Linq;
using RestAppBLL.BusinessObjects;
using RestAppBLL.Converters;
using RestAppDAL;

namespace RestAppBLL.Services
{
    public class AddressService : IService<AddressBO>
    {
        private readonly DALFacade _facade;
        private readonly AddressConverter _converter;

        public AddressService(DALFacade facade)
        {
            _facade = facade;
            _converter = new AddressConverter();
        }

        public AddressBO Create(AddressBO entityToCreate)
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                var newAddress = unitOfWork.AddressRepository.Create(_converter.Convert(entityToCreate));
                unitOfWork.Complete();
                return _converter.Convert(newAddress);
            }
        }

        public IList<AddressBO> CreateAll(IList<AddressBO> addresses)
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                var createdAddresses = new List<AddressBO>();
                foreach (var address in addresses)
                {
                    var createdAddress = unitOfWork.AddressRepository.Create(_converter.Convert(address));
                    createdAddresses.Add(_converter.Convert(createdAddress));
                }
                unitOfWork.Complete();
                return addresses;
            }
        }

        public IEnumerable<AddressBO> GetAll()
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                return unitOfWork.AddressRepository.GetAll().Select(_converter.Convert).ToList();
            }
        }

        public AddressBO GetById(int id)
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                var addressFromDB = unitOfWork.AddressRepository.GetById(id);
                if (addressFromDB == null) return null;
                return _converter.Convert(addressFromDB);
            }
        }

        public bool Delete(int id)
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                var address = GetById(id);

                if (address == null) return false;

                unitOfWork.AddressRepository.Delete(id);
                unitOfWork.Complete();

                return true;
            }
        }

        public AddressBO Update(AddressBO address)
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                var addressFromDB = unitOfWork.AddressRepository.GetById(address.Id);
                if (addressFromDB == null) return null;

                addressFromDB.Id = address.Id;
                addressFromDB.City = address.City;
                addressFromDB.Number = address.Number;
                addressFromDB.Street = address.Street;
                unitOfWork.Complete();

                return _converter.Convert(addressFromDB);
            }
        }
    }
}