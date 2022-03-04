using System.ComponentModel.DataAnnotations;

namespace OrderApi.Models
{
    public class OrderProducts
    {
        [Key]
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
