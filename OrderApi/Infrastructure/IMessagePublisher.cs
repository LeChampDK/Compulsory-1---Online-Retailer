using SharedModel;

namespace OrderApi.Infrastructure
{
    public interface IMessagePublisher
    {
        void PublishOrderCreatedMessage(OrderCreatedMessage orderMessage);
    }
}
