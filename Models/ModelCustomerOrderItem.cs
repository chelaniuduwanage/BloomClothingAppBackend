namespace Trendy_BackEnd.Models
{
    public class ModelCustomerOrderItem
    {
        public int? Id { get; set; }
        public string? CustomerOrderID { get; set; }
        public int? ItemId { get; set; }
        public string? Name { get; set; }
        public string? Link { get; set; }
        public decimal? Price { get; set; }
        public int? Qty { get; set; }
        public decimal? Total { get; set; }
        public string? size { get; set; }
        public string? Color { get; set; }
    }
}
