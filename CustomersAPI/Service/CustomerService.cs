using CustomerAPI.Data;
using CustomerAPI.Models;
using CustomerAPI.Services.Facade;
using System.Threading.Tasks;

namespace CustomerAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<CustomerModel> _repo;

        public CustomerService(IRepository<CustomerModel> repo)
        {
            _repo = repo;
        }

        public Task Add(CustomerModel customer)
        {
            _repo.Add(customer);
            return Task.CompletedTask;
        }

        public bool CheckIfCustomerExists(int customerId)
        {
            if (string.IsNullOrEmpty(customerId.ToString()))
            {
                return false;
            }

            var customer = _repo.Get(customerId);

            if (customer == null)
            {
                return false;
            }

            return true;
        }

        public Task Delete(int customerId)
        {
            _repo.Remove(customerId);
            return Task.CompletedTask;
        }

        public async Task<CustomerModel> Get(int customerId)
        {
            CustomerModel dudebro = _repo.Get(customerId);
            return dudebro;
        }

        public Task Update(CustomerModel customer)
        {
            _repo.Edit(customer);
            return Task.CompletedTask;
        }
    }
}
