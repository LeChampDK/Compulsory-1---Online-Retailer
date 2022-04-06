using OrderApi.Data.Facade;
using OrderApi.Service.Facade;
using System;
using System.Collections.Generic;
using OrderApi.Infrastructure;
using SharedModel;
using OrderApi.Models;

namespace OrderApi.Service
{
    public class OrderService : IOrderService
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

        public void PostOrder(Order order)
        {
            if (order == null)
            {
                throw new Exception();
            }

            var addedOrder = _repository.Add(order);
            var newListOrderLines = new List<SharedModel.OrderLine>();

            foreach (var line in addedOrder.OrderLines)
            {
                var tempLine = new SharedModel.OrderLine()
                {
                    ProductId = line.ProductId,
                    Quantity = line.Quantity,
                };

                newListOrderLines.Add(tempLine);
            }

            var newList = addedOrder.OrderLines;
            var orderMessage = new OrderCreatedMessage
            {
                CustomerId = addedOrder.customerId,
                OrderId = addedOrder.Id,
                OrderLines = newListOrderLines
            };

            messagePublisher.PublishOrderCreatedMessage(orderMessage);
        }

        public void Remove(int id)
        {
            _repository.Remove(id);
        }

        public void Login(Login login)
        {
            messagePublisher.PublishLoginMessage(login);
        }

        public void LoginCreate(Login login)
        {
            messagePublisher.PublishLoginCreateMessage(login);
        }
    }
}
