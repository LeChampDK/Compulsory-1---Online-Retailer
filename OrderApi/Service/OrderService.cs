using OrderApi.Data.Facade;
using SharedModels;
using OrderApi.Service.Facade;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderApi.Service
{
    public class OrderService : IOrderService<Order>
    {
        private readonly IRepository<Order> _repository;
        public OrderService(IRepository<Order> repository)
        {
            _repository = repository;
        }

        public Order Add(Order entity)
        {
            var result = _repository.Add(entity);
            return result;
        }

        public void Edit(Order entity)
        {
            _repository.Edit(entity);
        }

        public Order Get(int id)
        {
            var result = _repository.Get(id);
            return result;
        }

        public IEnumerable<Order> GetAll()
        {
            return _repository.GetAll();
        }

        public Order PostOrder(Order order)
        {
            // Call ProductApi to get the product ordered
            RestClient c = new RestClient();
            // You may need to change the port number in the BaseUrl below
            // before you can run the request.
            c.BaseUrl = new Uri("https://localhost:5001/products/");

            foreach (var prod in order.Products)
            {
                var request = new RestRequest(prod.ProductId.ToString(), Method.GET);
                var response = c.Execute<Product>(request);
                var orderedProduct = response.Data;

                if (prod.Quantity <= orderedProduct.ItemsInStock - orderedProduct.ItemsReserved)
                {
                    // reduce the number of items in stock for the ordered product,
                    // and create a new order.
                    orderedProduct.ItemsReserved += prod.Quantity;
                    var updateRequest = new RestRequest(orderedProduct.Id.ToString(), Method.PUT);
                    updateRequest.AddJsonBody(orderedProduct);
                    var updateResponse = c.Execute(updateRequest);

                    if (updateResponse.IsSuccessful)
                    {
                        var newOrder = _repository.Add(order);
                        return newOrder;
                    }
                }
            }

            throw new Exception("Error creating new order");
        }

        public void Remove(int id)
        {
            _repository.Remove(id);
        }
    }
}
