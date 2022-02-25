namespace CustomerAPI.Models
{
    public class CustomerModel
    {
        public int customerId { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string billingAddress { get; set; }
        public string shippingAddress { get; set; }
        public string creditStanding { get; set; }
    }
}
