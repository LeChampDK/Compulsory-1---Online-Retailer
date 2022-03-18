using System.Collections.Generic;
using SharedModels;

namespace OrderApi.Service.Facade
{
    public interface IOrderService<T>
    {
        void PostOrder(Order order);
        IEnumerable<T> GetAll();
        T Get(int id);
        T Add(T entity);
        void Edit(T entity);
        void Remove(int id);
    }
}
