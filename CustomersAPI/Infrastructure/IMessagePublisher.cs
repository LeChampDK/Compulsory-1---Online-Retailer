using SharedModel;

namespace CustomersAPI.Infrastructure
{
    public interface IMessagePublisher
    {
        void PublishCustomerExistAccepted(CustomerExistAccepted customerExistAccepted);
        void PublishCustomerExistRejected(CustomerExistRejected customerExistRejected);
    }
}
