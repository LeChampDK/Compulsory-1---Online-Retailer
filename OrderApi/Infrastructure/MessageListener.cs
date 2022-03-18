using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using OrderApi.Data.Facade;
using OrderApi.Models;
using SharedModel.Messages;
using System;
using System.Threading;
using static OrderApi.Enums.Enums;

namespace OrderApi.Infrastructure
{
    public class MessageListener
    {

        IServiceProvider _service;
        string _connectionString;
        IBus _bus;

        public MessageListener(IServiceProvider service, string connectionString)
        {
            _service = service;
            _connectionString = connectionString;
        }

        public void Start()
        {
            using (_bus = RabbitHutch.CreateBus(_connectionString))
            {
                _bus.PubSub.Subscribe<ProductAcceptResponse>("ProductAPIAccept", HandleProductAccepted);

                _bus.PubSub.Subscribe<ProductRejectResponse>("ProductAPIAccept", HandleProductRejected);

                lock (this)
                {
                    Monitor.Wait(this);
                }
            }
        }

        private void HandleProductRejected(ProductRejectResponse message)
        {
            using (var scope = _service.CreateScope())
            {
                var services = scope.ServiceProvider;
                var orderRepos = services.GetService<IRepository<Order>>();

                // Delete tentative order.
                orderRepos.Remove(message.Id);
            }
        }

        private void HandleProductAccepted(ProductAcceptResponse message)
        {
            using (var scope = _service.CreateScope())
            {
                var services = scope.ServiceProvider;
                var orderRepos = services.GetService<IRepository<Order>>();

                // Mark order as completed
                var order = orderRepos.Get(message.Id);
                order.Status = OrderStatus.Completed;
                orderRepos.Edit(order);
            }
        }
    }
}
