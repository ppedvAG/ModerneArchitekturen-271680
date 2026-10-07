namespace Order.WebApi.Models.Domain
{
    public class OrderEntity
    {
        public string OrderId { get; set; }

        public string CustomerId { get; set; }

        public string ProductId { get; set; }

        public DateTime DeliveryDate { get; set; }

        public bool IsCancelled { get; set; }
    }
}
