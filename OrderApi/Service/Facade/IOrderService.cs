using OrderApi.Models;
using System.Collections.Generic;

namespace OrderApi.Service.Facade
{
    public interface IOrderService<T>
    {
        Order PostOrder(Order order);
    }
}
