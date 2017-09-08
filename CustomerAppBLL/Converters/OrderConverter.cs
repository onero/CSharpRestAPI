using RestAppBLL.BusinessObjects;
using RestAppDAL.Entities;

namespace RestAppBLL.Converters
{
    internal static class OrderConverter
    {
        public static Order Convert(OrderBO order)
        {
            if (order == null) return null;

            return new Order()
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                DeliveryDate = order.DeliveryDate,
                CustomerId = order.CustomerId
            };
        }

        public static OrderBO Convert(Order order)
        {
            if (order == null) return null;

            return new OrderBO()
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                DeliveryDate = order.DeliveryDate,
                Customer = new CustomerConverter().Convert(order.Customer),
                CustomerId = order.CustomerId
            };
        }
    }
}