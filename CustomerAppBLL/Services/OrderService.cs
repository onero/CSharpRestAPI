using System.Collections.Generic;
using System.Linq;
using RestAppBLL.BusinessObjects;
using RestAppBLL.Converters;
using RestAppDAL;
using RestAppDAL.Entities;

namespace RestAppBLL.Services
{
    internal class OrderService : IService<OrderBO>
    {
        private readonly DALFacade _facade = new DALFacade();
        public OrderBO Create(OrderBO entityToCreate)
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                var newOrder = unitOfWork.OrderRepository.Create(OrderConverter.Convert(entityToCreate));
                unitOfWork.Complete();
                return OrderConverter.Convert(newOrder);
            }
        }

        public IList<OrderBO> CreateAll(IList<OrderBO> customers)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<OrderBO> GetAll()
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                return unitOfWork.OrderRepository.GetAll().Select(OrderConverter.Convert).ToList();
            }
        }

        public OrderBO GetById(int id)
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                var orderFromDB = unitOfWork.OrderRepository.GetById(id);
                return orderFromDB == null ? null : OrderConverter.Convert(orderFromDB);
            }
        }

        public bool Delete(int id)
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                var order = unitOfWork.OrderRepository.GetById(id);
                if (order == null) return false;
                unitOfWork.OrderRepository.Delete(id);
                unitOfWork.Complete();
                return true;
            }
        }

        public OrderBO Update(OrderBO entityToUpdate)
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                var order = unitOfWork.OrderRepository.GetById(entityToUpdate.Id);
                if (order == null) return null;
                order.DeliveryDate = entityToUpdate.DeliveryDate;
                order.OrderDate = entityToUpdate.OrderDate;
                unitOfWork.Complete();
                return OrderConverter.Convert(order);
            }
        }
    }
}