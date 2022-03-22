using CustomerAPI.Services.Facade;
using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using SharedModel;
using System;
using System.Threading;

namespace CustomersAPI.Infrastructure
{
    public class MessageListener
    {
        IServiceProvider provider;
        string connectionString;
        IBus bus;

        public MessageListener(IServiceProvider provider, string connectionString)
        {
            this.provider = provider;
            this.connectionString = connectionString;
        }

        public void Start()
        {
            using (bus = RabbitHutch.CreateBus(connectionString))
            {
                bus.PubSub.Subscribe<OrderCreatedMessage>("checkCustomerExists",
                    HandleCheckCustomer);

                // Block the thread so that it will not exit and stop subscribing.
                lock (this)
                {
                    Monitor.Wait(this);
                }
            }
        }

        private void HandleCheckCustomer(OrderCreatedMessage c)
        {
            using (var scope = provider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var customerService = services.GetService<ICustomerService>();

                var doesExist = customerService.CheckIfCustomerExists(c.CustomerId);

                if (doesExist)
                {
                    var customerExists = new CustomerExistAccepted()
                    {
                        CustomerId = c.CustomerId,
                        OrderId = c.OrderId,
                        OrderLines = c.OrderLines,
                    };
                    bus.PubSub.Publish(customerExists);

                }
                else
                {
                    var customerDoesNotExist = new CustomerExistRejected()
                    {
                        OrderId = c.OrderId
                    };

                    bus.PubSub.Publish(customerDoesNotExist);


                }
            }
        }
    }
}
