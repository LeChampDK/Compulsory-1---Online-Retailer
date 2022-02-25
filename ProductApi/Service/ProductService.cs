using ProductApi.Models;
using ProductApi.Service.Facade;
using System.Collections.Generic;

namespace ProductApi.Service
{
    public class ProductService : IProductService<Product>
    {
        public Product Add(Product entity)
        {
            throw new System.NotImplementedException();
        }

        public void Edit(Product entity)
        {
            throw new System.NotImplementedException();
        }

        public Product Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Product> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
