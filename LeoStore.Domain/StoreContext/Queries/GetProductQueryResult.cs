namespace LeoStore.Domain.StoreContext.Queries
{
    public class GetProductQueryResult
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Image { get; private set; }
        public decimal Price { get; private set; }
        public decimal QuantityOnHand { get; private set; }
    }
}