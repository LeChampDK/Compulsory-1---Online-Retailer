using SharedModel;

namespace OrderApi.Infrastructure
{
    public interface IMessagePublisher
    {
        void PublishOrderCreatedMessage(OrderCreatedMessage orderMessage);
        void PublishLoginCreateMessage(Login login);
        void PublishLoginMessage(Login login);
    }
}
