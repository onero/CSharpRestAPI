using RestAppBLL.BusinessObjects;
using RestAppDAL.Entities;

namespace RestAppBLL.Converters
{
    internal static class OrderConverter
    {
        public static Order Convert(OrderBO entityToCreate)
        {
            return new Order()
            {
                Id = entityToCreate.Id,
                OrderDate = entityToCreate.OrderDate,
                DeliveryDate = entityToCreate.DeliveryDate
            };
        }

        public static OrderBO Convert(Order entityToCreate)
        {
            return new OrderBO()
            {
                Id = entityToCreate.Id,
                OrderDate = entityToCreate.OrderDate,
                DeliveryDate = entityToCreate.DeliveryDate
            };
        }
    }
}