using OrderApi.Models;
using System.Collections.Generic;

namespace OrderApi.Service.Facade
{
    public interface IOrderService<T>
    {
        Order PostOrder(Order order);
        IEnumerable<T> GetAll();
        T Get(int id);
        T Add(T entity);
        void Edit(T entity);
        void Remove(int id);
    }
}
