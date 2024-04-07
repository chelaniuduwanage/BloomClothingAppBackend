namespace Trendy_BackEnd.Models
{
    public class ModelCustomerOrder
    {
        public int? Id { get; set; }
        public string? CustomerOrderID { get; set; }
        public int? UserId { get; set; }
        public string? Address { get; set; }
        public string? FirstName { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? Discount { get; set; }
        public decimal? FullTotal { get; set; }
        public string? PaymentMethod { get; set; }
        public string? CardLastDigits { get; set; }
        public List<ModelCustomerOrderItem>? Items { get; set; } = new List<ModelCustomerOrderItem>();
    }
}
