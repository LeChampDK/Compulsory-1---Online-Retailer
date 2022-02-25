using System.Collections.Generic;

namespace ProductApi.Service.Facade
{
    public interface IProductService<T>
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        T Add(T entity);
        void Edit(T entity);
        void Remove(int id);
    }
}
