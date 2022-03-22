using System.Collections.Generic;
using SharedModels;

namespace OrderApi.Service.Facade
{
    public interface IOrderService
    {
        void PostOrder(Order order);
        IEnumerable<Order> GetAll();
        Order Get(int id);
        Order Add(Order entity);
        void Edit(Order entity);
        void Remove(int id);
    }
}
