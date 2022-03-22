using EasyNetQ;
using SharedModel;

namespace CustomersAPI.Infrastructure
{
    public class MessagePublisher : IMessagePublisher
    {
        IBus bus;

        public MessagePublisher(string connectionString)
        {
            bus = RabbitHutch.CreateBus(connectionString);
        }

        public void PublishCustomerExistAccepted(CustomerExistAccepted customerExistAccepted)
        {
            bus.PubSub.Publish(customerExistAccepted);
        }

        public void PublishCustomerExistRejected(CustomerExistRejected customerExistRejected)
        {
            bus.PubSub.Publish(customerExistRejected);
        }
    }
}
