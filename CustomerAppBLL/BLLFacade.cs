﻿using RestAppBLL.BusinessObjects;
using RestAppBLL.Services;
using RestAppDAL;

namespace RestAppBLL
{
    public class BLLFacade
    {
        public IService<CustomerBO> CustomerService => new CustomerService(new DALFacade());

        public IService<OrderBO> OrderService => new OrderService(new DALFacade());
        public IService<AddressBO> AddressService => new AddressService(new DALFacade());
    }
}