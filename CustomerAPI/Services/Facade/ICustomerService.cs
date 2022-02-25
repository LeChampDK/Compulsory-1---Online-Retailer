using CustomerAPI.Models;

namespace CustomerAPI.Services.Facade
{
    public interface ICustomerService
    {
        Task<CustomerModel> Get(int customerId);
        Task Add(CustomerModel customer);
        Task Update(CustomerModel customer);
        Task Delete(int customerId);
    }
}
