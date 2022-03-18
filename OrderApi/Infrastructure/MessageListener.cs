using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using OrderApi.Data.Facade;
using OrderApi.Models;
using OrderApi.Service.Facade;
using SharedModel;
using SharedModel.Messages;
using System;
using System.Collections.Generic;
using System.Threading;
using static OrderApi.Enums.Enums;

namespace OrderApi.Infrastructure
{
    public class MessageListener
    {
        IOrderService<Order> _service;
        IServiceProvider _provider;
        string _connectionString;
        IBus _bus;

        public MessageListener(IServiceProvider provider, string connectionString, IOrderService<Order> service)
        {
            _service = service;
            _provider = provider;
            _connectionString = connectionString;
        }

        public void Start()
        {
            using (_bus = RabbitHutch.CreateBus(_connectionString))
            {
                _bus.PubSub.Subscribe<ProductAcceptResponse>("ProductAPIAccept", HandleProductAccepted);

                _bus.PubSub.Subscribe<ProductRejectResponse>("ProductAPIAccept", HandleProductRejected);

                _bus.PubSub.Subscribe<CustomerExistAccepted>("CustomereAPIAccept", HandleCustomerAccepted);

                _bus.PubSub.Subscribe<CustomerExistRejected>("CustomerAPIRejected", HandleCustomerRejected);

                lock (this)
                {
                    Monitor.Wait(this);
                }
            }
        }

        private void HandleCustomerRejected(CustomerExistRejected message)
        {
            using (var scope = _provider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var orderRepos = services.GetService<IRepository<Order>>();

                // Delete tentative order.
                orderRepos.Remove(message.OrderId);

                Console.WriteLine(message.OrderId + "was rejected because customer does not exist");
            }
        }

        private void HandleCustomerAccepted(CustomerExistAccepted message)
        {
            var productRequest = new ProductRequest
            {
                Id = message.OrderId,
                Products = message.OrderLines
            };

            _bus.PubSub.Publish(productRequest);
        }

        private void HandleProductRejected(ProductRejectResponse message)
        {
            using (var scope = _provider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var orderRepos = services.GetService<IRepository<Order>>();

                // Delete tentative order.
                orderRepos.Remove(message.Id);

                Console.WriteLine(message.Id + "has been rejected from productAPI");
            }
        }

        private void HandleProductAccepted(ProductAcceptResponse message)
        {
            using (var scope = _provider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var orderRepos = services.GetService<IRepository<Order>>();

                // Mark order as completed
                var order = orderRepos.Get(message.Id);
                order.Status = OrderStatus.Completed;
                orderRepos.Edit(order);

                Console.WriteLine(message.Id + "has been accepted from productAPI");
            }
        }
    }
}
