using System;

namespace LeoStore.Domain.StoreContext.Queries
{
    public class CustomerOrderCountResult
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Document { get; set; }
        public int Orders { get; set; }
    }
}