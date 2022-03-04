using CustomerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerAPI.Data
{
    public class CustomerRepository : IRepository<CustomerModel>
    {
        private readonly CustomerApiContext db;

        public CustomerRepository(CustomerApiContext context)
        {
            db = context;
        }

        CustomerModel IRepository<CustomerModel>.Add(CustomerModel entity)
        {
            var newCustomer = db.Customers.Add(entity).Entity;
            db.SaveChanges();
            return newCustomer;
        }

        void IRepository<CustomerModel>.Edit(CustomerModel entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }

        CustomerModel IRepository<CustomerModel>.Get(int id)
        {
            return db.Customers.FirstOrDefault(p => p.customerId == id);
        }

        IEnumerable<CustomerModel> IRepository<CustomerModel>.GetAll()
        {
            return db.Customers.ToList();
        }

        void IRepository<CustomerModel>.Remove(int id)
        {
            var product = db.Customers.FirstOrDefault(p => p.customerId == id);
            db.Customers.Remove(product);
            db.SaveChanges();
        }
    }
}
