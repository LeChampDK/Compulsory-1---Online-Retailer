using System.Collections.Generic;
using SharedModel;
using SharedModels;

namespace OrderApi.Infrastructure
{
    public interface IMessagePublisher
    {
        void PublishOrderCreatedMessage(OrderCreatedMessage orderMessage);
    }
}
