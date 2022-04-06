using System;
using System.Collections.Generic;
using EasyNetQ;
using SharedModel;
using SharedModel.Messages.LoginComponent;
using SharedModels;

namespace OrderApi.Infrastructure
{
    public class MessagePublisher : IMessagePublisher, IDisposable
    {
        IBus bus;

        public MessagePublisher(string connectionString)
        {
            bus = RabbitHutch.CreateBus(connectionString);
        }

        public void Dispose()
        {
            bus.Dispose();
        }

        public void PublishOrderStatusChangedMessage(int? customerId, IList<SharedModel.OrderLine> orderLines)
        {
            var message = new OrderStatusChangedMessage
            {
                CustomerId = customerId,
                OrderLines = orderLines
            };

            bus.PubSub.Publish(message);
        }

        public void PublishOrderCreatedMessage(OrderCreatedMessage orderMessage)
        {
            bus.PubSub.Publish(orderMessage);
        }

        public void LoginMessage(Login login)
        {
            var request = new LoginRequest()
            {
                email = login.email,
                password = login.password
            };

            bus.PubSub.Publish(request);
        }

    }
}
