using OrderApi.Models;
using OrderApi.Service.Facade;
using System.Collections.Generic;

namespace OrderApi.Service
{
    public class OrderService : IOrderService<Order>
    {
        public Order Add(Order entity)
        {
            throw new System.NotImplementedException();
        }

        public void Edit(Order entity)
        {
            throw new System.NotImplementedException();
        }

        public Order Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Order> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
