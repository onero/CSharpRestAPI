using System.Collections.Generic;
using System.Linq;
using RestAppBLL.BusinessObjects;
using RestAppBLL.Converters;
using RestAppDAL;

namespace RestAppBLL.Services
{
    internal class OrderService : IService<OrderBO>
    {
        private readonly DALFacade _facade;

        public OrderService(DALFacade facade)
        {
            _facade = facade;
        }

        public OrderBO Create(OrderBO entityToCreate)
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                var newOrder = unitOfWork.OrderRepository.Create(OrderConverter.Convert(entityToCreate));
                unitOfWork.Complete();
                return OrderConverter.Convert(newOrder);
            }
        }

        public IList<OrderBO> CreateAll(IList<OrderBO> orders)
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                var createdOrders = new List<OrderBO>();
                foreach (var order in orders)
                {
                    var createdOrder = unitOfWork.OrderRepository.Create(OrderConverter.Convert(order));
                    createdOrders.Add(OrderConverter.Convert(createdOrder));
                }
                unitOfWork.Complete();
                return orders;
            }
        }

        /// <summary>
        /// Get all prders
        /// </summary>
        /// <returns>List of orders</returns>
        /// <remarks>Customer attribute will be null</remarks>
        public IEnumerable<OrderBO> GetAll()
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                return unitOfWork.OrderRepository.GetAll().Select(OrderConverter.Convert).ToList();
            }
        }

        /// <summary>
        /// Get order by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>OrderBO</returns>
        /// <remarks>OrderBO customer attribute will be returned</remarks>
        public OrderBO GetById(int id)
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                var orderFromDB = unitOfWork.OrderRepository.GetById(id);
                if (orderFromDB == null) return null;
                // Get customer associated with the order
                orderFromDB.Customer = unitOfWork.CustomerRepository.GetById(orderFromDB.CustomerId);
                return OrderConverter.Convert(orderFromDB);
            }
        }

        /// <summary>
        /// Delete order by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true, if deleted</returns>
        public bool Delete(int id)
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                var order = GetById(id);

                if (order == null) return false;

                unitOfWork.OrderRepository.Delete(id);
                unitOfWork.Complete();

                return true;
            }
        }

        /// <summary>
        /// Update order with provided order
        /// </summary>
        /// <param name="orderBO"></param>
        /// <returns></returns>
        public OrderBO Update(OrderBO orderBO)
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                var orderFromDB = unitOfWork.OrderRepository.GetById(orderBO.Id);
                if (orderFromDB == null) return null;

                orderFromDB.DeliveryDate = orderBO.DeliveryDate;
                orderFromDB.OrderDate = orderBO.OrderDate;
                orderFromDB.CustomerId = orderFromDB.CustomerId;
                unitOfWork.Complete();

                return OrderConverter.Convert(orderFromDB);
            }
        }
    }
}