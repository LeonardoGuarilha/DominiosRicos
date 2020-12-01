namespace LeoStore.Domain.StoreContext.Queries
{
    public class GetOrderQueryResult
    {
        public string CustomerId { get; set; }

        public string OrderId { get; set; }

        public decimal Quantity { get; set; }

    }
}