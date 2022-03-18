using OrderApi.Data.Facade;
using SharedModels;
using OrderApi.Service.Facade;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrderApi.Infrastructure;

namespace OrderApi.Service
{
    public class OrderService : IOrderService<Order>
    {
        private readonly IRepository<Order> _repository;
        IMessagePublisher messagePublisher;
        public OrderService(IRepository<Order> repository, 
            IMessagePublisher publisher)
        {
            messagePublisher = publisher;
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

        }

        public void Remove(int id)
        {
            _repository.Remove(id);
        }
    }
}
