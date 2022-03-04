using CustomerAPI.Models;

namespace CustomerAPI.Data
{
    public class DbInitializer : IDbInitializer
    {
        public void Initialize(CustomerApiContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // Look for any Products
            if (context.Customers.Any())
            {
                return;   // DB has been seeded
            }

            List<CustomerModel> customers = new List<CustomerModel>
            {
                new CustomerModel { customerId = 1, email = "email1@email.email", name = "johnson1", billingAddress = "BillingAddress1", creditStanding = "good?1", phoneNumber = "44468491", shippingAddress = "ShippingAddress1" },
                new CustomerModel { customerId = 2, email = "email2@email.email", name = "johnson2", billingAddress = "BillingAddress2", creditStanding = "good?2", phoneNumber = "44468492", shippingAddress = "ShippingAddress2" },
                new CustomerModel { customerId = 3, email = "email3@email.email", name = "johnson3", billingAddress = "BillingAddress3", creditStanding = "good?3", phoneNumber = "44468493", shippingAddress = "ShippingAddress3" },
            };

            context.Customers.AddRange(customers);
            context.SaveChanges();
        }
    }
}
